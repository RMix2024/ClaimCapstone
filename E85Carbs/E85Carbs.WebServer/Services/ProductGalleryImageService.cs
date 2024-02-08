using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Models;
using Microsoft.Extensions.Logging;

namespace E85Carbs.WebServer.Services
{
    public class ProductGalleryImageService
    {

        readonly ApplicationDbContext _context;
        readonly ILogger _logger;

        public ProductGalleryImageService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            this._logger = factory.CreateLogger<ProductGalleryImageService>();
        }

        public ProductGalleryImage AddNewProductGalleryImage(AddProductGalleryImage name)
        {
            var productGalleryImage = name.ToProductGalleryImage();
            _context.Add(productGalleryImage);
            _context.SaveChanges();
            return productGalleryImage;
        }

        public List<ProductGalleryImage> GetAllFilteredProductGalleryImages(int id)
        {
            List<ProductGalleryImage> productGalleryImages = new List<ProductGalleryImage>();
            var productGalleryImageList = _context.productGalleryImages
               .Where(x => x.ProductId == id);
            foreach (var productGalleryImage in productGalleryImageList)
            {
                productGalleryImages.Add(productGalleryImage);
            }
            return productGalleryImages;
        }

        public ProductGalleryImage GetAProductGalleryImage(int id)
        {
            return this._context.productGalleryImages.Where(x => x.ProductGalleryImageId == id).FirstOrDefault();
        }       

    }
}
