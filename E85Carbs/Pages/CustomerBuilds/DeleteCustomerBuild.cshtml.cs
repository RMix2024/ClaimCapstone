using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E85Carbs.WebServer.Pages.CustomerBuilds
{
    [Authorize("AdminOnly")]
    public class DeleteCustomerBuildModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
