using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhiKapStudyHours.Pages.HCI
{
    public class AssociateModel : PageModel
    {
        [BindProperty]
        public double hours { get; set; }

        [BindProperty]
        public double percent_hours { get; set; }

        [BindProperty]
        public string width_hours { get; set; }

        public IStudyData DataRefrence = new IStudyData();

        [BindProperty]
        public List<Entry> entries { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "Yes")
            {
                var role = DataRefrence.get_role(HttpContext.Session.GetString("CurrentUser"));
                role = role.Trim();
                Console.WriteLine(role);
                if (role == "Associate")
                {
                    hours = DataRefrence.get_hours_for_student(HttpContext.Session.GetString("CurrentUser"));
                    if (hours > 8) { hours = 8; }
                    percent_hours = hours / 8;
                    width_hours = percent_hours * 100 + "%";

                    var user = HttpContext.Session.GetString("CurrentUser");
                    entries = DataRefrence.get_entries_by_student(user);
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
