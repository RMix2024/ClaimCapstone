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


namespace E85Carbs.WebServer.Pages.Products
{
    [Authorize("AdminOnly")]
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public AddProduct Input { get; set; }


        private readonly ProductService _productservice;

        private readonly CategoryService _categoryservice;

        private readonly MakeService _makeservice;

        private readonly ApplicationDbContext _context;


        private readonly ILogger<AddProductModel> _logger;

        public List<SelectListItem> CategoryDropdownList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MakeDropdownList { get; set; } = new List<SelectListItem>();



        public AddProductModel(ILogger<AddProductModel> logger, ProductService service, MakeService makeService, CategoryService categoryService, ApplicationDbContext context)
        {
            _logger = logger;
            _productservice = service;
            _makeservice = makeService;
            _categoryservice = categoryService;
            _context = context;
        }

        public void OnGet()
        {
            Input = new AddProduct();
            MakeDropdownList = _makeservice.MakeDropDownList();
            CategoryDropdownList = _categoryservice.CategoryDropDownList();
        }

        public IActionResult OnPost()
        {           
            _productservice.AddNewProduct(Input);          
            return Redirect("./ProductsMenu");            
        }
    }
}
