using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhiKapStudyHours
{
    public class Entry
    {
        public string Name { get; set; }

        public string Proctor { get; set; }

        public double Hours { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }

        public Entry()
        {

        }
        public Entry (string name, string proctor, double hours, DateTime date, int id)
        {
            this.Name = name;
            this.Proctor = proctor;
            this.Hours = hours;
            this.Date = date;
            this.Id = id;
        }
    }
}
