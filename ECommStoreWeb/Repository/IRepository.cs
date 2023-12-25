using ECommStoreWeb.Data;
using ECommStoreWeb.Models;

namespace ECommStoreWeb.Repository
{
    public interface IRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int id);
    }
}
