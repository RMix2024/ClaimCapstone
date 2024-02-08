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

namespace E85Carbs.WebServer.Pages.BuildGalleryImages
{
    [Authorize("AdminOnly")]
    public class DeleteBuildGalleryImageModel : PageModel
    {

        [BindProperty]
        public BuildGalleryImage BuildGalleryImage { get; set; }

        private readonly BuildGalleryImageService _buildGalleryImageService;
        private readonly ILogger<DeleteBuildGalleryImageModel> _logger;
        public ApplicationDbContext _context;
        public DeleteBuildGalleryImageModel(BuildGalleryImageService buildGalleryImageService, ILogger<DeleteBuildGalleryImageModel> logger, ApplicationDbContext context)
        {
            _buildGalleryImageService = buildGalleryImageService;
            _logger = logger;
            _context = context;
        }
        public void OnGet(int id)
        {
            BuildGalleryImage = _buildGalleryImageService.GetABuildGalleryImage(id);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var buildGalleryImage = await _context.buildGalleryImages.FindAsync(id);
            if (buildGalleryImage == null)
            {
                return RedirectToPage("/BuildGalleryImages/BuildGalleryImagesMenu", new { id = buildGalleryImage.BuildGalleryImageId });
            }
            _context.Remove(buildGalleryImage);
            await _context.SaveChangesAsync();
            return RedirectToPage("/BuildGalleryImages/BuildGalleryImagesMenu", new { id = buildGalleryImage.CustomerBuildId });
        }
    }
}
