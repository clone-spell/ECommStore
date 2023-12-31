﻿using ECommStoreWeb.Data;
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


        //products
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
            p.ProductPhotoLink = product.ProductPhotoLink;
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


        //cart items
        public void AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }
        public void RemoveFromCart(Cart cart)
        {
            
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            
        }
        public IEnumerable<Cart> GetAllCartItemByUser(string id)
        {
            var cartItems = _context.Carts.Where(x => x.UserId == id).ToList();
            return cartItems;
        }
        public void EditCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }
        public Cart GetCartItemById(int id)
        {
            return _context.Carts.Where(x => x.Id == id).FirstOrDefault();
        }
        public Cart GetCartItem(int productId, string userId)
        {
            var cartItem = _context.Carts.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            return cartItem;
        }

        //buy item
        public void AddToOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }


        //category
        public Category GetCategory(int id)
        {
            return _context.Categories.Where(x=>x.Id == id).FirstOrDefault();
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void EditCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Where(x=>x.Id == id).FirstOrDefault();
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }


        //sub category
        public SubCategory GetSubCategory(int id)
        {
            return _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
        }
        public IEnumerable<SubCategory> GetAllSubCategories()
        {
            return _context.SubCategories;
        }
        public void AddSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();
        }
        public void EditSubCategory(SubCategory SubCategory)
        {
            _context.SubCategories.Update(SubCategory);
            _context.SaveChanges();
        }
        public void DeleteSubCategory(int id)
        {
            var subCat = _context.SubCategories.Where(x=>x.Id== id).FirstOrDefault();
            if(subCat != null)
            {
                _context.SubCategories.Remove(subCat);
                _context.SaveChanges();
            }
        }
    }
}
