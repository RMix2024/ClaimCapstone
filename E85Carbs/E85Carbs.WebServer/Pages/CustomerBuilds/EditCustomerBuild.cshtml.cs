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
    [Authorize("AdminOnly")]
    public class EditCustomerBuildModel : PageModel
    {
        [BindProperty]
        public CustomerBuild CustomerBuild { get; set; }

        [BindProperty]
        public int customerBuildid { get; set; }

        [BindProperty]
        public List<CustomerBuild> CustomerBuilds { get; set; }


        private readonly CustomerBuildsService _customerBuildsService;
        private readonly ILogger<EditCustomerBuildModel> _logger;
        public ApplicationDbContext _context;
        public EditCustomerBuildModel(CustomerBuildsService service, ILogger<EditCustomerBuildModel> logger, ApplicationDbContext context)
        {
            _customerBuildsService = service;
            _logger = logger;
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            CustomerBuild = _context.customerBuilds.Find(id);
            if (id == null)
            {
                return NotFound();
            }

            if (CustomerBuild == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {

            _customerBuildsService.UpdateCustomerBuild(CustomerBuild.CustomerBuildId, CustomerBuild.CustomerName, CustomerBuild.CustomerBuildTitle, CustomerBuild.CustomerBuildDescription);

            return Redirect("./CustomerBuildMenu");

        }
    }
}
