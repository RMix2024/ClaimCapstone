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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Pages.CustomerBuilds
{
    public class CustomerBuildDetailsModel : PageModel
    {
        [BindProperty]
        public CustomerBuild CustomerBuild { get; set; }

        [BindProperty]
        public int customerBuildid { get; set; }

        [BindProperty]
        public List<CustomerBuild> CustomerBuilds { get; set; }
        [BindProperty]
        public List<BuildGalleryImage> BuildGalleryImages { get; set; }

        private readonly CustomerBuildsService _customerBuildsService;
        private readonly BuildGalleryImageService _buildGalleryImageService;
        private readonly ILogger<CustomerBuildDetailsModel> _logger;
        public ApplicationDbContext _context;
        public CustomerBuildDetailsModel(CustomerBuildsService customerBuildsService, ILogger<CustomerBuildDetailsModel> logger, ApplicationDbContext context, BuildGalleryImageService buildGalleryImageService)
        {
            _customerBuildsService = customerBuildsService;
            _buildGalleryImageService = buildGalleryImageService;
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
            CustomerBuild = _customerBuildsService.GetACustomerBuild(id);
            BuildGalleryImages = _buildGalleryImageService.GetAllFilteredBuildGalleryImages(id);
        }
    }
}
