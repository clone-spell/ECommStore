using ECommStoreWeb.Data;
using ECommStoreWeb.Models;

namespace ECommStoreWeb.Repository
{
    public interface IRepository
    {
        //products
        Product GetProduct(int id);
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int id);

        //categories
        Category GetCategory(int id);
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);

        //sub-categories
        SubCategory GetSubCategory(int id);
        IEnumerable<SubCategory> GetAllSubCategories();
        void AddSubCategory(SubCategory subCategory);
        void EditSubCategory(SubCategory SubCategory);
        void DeleteSubCategory(int id);

        //cart items
        void AddToCart(Cart cart);
        void RemoveFromCart(Cart cart);
        void EditCart(Cart cart);
        IEnumerable<Cart> GetAllCartItemByUser(string id);
        Cart GetCartItem(int productId, string userId);
        Cart GetCartItemById(int id);

        //buy item
        void AddToOrder(Order order);
    }
}
