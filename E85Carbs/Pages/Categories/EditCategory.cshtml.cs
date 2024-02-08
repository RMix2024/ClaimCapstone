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

    public class EditCategoryModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public int categoryid { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

        private readonly CategoryService _service;
        private readonly ILogger<EditCategoryModel> _logger;
        public ApplicationDbContext _context;
        public EditCategoryModel(CategoryService service, ILogger<EditCategoryModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            Category = _context.Categories.Find(id);
            if (id == null)
            {
                return NotFound();
            }

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var categoryToUpdate = await _context.Categories.FindAsync(id);
            if (categoryToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Category>(
                categoryToUpdate,
                "Category",
                a => a.CategoryID,
                a => a.CategoryName              
                ))
            {
                await _context.SaveChangesAsync();
                return Redirect("../CategoryMenu");
            }
            return Page();
        }
    }
}
