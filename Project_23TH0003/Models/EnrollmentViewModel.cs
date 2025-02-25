using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_23TH0003.Models
{
    public class EnrollmentViewModel:Enrollment
    {
        public Nullable<decimal> TinhDiemHP
        {
            get
            {
                decimal? result = this.Midterm * Class.HeSo_Diem1 / 100 + this.Final * Class.HeSo_Diem2 / 100;
                return result;
            }
        }
    }
}