using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhiKapStudyHours;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }

        public IStudyData DataRefrence = new IStudyData();

        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "Yes")
            {
                var role = DataRefrence.get_role(HttpContext.Session.GetString("CurrentUser"));
                role = role.Trim();
                Console.WriteLine(role);
                return RedirectToPage("./HCI/" + role);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (DataRefrence.check_login(Username, Password))
            {
                HttpContext.Session.SetString("IsLoggedIn", "Yes");
                HttpContext.Session.SetString("CurrentUser", Username);
                Console.WriteLine("success");
                var role = DataRefrence.get_role(Username);
                role = role.Trim();
                Console.WriteLine(role);
                return RedirectToPage("./HCI/" + role);
            }
            else
            {
                return this.OnGet();
            }
        }
    }
}
