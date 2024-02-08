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
    public class CustomerBuildsModel : PageModel
    {
        [BindProperty]
        public CustomerBuild CustomerBuild { get; set; }

        [BindProperty]
        public int customerBuildid { get; set; }

        [BindProperty]
        public List<CustomerBuild> CustomerBuilds { get; set; }


        private readonly CustomerBuildsService _customerBuildsService;
        private readonly ILogger<CustomerBuildsModel> _logger;
        public ApplicationDbContext _context;
        public CustomerBuildsModel(CustomerBuildsService service, ILogger<CustomerBuildsModel> logger, ApplicationDbContext context)
        {
            _customerBuildsService = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            CustomerBuilds = _customerBuildsService.GetAllCustomerBuilds();
        }
    }
}
