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
    public class BuildGalleryImagesMenuModel : PageModel
    {
        [BindProperty]
        public List<BuildGalleryImage> BuildGalleryImages { get; set; }

        [BindProperty]
        public CustomerBuild customerBuild { get; set; }

        private readonly BuildGalleryImageService _buildGalleryImageService;
        private readonly CustomerBuildsService _customerBuildsService;

        private readonly ILogger<BuildGalleryImagesMenuModel> _logger;
        public ApplicationDbContext _context;
        public BuildGalleryImagesMenuModel(BuildGalleryImageService buildGalleryImageService, ILogger<BuildGalleryImagesMenuModel> logger, ApplicationDbContext context, CustomerBuildsService customerBuildsService)
        {
            _buildGalleryImageService = buildGalleryImageService;
            _logger = logger;
            _context = context;
            _customerBuildsService = customerBuildsService;
        }

        public void OnGet(int id)
        {
            customerBuild = _customerBuildsService.GetACustomerBuild(id);
            BuildGalleryImages = _buildGalleryImageService.GetAllFilteredBuildGalleryImages(id);
        }
    }
}
