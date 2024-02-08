using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel;

namespace E85Carbs.WebServer.Data
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public System.DateTime DateCreated { get; set; }    

        [Required]
        [ForeignKey("NormalizedUserName")]
        public string NormalizedUserName { get; set; } 
        
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
