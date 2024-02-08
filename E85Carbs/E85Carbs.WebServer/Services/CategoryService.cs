using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E85Carbs.WebServer.Services
{
    public class CategoryService
    {
        public List<Category> Categories { get; set; }

        readonly ApplicationDbContext _context;

        readonly ILogger _logger;

        public CategoryService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            _logger = factory.CreateLogger<CategoryService>();
        }
        public void AddNewCategory(AddCategory name)
        {
            var category = name.ToCategory();
            _context.Add(category);
            _context.SaveChanges();
        }
        public List<Category> GetAllCategories()
        {
            var categoryList = _context.Categories.ToList();
            return categoryList;
        }

        public Category GetACategory( int id)
        {
            return this._context.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
        }
        public List<SelectListItem> CategoryDropDownList()
        {
            List<SelectListItem> DropDownList = new List<SelectListItem>();
            Categories = GetAllCategories();
            foreach (var category in Categories)
            {
                DropDownList.Add(new SelectListItem { Value = category.CategoryName, Text = category.CategoryName });
            }
            return DropDownList;
        }
    }
}
