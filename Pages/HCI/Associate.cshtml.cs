using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void OnGet()
        {
            hours = 8;
            percent_hours = hours / 8;
            width_hours = percent_hours * 100 + "%";
        }
    }
}
