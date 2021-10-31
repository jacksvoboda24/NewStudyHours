using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PhiKapStudyHours.Pages.HCI
{
    //name of who's logged in
    //so we can then see who's entered what hours and display them from a list we generate in IStudyData
    // make entries class
    //ability to add to the entries table 
    public class HomeModel : PageModel
    {
        public IStudyData DataRefrence = new IStudyData();

        [BindProperty]
        public List<Entry> entries { get; set; }

        [BindProperty]
        public string user { get; set; }
        public IActionResult OnGet()
        {
            user = HttpContext.Session.GetString("CurrentUser");
            entries = DataRefrence.get_entries_by_user(user);
            return Page();
        }
    }
}
