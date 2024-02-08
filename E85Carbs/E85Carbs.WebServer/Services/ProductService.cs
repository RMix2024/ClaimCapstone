using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Models;
using Microsoft.Extensions.Logging;

namespace E85Carbs.WebServer.Services
{
    public class ProductService
    {
        readonly ApplicationDbContext _context;
        readonly ILogger _logger;

        public ProductService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            this._logger = factory.CreateLogger<ProductService>();
        }

        public Product AddNewProduct(AddProduct name)
        {
            var product = name.ToProduct();
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts()
        {
            var productList = _context.Products.ToList();
            return productList;
        }

        public Product GetAProduct(int ?id)
        {
            return this._context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }

        public List<Product> GetFilteredProductsByMake(string makename)
        {
            
            return this._context.Products.Where(x => x.MakeName == makename).ToList();
        }

        public List<Product> GetFilteredProductsByCategory(string categoryname)
        {            
            return this._context.Products.Where(x => x.CategoryName == categoryname).ToList();
        }

        public List<Product> GetFilteredProductsByCatMake(string categoryname, string makename)
        {
            return this._context.Products.Where(x => x.CategoryName == categoryname).Where(x => x.MakeName == makename).ToList();             
        }

        public void UpdateProduct(int productid, string productname, double productprice, string productdescription )
        {
            var updateProduct = _context.Products.Find(productid);
            updateProduct.ProductName = productname;
            updateProduct.ProductPrice = productprice;
            updateProduct.ProductDescription = productdescription;
            _context.SaveChanges();

        }

        public void UpdatePricingByMake(string makename, double percentAdjust)
        {
            List<Product> ProductList = GetFilteredProductsByMake(makename);
            foreach (var product in ProductList)
            {
                product.ProductPrice = product.ProductPrice * percentAdjust;
                _context.SaveChanges();
            }
           
        }

        public void UpdatePricingByCategory(string categoryname, double percentAdjust)
        {
            List<Product> ProductList = GetFilteredProductsByCategory(categoryname);
            foreach (var product in ProductList)
            {
                product.ProductPrice = product.ProductPrice * percentAdjust;
                _context.SaveChanges();
            }
        }

        public void UpdatePricingByMakeCat(string categoryname, string makename, double percentAdjust)
        {
            List<Product> ProductList = GetFilteredProductsByCatMake(categoryname, makename);
            foreach (var product in ProductList)
            {
                product.ProductPrice = product.ProductPrice * percentAdjust;
                _context.SaveChanges();

            }
        }


    }
}
