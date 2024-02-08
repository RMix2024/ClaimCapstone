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
    public class BuildGalleryImage
    {
        [Key]
        public int BuildGalleryImageId { get; set; }

        [Required]
        public string BuildGalleryImageName { get; set; }

        [Required]
        public byte[] BuildGalleryByte { get; set; }

        [ForeignKey("CustomerBuildId")]
        public int CustomerBuildId { get; set; }
        public CustomerBuild CustomerBuild { get; set; }

        [NotMapped]
        public string BuildGalleryBase64Image
        {
            get
            {
                if (this.BuildGalleryByte == null)
                {
                    return String.Empty;
                }
                return Convert.ToBase64String(this.BuildGalleryByte);
            }
        }
    }
}
