using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E85Carbs.WebServer.Helpers;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Services;
using E85Carbs.WebServer.Data;
using Microsoft.AspNetCore.Authorization;


namespace E85Carbs.WebServer.Pages.AnonymousShoppingCart
{
    public class ShoppingCartModel : PageModel
    {
        public List<Product> products { get; set; } = new List<Product> { };
        public double Total { get; set; } = 0;

        public Product product { get; set; }

        private readonly ProductService _service;
        private readonly ILogger<ShoppingCartModel> _logger;
        public ApplicationDbContext _context;

        public ShoppingCartModel(ProductService service, ILogger<ShoppingCartModel> logger, ApplicationDbContext context)
        {
            _service = service;          
            _logger = logger;
            _context = context;

        }
        public IActionResult OnGet(int ?id)
        {
            string cartItems; 
            bool cookieCheck =  this.Request.Cookies.TryGetValue("cartItems", out cartItems); // checks for cookie y or n

            if(id != null && id != -1) // product id
            {
                if (cookieCheck)
                {
                    cartItems += "," + id.ToString(); // adding to cart seperating ids with ,
                    //maybe need to look into json conversion
                }
                else
                {
                    cartItems = id.ToString();
                }
                this.Response.Cookies.Append("cartItems", cartItems);
                return RedirectToPage("/AnonymousShoppingCart/ShoppingCart", new { id = -1}); // -1 is to skip adding to cart again
            }

            if (cartItems != null)
            {
                List<string> ProductIds = new List<string> { }; //ids are in cookie add them to list
                if (cartItems.Contains(","))
                {
                    ProductIds = cartItems.Split(',').ToList(); //multiple items
                }
                else
                {
                    ProductIds.Add(cartItems);
                }
                HashSet<string> singleProductIds = new HashSet<string>(ProductIds);
                foreach (var PID in singleProductIds)
                {
                    products.Add(_service.GetAProduct(Int32.Parse(PID)));
                }
            }            
            
            if(products == null)
            {
                Total = 0;
            }
            else
            {
                Total = products.Sum(i => i.ProductPrice);
            }
            return Page();
        }
        //need to be concerned with quatities somewhere




        public IActionResult OnGetDeleteFromCart(int id)
        {
            string cartItems;
            bool cookieCheck = this.Request.Cookies.TryGetValue("cartItems", out cartItems); // checks for cookie y or n
            if (cartItems != null || cartItems != string.Empty)
            {

                if (cartItems.Contains(id.ToString()))
                {
                    
                    int productIndex = cartItems.IndexOf(id.ToString());
                    if (cartItems.Contains(","))
                    {
                        string removedItem;
                        if (productIndex == 0)
                        {
                          removedItem =  cartItems.Remove(productIndex, 2);
                        }
                        else
                        {
                            removedItem = cartItems.Remove(productIndex - 1, 2);
                        }
                        this.Response.Cookies.Append("cartItems", removedItem);
                    }
                    else
                    {
                        this.Response.Cookies.Delete("cartItems");

                    }
                }
            }        

            return RedirectToPage("/AnonymousShoppingCart/ShoppingCart", new { id = -1 });
            
        }

    }
}
