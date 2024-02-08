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


namespace E85Carbs.WebServer.Pages.Categories
{
    [Authorize("AdminOnly")]
    public class CategoryMenuModel : PageModel
    {
        [BindProperty]
        public List<Category> Categories { get; set; }

        private readonly CategoryService _service;
        private readonly ILogger<DeleteCategoryModel> _logger;
        public ApplicationDbContext _context;
        public CategoryMenuModel(CategoryService service, ILogger<DeleteCategoryModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Categories = _service.GetAllCategories();
        }
    }
}
