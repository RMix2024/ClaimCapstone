using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Pages.CustomerBuilds
{
    [Authorize("AdminOnly")]
    public class AddCustomerBuildModel : PageModel
    {
        [BindProperty]
        public AddCustomerBuild Input { get; set; }       

        private readonly CustomerBuildsService _customerBuildsService;

        private readonly ApplicationDbContext _context;


        private readonly ILogger<AddCustomerBuildModel> _logger;



        public AddCustomerBuildModel(ILogger<AddCustomerBuildModel> logger, CustomerBuildsService customerBuildsService, ApplicationDbContext context)
        {
            _logger = logger;
            _customerBuildsService = customerBuildsService;        
            _context = context;
        }

        public void OnGet()
        {
            Input = new AddCustomerBuild();         
        }

        public IActionResult OnPost()
        {
            _customerBuildsService.AddNewCustomerBuild(Input);
            return Redirect("./CustomerBuildMenu");
        }




    }
}
