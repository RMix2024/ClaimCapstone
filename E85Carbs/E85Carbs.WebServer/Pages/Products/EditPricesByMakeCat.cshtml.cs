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

namespace E85Carbs.WebServer.Pages.Products
{
    [Authorize("AdminOnly")]
    public class EditPricesByMakeCatModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int productid { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public string makeName { get; set; }

        [BindProperty]
        public string categoryName { get; set; }

        [BindProperty]
        public double percentAdjust { get; set; }

        public List<SelectListItem> CategoryDropdownList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MakeDropdownList { get; set; } = new List<SelectListItem>();

        private readonly ProductService _service;
        private readonly CategoryService _categoryservice;
        private readonly MakeService _makeservice;
        private readonly ILogger<EditPricesByMakeCatModel> _logger;
        public ApplicationDbContext _context;
        public EditPricesByMakeCatModel(ProductService service, ILogger<EditPricesByMakeCatModel> logger, ApplicationDbContext context, CategoryService categoryservice, MakeService makeservice)
        {
            _service = service;
            _logger = logger;
            _context = context;
            _makeservice = makeservice;
            _categoryservice = categoryservice;
        }
        public void OnGet()
        {
            CategoryDropdownList = _categoryservice.CategoryDropDownList();
            CategoryDropdownList.Insert(0, new SelectListItem("Select one", null, false));
            MakeDropdownList = _makeservice.MakeDropDownList();
            MakeDropdownList.Insert(0, new SelectListItem("Select one", null, false));
            Products = _service.GetAllProducts();
        }

        public IActionResult OnPost()
        {
            if (makeName != "Select one" && categoryName == "Select one")                
            {
                _service.UpdatePricingByMake(makeName, percentAdjust);
                return RedirectToPage("../Products/EditPricesByMakeCat");
            }

            else if (categoryName != "Select one" && makeName == "Select one")
            {
                _service.UpdatePricingByCategory(categoryName, percentAdjust);
                return RedirectToPage("../Products/EditPricesByMakeCat");
            }
            else if (makeName != "Select one" && categoryName != "Select one")
            {
                _service.UpdatePricingByMakeCat(categoryName, makeName, percentAdjust);
                return RedirectToPage("../Products/EditPricesByMakeCat");
            }
            else
            {
                return RedirectToPage("../Products/EditPricesByMakeCat");
            }            
        }
    }
}
