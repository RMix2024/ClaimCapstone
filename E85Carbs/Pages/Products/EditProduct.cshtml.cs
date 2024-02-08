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

    public class EditProductModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int productid { get; set; }

        [BindProperty]
        public List<Product> products { get; set; }

        public List<SelectListItem> CategoryDropdownList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MakeDropdownList { get; set; } = new List<SelectListItem>();

        private readonly ProductService _service;
        private readonly ILogger<EditProductModel> _logger;
        public ApplicationDbContext _context;
        public EditProductModel(ProductService service, ILogger<EditProductModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            Product = _context.Products.Find(id);
            if (id == null)
            {
                return NotFound();
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {

            _service.UpdateProduct(Product.ProductId, Product.ProductName, Product.ProductPrice, Product.ProductDescription);

            return Redirect("../ProductsMenu");

        }
    }
}
