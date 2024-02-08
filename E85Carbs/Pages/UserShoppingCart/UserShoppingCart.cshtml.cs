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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace E85Carbs.WebServer.Pages.UserShoppingCart
{
    [Authorize]
    public class UserShoppingCartModel : PageModel
    {
        [BindProperty]
        public string NormalizedUserName { get; set; }

        [BindProperty]
        public List<CartItem> CartItems { get; set; } = new List<CartItem> { };
        
        [BindProperty]
        public CartItem item { get; set; }
      
        public double Total { get; set; } = 0;

        public ShoppingCart shoppingCart { get; set; }

        [BindProperty]
        public CartItem cartItem { get; set; }

        private readonly ShoppingCartService _shoppingCartService;
        private readonly CartItemService _cartItemService;
        private readonly ProductService _productService;

        private readonly ILogger<UserShoppingCartModel> _logger;
        public ApplicationDbContext _context;

        public UserShoppingCartModel(ShoppingCartService shoppingCartService, CartItemService cartItemService, ILogger<UserShoppingCartModel> logger, ApplicationDbContext context, ProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _cartItemService = cartItemService;
            _productService = productService;
            _logger = logger;
            _context = context;

        }
        public IActionResult OnGet()
        {
           
          ShoppingCart ThisCart = _shoppingCartService.GetAShoppingCart(User.Identity.Name);
            if(ThisCart != null)
            {
                CartItems = ThisCart.CartItems;
                
                foreach(var item in CartItems)
                {
                    Total = Total + (item.Product.ProductPrice * item.Quantity);

                }
            }
          
           
            return Page();            
        }
        
        public IActionResult OnPost(int id)
        {
            ShoppingCart currentCart = _shoppingCartService.GetAShoppingCart(User.Identity.Name);
            CartItem cartItem = _cartItemService.GetACartItemByProductId(id);
            if (currentCart != null && cartItem != null)
            {
                _cartItemService.UpdateAdd1(id);
                return RedirectToPage("/UserShoppingCart/UserShoppingCart");
            }

            else if(currentCart == null)
            {
                currentCart = new ShoppingCart
                {
                   
                    DateCreated = System.DateTime.Now,
                    NormalizedUserName = User.Identity.Name,
                    CartItems = CartItems

                };
                CartItem newCartItem = new CartItem
                {
                    ProductId = id,
                    Quantity = 1,
                    DateCreated = System.DateTime.Now
                };
                
                currentCart.CartItems.Add(newCartItem);
                _shoppingCartService.AddNewShoppingCart(currentCart);
            }            
            else
            {
                
                CartItem newCartItem = new CartItem
                {
                    ProductId = id,
                    Quantity = 1,
                    DateCreated = System.DateTime.Now,
                    ShoppingCartId = currentCart.ShoppingCartId
                   
                };
                _cartItemService.AddNewCartItem(newCartItem);
                
            }

            return RedirectToPage("/UserShoppingCart/UserShoppingCart");
        }



        public IActionResult OnGetDeleteFromCart(int deleteid)
        {
            _cartItemService.DeleteACartItem(deleteid);
            return RedirectToPage("/UserShoppingCart/UserShoppingCart");
        }

        public IActionResult OnPostUpdateCartItem()
        {
            if(item.Quantity > 1)
            {
                _cartItemService.UpdateQuantity(item.CartItemId, item.Quantity);
                return RedirectToPage("/UserShoppingCart/UserShoppingCart");

            }
            else
            {
                _cartItemService.DeleteACartItem(item.CartItemId);
               
                return RedirectToPage("/UserShoppingCart/UserShoppingCart");
            }                        
        }
    }
}

