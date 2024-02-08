using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using E85Carbs.WebServer.Helpers;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Services;
using E85Carbs.WebServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace E85Carbs.WebServer.Pages.UserStore
{
    [Authorize]
    public class UserProductDetailsPageModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public List<ProductGalleryImage> ProductGalleryImages { get; set; }

        private readonly ProductService _service;
        private readonly ILogger<UserProductDetailsPageModel> _logger;
        public ApplicationDbContext _context;
        private readonly ProductGalleryImageService _productGalleryImageService;
        public UserProductDetailsPageModel(ProductService service, ILogger<UserProductDetailsPageModel> logger, ApplicationDbContext context, ProductGalleryImageService productGalleryImageService)
        {
            _service = service;
            _productGalleryImageService = productGalleryImageService;
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
            product = _service.GetAProduct(id);
            ProductGalleryImages = _productGalleryImageService.GetAllFilteredProductGalleryImages(id);
        }
    }
}
