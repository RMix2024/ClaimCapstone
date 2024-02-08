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
    public class MakesMenuModel : PageModel
    {
        [BindProperty]
        public List<Make> Makes { get; set; }

        private readonly MakeService _service;
        private readonly ILogger<MakesMenuModel> _logger;
        public ApplicationDbContext _context;
        public MakesMenuModel(MakeService service, ILogger<MakesMenuModel> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Makes = _service.GetAllMakes();
        }
    }
}
