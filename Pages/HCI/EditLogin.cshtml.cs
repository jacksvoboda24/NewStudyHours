using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PhiKapStudyHours;

namespace jacksvoboda.com.Pages.Fantasy
{
    public class EditLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        public IStudyData DataRefrence = new IStudyData();

        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("IsLoggedIn") == "Yes")
            {
                return RedirectToPage("./EditLogin");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (DataRefrence.check_login(Username, Password))
            {
                HttpContext.Session.SetString("IsLoggedIn", "Yes");
                HttpContext.Session.SetString("CurrentUser", Username);
                return RedirectToPage("./Associate");
            }
            else
            {
                return RedirectToPage("./EditLogin");
            }
        }
    }
}
