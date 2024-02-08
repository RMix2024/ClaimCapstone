using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Models;
using Microsoft.Extensions.Logging;

namespace E85Carbs.WebServer.Services
{
    public class BuildGalleryImageService
    {
        readonly ApplicationDbContext _context;
        readonly ILogger _logger;

        public BuildGalleryImageService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            this._logger = factory.CreateLogger<BuildGalleryImageService>();
        }

        public BuildGalleryImage AddNewBuildGalleryImage(AddCustomerBuildGalleryImage name)
        {
            var buildGalleryImage = name.ToBuildGalleryImage();
            _context.Add(buildGalleryImage);
            _context.SaveChanges();
            return buildGalleryImage;
        }

        public List<BuildGalleryImage> GetAllFilteredBuildGalleryImages(int id)
        {
            List<BuildGalleryImage> buildGalleryImages = new List<BuildGalleryImage>();
            var buildGalleryImageList = _context.buildGalleryImages
               .Where(x => x.CustomerBuildId == id);
            foreach (var buildGalleryImage in buildGalleryImageList)
            {
                buildGalleryImages.Add(buildGalleryImage);
            }
            return buildGalleryImages;
        }

        public BuildGalleryImage GetABuildGalleryImage(int id)
        {
            return this._context.buildGalleryImages.Where(x => x.BuildGalleryImageId == id).FirstOrDefault();
        }

    }
}
