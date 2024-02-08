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
    public class AddProduct
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

       public string ProductDescription { get; set; }

        public IFormFile MainImageData { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string MakeName { get; set; }       

        public List<ProductGalleryImage> ProductGalleryImages { get; set; } = new List<ProductGalleryImage>();


        public Product ToProduct()
        {
            return new Product
            {
                ProductName = ProductName,
                ProductPrice = ProductPrice,
                ProductDescription = ProductDescription,
                CategoryName = CategoryName,
                MakeName = MakeName,
                MainProductImage = ToByte()
                
            };
        }
        public byte[] ToByte()
        {
            IFormFile file = this.MainImageData; 
            long length = file.Length; 
            using var fileStream = file.OpenReadStream(); 
            byte[] bytes = new byte[length]; 
            fileStream.Read(bytes, 0, (int)file.Length);
            return bytes;
        }

     
    }
}


//IFormFile file = data.File;
//long length = file.Length;
//if (length < 0) return BadRequest(); 
//using var fileStream = file.OpenReadStream(); 
//byte[] bytes = new byte[length]; 
//fileStream.Read(bytes, 0, (int)file.Length);