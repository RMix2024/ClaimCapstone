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

namespace E85Carbs.WebServer.Pages.Store
{
    public class StoreMainPageModel : PageModel
    {
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public string makeName { get; set; }

        [BindProperty]
        public string categoryName { get; set; }

        private readonly ProductService _service;
        private readonly CategoryService _categoryservice;
        private readonly MakeService _makeservice;
        private readonly ILogger<StoreMainPageModel> _logger;
        public ApplicationDbContext _context;
        public List<SelectListItem> CategoryDropdownList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MakeDropdownList { get; set; } = new List<SelectListItem>();

      
        public StoreMainPageModel(ProductService service, ILogger<StoreMainPageModel> logger, ApplicationDbContext context, CategoryService categoryservice, MakeService makeservice)
        {
            _service = service;
            _makeservice = makeservice;
            _categoryservice = categoryservice;
            _logger = logger;
            _context = context;

        }

        public void OnGet()
        {
            CategoryDropdownList = _categoryservice.CategoryDropDownList();
            CategoryDropdownList.Insert(0, new SelectListItem("Select one", null, false));
            MakeDropdownList = _makeservice.MakeDropDownList();
            MakeDropdownList.Insert(0, new SelectListItem("Select one", null, false));
            Products = _service.GetAllProducts();
        }

        public void OnPost()
        {
            
            if (makeName != "Select one" && categoryName == "Select one")
            {
                Products = _service.GetFilteredProductsByMake(makeName);
            }
            else if (categoryName != "Select one" && makeName == "Select one")
            {
                Products = _service.GetFilteredProductsByCategory(categoryName);
            }
            else if (makeName != "Select one" && categoryName != "Select one")
            {
                Products = _service.GetFilteredProductsByCatMake(categoryName, makeName);
            }
            else
            {
                RedirectToPage("../Store/StoreMainPage");
            }
        }
    }
}
