using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhiKapStudyHours;

namespace jacksvoboda.com.Pages.HCI
{
    public class StudyHoursModel : PageModel
    {
        //need a way to get back a list of students to display
        [BindProperty]
        public List<string> Students { get; set; }
        //DataRefrence is like our personal buttler who gets stuff from sql for us
        public IStudyData DataRefrence = new IStudyData();

        //model binding an entry because I'm lazy
        [BindProperty]
        public Entry entry { get; set; }

        [BindProperty]
        public string user { get; set; }

        [BindProperty]
        public DateTime date { get; set; }

        [BindProperty]
        public string title { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Edit") == "No")
            {
                Students = DataRefrence.get_students();
                user = HttpContext.Session.GetString("CurrentUser");
                date = DateTime.Today;
                entry = new Entry();
                entry.Proctor = user;
                entry.Date = DateTime.Today;
                title = "Create a new Entry";
                return this.Page();
            }
            else
            {
                var id = Int32.Parse(HttpContext.Session.GetString("Id"));
                entry = DataRefrence.get_entry_by_id(id);
                Students = DataRefrence.get_students();
                user = HttpContext.Session.GetString("CurrentUser");
                date = DateTime.Today;
                title = "Edit an existing Entry";
                return this.Page();
            }
        }

        public IActionResult OnPost()
        {
            entry.Proctor = HttpContext.Session.GetString("CurrentUser");
            Console.WriteLine(entry.Date);
            DataRefrence.create_entry(entry);
            var role = DataRefrence.get_role(HttpContext.Session.GetString("CurrentUser"));
            role = role.Trim();
            Console.WriteLine(role);
            return RedirectToPage("./" + role);
        }
        //also need a way to add new entry to database
    }
}
