using ECommStoreWeb.Data;
using ECommStoreWeb.Models;

namespace ECommStoreWeb.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context=context;
        }



        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Where(x=>x.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void EditProduct(Product product)
        {
            Product p = _context.Products.Where(x=>x.ProductId == product.ProductId).FirstOrDefault();
            p.ProductName = product.ProductName;
            p.ProductCategory = product.ProductCategory;
            p.Description = product.Description;
            p.Price = product.Price;
            p.StockQuantity = product.StockQuantity;

            _context.Products.Update(p);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(x=>x.ProductId == id).FirstOrDefault();
        }
    }
}
