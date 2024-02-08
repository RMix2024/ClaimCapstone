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


namespace E85Carbs.WebServer.Pages.Makes
{
    [Authorize("AdminOnly")]

    public class DeleteMakeModel : PageModel
    {
        [BindProperty]
        public Make make  { get; set; }

        [BindProperty]
        public int makeid { get; set; }

        [BindProperty]
        public List<Make> Makes { get; set; }

        private readonly MakeService _service;
        private readonly ILogger<DeleteMakeModel> _logger;
        public ApplicationDbContext _context;
        public DeleteMakeModel(MakeService service, ILogger<DeleteMakeModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
            make = _service.GetAMake(id);
        }

        public IActionResult OnPostDelete(int id)
        {
            var make = _context.Makes.Find(id);
            if (make == null)
            {
                return NotFound();
            }
            _context.Remove(make);
            _context.SaveChanges();
            return Redirect("../MakesMenu");
        }
    }
}
