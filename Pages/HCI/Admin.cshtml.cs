using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhiKapStudyHours.Pages.HCI
{
    public class AdminModel : PageModel
    {
        public IStudyData DataRefrence = new IStudyData();

        [BindProperty]
        public List<Entry> entries { get; set; }

        [BindProperty]
        public string user { get; set; }

        [BindProperty]
        public int edit_entry { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "Yes")
            {
                var role = DataRefrence.get_role(HttpContext.Session.GetString("CurrentUser"));
                role = role.Trim();
                Console.WriteLine(role);
                if (role == "Admin")
                {
                    user = HttpContext.Session.GetString("CurrentUser");
                    HttpContext.Session.SetString("Edit", "No");
                    entries = DataRefrence.get_all_entries();
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            string id = edit_entry.ToString();
            Console.WriteLine(id);
            HttpContext.Session.SetString("Edit", "Yes");
            HttpContext.Session.SetString("Id", id);
            return RedirectToPage("./StudyHours");
        }
    }
}
