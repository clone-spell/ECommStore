using ECommStoreWeb.Data;
using ECommStoreWeb.Models;
using ECommStoreWeb.Repository;
using ECommStoreWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommStoreWeb.Controllers
{
    [Authorize(Roles ="seller")]
    public class ProductController : Controller
    {
        private readonly IRepository _repositories;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IRepository repositories, IWebHostEnvironment webHostEnvironment)
        {
            _repositories=repositories;
            _webHostEnvironment=webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _repositories.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProductViewModel();
            vm.ProductCategoryList = _repositories.GetAllSubCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.ProductName = model.ProductName;
                p.Description = model.Description;
                p.Price = model.Price;
                p.StockQuantity = model.StockQuantity;
                p.ProductCategory = model.ProductCategory;
                string oldPhotoLink = model.ProductPhotoLink;
                p.ProductPhotoLink = UploadProductPhoto(model.ProductPhoto, oldPhotoLink);

                _repositories.AddProduct(p);
                TempData["success"] = "Product Added Successfully!";
                return RedirectToAction("Index");
            }

            model.ProductCategoryList = _repositories.GetAllSubCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product model = _repositories.GetProduct(id);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.ProductName = model.ProductName;
            viewModel.Description = model.Description;
            viewModel.Price = model.Price;
            viewModel.ProductPhotoLink = model.ProductPhotoLink;
            viewModel.StockQuantity = model.StockQuantity;
            viewModel.ProductId = model.ProductId;
            viewModel.ProductCategory = model.ProductCategory;
            viewModel.ProductCategoryList = _repositories.GetAllSubCategories().Select(x=> new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = model.ProductCategory.Equals(x.Id)
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Product p = _repositories.GetProduct(viewModel.ProductId);
                string oldPhotoLink = p.ProductPhotoLink;
                p.ProductPhotoLink = UploadProductPhoto(viewModel.ProductPhoto, oldPhotoLink);
                p.ProductName = viewModel.ProductName;
                p.Description = viewModel.Description;
                p.ProductCategory = viewModel.ProductCategory;
                p.StockQuantity = viewModel.StockQuantity;
                p.Price = viewModel.Price;
                
                _repositories.EditProduct(p);
                TempData["success"] = "Changes are Saved Successfully!";
                return RedirectToAction("Index");
            }

            viewModel.ProductCategoryList = _repositories.GetAllSubCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            Product p = _repositories.GetProduct(id);
            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteProduct(int id)
        {
            if(ModelState.IsValid)
            {
                var product = _repositories.GetProduct(id);
                string photoLink = product.ProductPhotoLink;
                _repositories.DeleteProduct(id);

                if(photoLink != null)
                {
                    string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages", photoLink);
                    if (System.IO.File.Exists(photoPath))
                    {
                        Thread.Sleep(1000);
                        System.IO.File.Delete(photoPath);
                    }
                }

                TempData["success"] = "Product Deleted Successfully!";
                return RedirectToAction("Index");
            }
            return View(id);
        }

        public string UploadProductPhoto(IFormFile? file, string? oldPhotoLink)
        {
            if (file == null)
                if (oldPhotoLink == null)
                    return "";
                else
                    return oldPhotoLink;

            //upload profile image
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = Path.Combine(filePath, fileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));

            //delete old profile image
            if (oldPhotoLink != null)
            {
                string oldPhotoPath = Path.Combine(filePath, oldPhotoLink);
                if (System.IO.File.Exists(oldPhotoPath))
                {
                    Thread.Sleep(1000);
                    System.IO.File.Delete(oldPhotoPath);
                }

            }
            return fileName;
        }

    }
}
