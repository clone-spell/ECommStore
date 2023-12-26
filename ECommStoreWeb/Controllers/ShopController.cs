using ECommStoreWeb.Data;
using ECommStoreWeb.Models;
using ECommStoreWeb.Repository;
using ECommStoreWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommStoreWeb.Controllers
{
    [Authorize(Roles ="customer")]
	public class ShopController : Controller
	{
		private readonly IRepository _repository;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public ShopController(IRepository repository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_repository=repository;
			_signInManager=signInManager;
			_userManager=userManager;
		}

        [AllowAnonymous]
		public IActionResult Index()
		{
			IEnumerable<Product> products = _repository.GetAllProducts();
			return View(products);
		}

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var cartItems = _repository.GetAllCartItemByUser(user.Id);
            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            foreach (var cartItem in cartItems)
            {
                var p = _repository.GetProduct(cartItem.ProductId);
                CartViewModel c = new CartViewModel(cartItem.Id,p, cartItem.Quantity);
                cartViewModel.Add(c);
            }
            return View(cartViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			var user = await _signInManager.UserManager.GetUserAsync(User);
			if(await _userManager.IsInRoleAsync(user, "customer"))
			{
                var cartItems = _repository.GetAllCartItemByUser(user.Id);
                Product p = _repository.GetProduct(id);

                if (cartItems.Any())
                {
                    var cartItem = cartItems.Where(x => x.ProductId == p.ProductId).FirstOrDefault();
                    if(cartItem != null)
                    {
                        cartItem.Quantity += 1;
                        _repository.EditCart(cartItem);
                        TempData["success"] = "Product successfully added to cart!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Cart cart = new Cart()
                        {
                            ProductId = p.ProductId,
                            UserId = user.Id,
                            Quantity = 1
                        };
                        _repository.AddToCart(cart);
                        TempData["success"] = "Product successfully added to cart!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Cart cart = new Cart()
                    {
                        ProductId = p.ProductId,
                        UserId = user.Id,
                        Quantity = 1
                    };
                    _repository.AddToCart(cart);
                    TempData["success"] = "Product successfully added to cart!";
                    return RedirectToAction("Index");
                }
            }
            return BadRequest("user must be customer to use the add to cart option");
		}

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var cart = _repository.GetCartItemById(id);
            if(cart.UserId == user.Id)
            {
                _repository.RemoveFromCart(cart);
                return RedirectToAction("Cart");
            }
            return BadRequest("from : RemoveFromCart\nSomthing is wrong");
        }

		[HttpGet]
		public async Task<IActionResult> Buy(int id, int q)
		{
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var product = _repository.GetProduct(id);
            Order model = new Order();
            model.OrderId = Guid.NewGuid();
            model.ProductId = product.ProductId;
            model.UserId = user.Id;
            model.Quantity = q;
            model.Placed = true;

			return View(model);
		}

        [HttpPost]
        public IActionResult Buy(Order model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddToOrder(model);
                var cartItem = _repository.GetCartItem(model.ProductId, model.UserId);
                if(cartItem != null)
                {
                    _repository.RemoveFromCart(cartItem);
                }
                return RedirectToAction("index", "shop");
            }
            return View(model);
        }
	}
}
