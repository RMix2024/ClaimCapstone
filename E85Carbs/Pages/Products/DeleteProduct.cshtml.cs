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
    public class DeleteProductModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public int productid { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }

        private readonly ProductService _service;
        private readonly ILogger<DeleteProductModel> _logger;
        public ApplicationDbContext _context;
        public DeleteProductModel(ProductService service, ILogger<DeleteProductModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
            product = _service.GetAProduct(id);
        }

        public IActionResult OnPostDelete(int id)
        {
            var product = _context.Products.Find(id);           
            _context.Remove(product);
            _context.SaveChanges();
            return Redirect("../ProductsMenu");
        }
    }
}
