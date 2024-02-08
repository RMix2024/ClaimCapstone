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

    public class EditMakeModel : PageModel
    {
        [BindProperty]
        public Make Make { get; set; }

        [BindProperty]
        public int makeid { get; set; }

        [BindProperty]
        public List<Make> Makes { get; set; }

        private readonly MakeService _service;
        private readonly ILogger<EditMakeModel> _logger;
        public ApplicationDbContext _context;
        public EditMakeModel(MakeService service, ILogger<EditMakeModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            Make = _context.Makes.Find(id);
            if (id == null)
            {
                return NotFound();
            }

            if (Make == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var makeToUpdate = await _context.Makes.FindAsync(id);
            if (makeToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Make>(
                makeToUpdate,
                "Make",
                a => a.MakeId,
                a => a.MakeName
                ))
            {
                await _context.SaveChangesAsync();
                return Redirect("../MakesMenu");
            }
            return Page();
        }
    }
}
