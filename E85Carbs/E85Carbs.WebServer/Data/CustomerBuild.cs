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
    public class CustomerBuild
    {
        [Key]
        public int CustomerBuildId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerBuildTitle { get; set; }

        [Required]
        public string CustomerBuildDescription { get; set; }

        [Required]
        public byte[] MainBuildImage { get; set; }

        public List<BuildGalleryImage> BuildGalleryImages { get; set; } = new List<BuildGalleryImage>();

     

        [NotMapped]
        public string MainBase64Image
        {
            get
            {
                if (this.MainBuildImage == null)
                {
                    return String.Empty;
                }
                return Convert.ToBase64String(this.MainBuildImage);
            }
        }
    }
}
