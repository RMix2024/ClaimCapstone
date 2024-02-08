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
    public class ProductGalleryImage
    {

        [Key]
        public int ProductGalleryImageId { get; set; }

        [Required]
        public string ProductGalleryImageName { get; set; }       

        [Required]
        public byte[] ProductGalleryByte { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public string ProductGalleryBase64Image
        {
            get
            {
                if (this.ProductGalleryByte == null)
                {
                    return String.Empty;
                }
                return Convert.ToBase64String(this.ProductGalleryByte);
            }
        }

    }
}
