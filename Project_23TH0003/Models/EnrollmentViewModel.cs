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

    public class DiemViewModel
    {
        public int classId { get; set; }
        public decimal? Midterm { get; set; }
        public decimal? Final { get; set; }
        public decimal? DiemHP {
            get
            {
                decimal? result = this.Midterm * Class.HeSo_Diem1 / 100 + this.Final * Class.HeSo_Diem2 / 100;
                return result;
            }
        }
        public int SoTinChi { get; set; }
        public Class Class { get; set; }
    }
    public class XemDiemViewModel
    {
        public int studentId { get; set; }
        public Student Student { get; set; }
       
        public List<DiemViewModel> ListDiem { get; set; } = new List<DiemViewModel>();
        public decimal? DiemTBHocKy
        {
            get
            {
                decimal totalPoints = 0;
                decimal totalCredits = 0;
                foreach (var diem in ListDiem)
                {
                    if (diem.DiemHP.HasValue)
                    {
                        totalPoints += diem.DiemHP.Value * diem.SoTinChi;
                        totalCredits += diem.SoTinChi;
                    }
                }
                return totalCredits > 0 ? totalPoints / totalCredits : (decimal?)null;
            }
        }

        public Nullable<decimal> DiemTBTichLuy { get; set; }
    }
}