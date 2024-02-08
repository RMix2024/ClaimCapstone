using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E85Carbs.WebServer.Data;
using Microsoft.AspNetCore.Http;

namespace E85Carbs.WebServer.Models
{
    public class AddShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public System.DateTime DateCreated { get; set; }


        [Required]
        public string NormalizedUserName { get; set; }


        [Required]
        public int CartItemId { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public ShoppingCart ToShoppingCart()
        {
            return new ShoppingCart
            {
                ShoppingCartId = ShoppingCartId,
                DateCreated = DateCreated,
                NormalizedUserName = NormalizedUserName,               
                CartItems = CartItems
            };
        }
    }
}
