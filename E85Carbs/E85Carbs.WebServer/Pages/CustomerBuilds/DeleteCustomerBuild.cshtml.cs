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

namespace E85Carbs.WebServer.Pages.CustomerBuilds
{
    [Authorize("AdminOnly")]
    public class DeleteCustomerBuildModel : PageModel
    {
        [BindProperty]
        public CustomerBuild customerBuild { get; set; }

        [BindProperty]
        public int customerBuildid { get; set; }

        [BindProperty]
        public List<CustomerBuild> CustomerBuilds { get; set; }

        private readonly CustomerBuildsService _customerBuildsService;
        private readonly ILogger<DeleteCustomerBuildModel> _logger;
        public ApplicationDbContext _context;
        public DeleteCustomerBuildModel(CustomerBuildsService service, ILogger<DeleteCustomerBuildModel> logger, ApplicationDbContext context)
        {
            _customerBuildsService = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
            customerBuild = _customerBuildsService.GetACustomerBuild(id);
        }

        public IActionResult OnPostDelete(int id)
        {
            var customerBuild = _context.customerBuilds.Find(id);
            _context.Remove(customerBuild);
            _context.SaveChanges();
            return Redirect("../CustomerBuildMenu");
        }
    }
}
