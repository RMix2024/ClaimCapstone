using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace E85Carbs.WebServer.Data
{
    public class Product
    {
        [Key]
        public int ProductId {get; set;}

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public byte[] MainProductImage { get; set; }

        public List<ProductGalleryImage> ProductGalleryImages { get; set; } = new List<ProductGalleryImage>();

        [ForeignKey("CategoryName")]
        public string CategoryName { get; set; }

        [ForeignKey("MakeName")]
        public string MakeName { get; set; }

        [NotMapped]
        public string MainBase64Image
        {
            get
            {
                if (this.MainProductImage == null)
                {
                    return String.Empty;
                }
                return Convert.ToBase64String(this.MainProductImage);
            }
        }
    }
}
