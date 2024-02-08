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
    public class AddProductGalleryImage
    {

        [Key]
        public int ProductGalleryImageId { get; set; }

        [Required]
        public string ProductGalleryImageName { get; set; }
       
        public int ProductId { get; set; }

        public IFormFile ProductGalleryByte { get; set; }

  

        public ProductGalleryImage ToProductGalleryImage()
        {
            return new ProductGalleryImage
            {
                ProductGalleryImageName = ProductGalleryImageName,
                ProductGalleryImageId = ProductGalleryImageId,
                ProductGalleryByte = ToByteProductGalleryImage(),
                ProductId = ProductId

            };
        }
        public byte[] ToByteProductGalleryImage()
        {
            IFormFile file = this.ProductGalleryByte;
            long length = file.Length;
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            fileStream.Read(bytes, 0, (int)file.Length);
            return bytes;
        }

    }
}
