using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Services;
using E85Carbs.WebServer.Data;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Pages.ProductGalleryImages
{
    [Authorize("AdminOnly")]
    public class ProductGalleryImagesMenuModel : PageModel
    {
        [BindProperty]
        public List<ProductGalleryImage> ProductGalleryImages { get; set; }

        [BindProperty]
        public Product product { get; set; }

        private readonly ProductGalleryImageService _service;
        private readonly ProductService _productService;

        private readonly ILogger<ProductGalleryImagesMenuModel> _logger;
        public ApplicationDbContext _context;
        public ProductGalleryImagesMenuModel(ProductGalleryImageService service, ILogger<ProductGalleryImagesMenuModel> logger, ApplicationDbContext context, ProductService productService)
        {
            _service = service;
            _logger = logger;
            _context = context;
            _productService = productService;
        }

        public void OnGet(int id)
        {
            product = _productService.GetAProduct(id);
            ProductGalleryImages = _service.GetAllFilteredProductGalleryImages(id);
        }
    }
}
