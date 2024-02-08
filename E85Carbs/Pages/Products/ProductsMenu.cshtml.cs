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


namespace E85Carbs.WebServer.Pages.Products
{
    [Authorize("AdminOnly")]
    public class ProductsMenuModel : PageModel
    {
        [BindProperty]
        public List<Product> Products { get; set; }

        private readonly ProductService _service;
        private readonly ILogger<ProductsMenuModel> _logger;
        public ApplicationDbContext _context;
        public ProductsMenuModel(ProductService service, ILogger<ProductsMenuModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Products = _service.GetAllProducts();
        }
    }
}
