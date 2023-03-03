using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShop16
{
    public class CourseModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int points { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        [NotMapped]
        public string StartDateShort => start_date.ToShortDateString();

        [NotMapped]
        public string EndDateShort => end_date.ToShortDateString();
        [NotMapped]
        public int DurationInDays => (int)(end_date - start_date).TotalDays;

    }
}
