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
    public class AddProductGalleryImageModel : PageModel
    {
        [BindProperty]
        public ProductGalleryImage ProductGalleryImage { get; set; }

        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public AddProductGalleryImage Input { get; set; }

        [BindProperty]
        public int productid { get; set; }


        private readonly ProductGalleryImageService _service;
        private readonly ProductService _productService;

        private readonly ILogger<AddProductGalleryImageModel> _logger;
        public ApplicationDbContext _context;
        public AddProductGalleryImageModel(ProductGalleryImageService service, ILogger<AddProductGalleryImageModel> logger, ApplicationDbContext context, ProductService productService)
        {
            _service = service;
            _logger = logger;
            _context = context;
            _productService = productService;
        }

        public void OnGet(int id)
        {
            Input = new AddProductGalleryImage();
            productid = id;
            product = _productService.GetAProduct(id);
          
        }

        public IActionResult OnPost()
        {
            Input.ProductId = productid;
            _service.AddNewProductGalleryImage(Input);
            return RedirectToPage("/ProductGalleryImages/ProductGalleryImagesMenu", new { id = ProductGalleryImage.ProductId});
        }
    }
}
