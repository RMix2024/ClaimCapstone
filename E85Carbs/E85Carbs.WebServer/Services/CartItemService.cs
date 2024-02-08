using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Services;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E85Carbs.WebServer.Services
{
    public class CartItemService
    {
        [BindProperty]
        public CartItem cartItem { get; set; }

        readonly ApplicationDbContext _context;
        readonly ILogger _logger;

        public CartItemService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            this._logger = factory.CreateLogger<ShoppingCartService>();
        }

        public CartItem AddNewCartItem(CartItem cartitem)
        {
            
            _context.Add(cartitem);
            _context.SaveChanges();
            return cartitem;
        }


        public CartItem GetACartItem(int cartitemid)
        {
            return this._context.CartItems.Where(x => x.CartItemId == cartitemid).FirstOrDefault();
        }

        public List<CartItem> GetAllCartItems()
        {
            var cartItemList = _context.CartItems.ToList();
            return cartItemList;
        }

        public void DeleteACartItem(int deleteid)
        {
            var cartItem = _context.CartItems.Find(deleteid);
            _context.Remove(cartItem);
            _context.SaveChanges();
        }

        public void UpdateAdd1(int itemId)
        {
            var productitem = _context.Products.Find(itemId);
            CartItem cartItem = _context.CartItems.Where(x => x.ProductId == productitem.ProductId).FirstOrDefault();
            cartItem.Quantity += 1;
            _context.SaveChanges();
        }

        public void UpdateMinus1(int itemId)
        {
            var cartitem = _context.CartItems.Find(itemId);
            cartitem.Quantity -= 1;
            _context.SaveChanges();
        }

        public void UpdateQuantity(int itemId, int updateNum)
        {
            var cartitem = _context.CartItems.Find(itemId);
            cartitem.Quantity = updateNum;
            _context.SaveChanges();

        }

        public CartItem GetACartItemByProductId(int id)
        {
            var productitem = _context.Products.Find(id);
            return _context.CartItems.Where(x => x.ProductId == productitem.ProductId).FirstOrDefault();
        }

        
    }
}
