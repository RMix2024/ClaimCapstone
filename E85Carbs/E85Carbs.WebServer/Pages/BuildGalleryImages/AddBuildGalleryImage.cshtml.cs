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
    public class AddBuildGalleryImageModel : PageModel
    {
        [BindProperty]
        public BuildGalleryImage BuildGalleryImage { get; set; }

        [BindProperty]
        public CustomerBuild CustomerBuild { get; set; }

        [BindProperty]
        public AddCustomerBuildGalleryImage Input { get; set; }

        [BindProperty]
        public int customerbuildid { get; set; }


        private readonly BuildGalleryImageService _buildGalleryImageService;
        private readonly CustomerBuildsService _customerBuildsService;

        private readonly ILogger<AddBuildGalleryImageModel> _logger;
        public ApplicationDbContext _context;
        public AddBuildGalleryImageModel(BuildGalleryImageService buildGalleryImageService, ILogger<AddBuildGalleryImageModel> logger, ApplicationDbContext context, CustomerBuildsService customerBuildsService)
        {
            _buildGalleryImageService = buildGalleryImageService;
            _logger = logger;
            _context = context;
            _customerBuildsService = customerBuildsService;
        }

        public void OnGet(int id)
        {
            Input = new AddCustomerBuildGalleryImage();
            customerbuildid = id;
            CustomerBuild = _customerBuildsService.GetACustomerBuild(id);

        }

        public IActionResult OnPost()
        {
            Input.CustomerBuildId = customerbuildid;
            _buildGalleryImageService.AddNewBuildGalleryImage(Input);
            return RedirectToPage("/BuildGalleryImages/BuildGalleryImagesMenu", new { id = BuildGalleryImage.CustomerBuildId });
        }
    }
}
