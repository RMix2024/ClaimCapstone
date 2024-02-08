using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using E85Carbs.WebServer.Models;
using E85Carbs.WebServer.Services;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Pages.Makes
{
        [Authorize("AdminOnly")]
    public class AddMakeModel : PageModel
    {


        [BindProperty]
            public AddMake Input { get; set; }

            private readonly MakeService _service;

            private readonly ILogger<AddMakeModel> _logger;

            public AddMakeModel(ILogger<AddMakeModel> logger, MakeService service)
            {
                _logger = logger;
                _service = service;
            }

            public void OnGet()
            {
                Input = new AddMake();
            }

            public IActionResult OnPost()
            {
                _service.AddNewMake(Input);
                return Redirect("./MakesMenu");
            }
        }
}
