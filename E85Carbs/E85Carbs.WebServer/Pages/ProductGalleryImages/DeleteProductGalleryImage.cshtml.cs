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
    public class DeleteProductGalleryImageModel : PageModel
    {
        [BindProperty]
        public ProductGalleryImage ProductGalleryImage { get; set; }       

        private readonly ProductGalleryImageService _service;
        private readonly ILogger<DeleteProductGalleryImageModel> _logger;
        public ApplicationDbContext _context;
        public DeleteProductGalleryImageModel(ProductGalleryImageService service, ILogger<DeleteProductGalleryImageModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }
        public void OnGet(int id)
        {
            ProductGalleryImage = _service.GetAProductGalleryImage(id);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var productGalleryImage = await _context.productGalleryImages.FindAsync(id);
            if (productGalleryImage == null)
            {
                return RedirectToPage("/ProductGalleryImages/ProductGalleryImagesMenu", new { id = productGalleryImage.ProductGalleryImageId });
            }
            _context.Remove(productGalleryImage);
            await _context.SaveChangesAsync();
            return RedirectToPage("/ProductGalleryImages/ProductGalleryImagesMenu", new { id = productGalleryImage.ProductId });
        }
    }
}
