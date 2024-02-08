using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Services;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace E85Carbs.WebServer.Pages.Categories
{
    [Authorize("AdminOnly")]
    public class AddCategoryModel : PageModel
    {
        [BindProperty]
        public AddCategory Input { get; set; }

        private readonly CategoryService _service;

        private readonly ILogger<AddCategoryModel> _logger;

        public AddCategoryModel(ILogger<AddCategoryModel> logger, CategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public void OnGet()
        {
            Input = new AddCategory();
        }

        public IActionResult OnPost()
        {
            _service.AddNewCategory(Input);
            return Redirect("./CategoryMenu");
        }
    }
}
