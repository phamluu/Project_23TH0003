using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_23TH0003.Models
{
    public class EnrollmentViewModel : Enrollment
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
                result = Math.Round(result ?? 0m, 2);
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
                return totalCredits > 0 ? Math.Round(totalPoints / totalCredits, 2) : (decimal?)null;
            }
        }

        public Nullable<decimal> DiemTBTichLuy { get; set; }
    }

    #region Màn hình xem điềm của toàn thể sinh viên group theo năm học, group theo học kì
    // Group theo năm học: Màn hình có các tab theo năm học, mỗi tab có group theo học kì, mỗi học kì có danh sách các sinh viên
    // Danh sách năm học
    
        public class XemDiemTheoNamHoc
        {
            public int Year { get; set; }
            public List<XemDiemTheoHocKy> LisDiemHocKy { get; set; }
        }
        
    // Danh sách môn + Danh sách sinh viên theo từng học kỳ
        public class XemDiemTheoHocKy
        {
            public int semester { get; set; }
            public List<Class> ListClass { get; set; }
            public List<XemDiemSinhVien> ListDiemSinhVien { get; set; }
            
        }
        public class XemDiemSinhVien
        {
           public Student student { get; set; }
           public List<DiemViewModel> ListDiem { get; set; }
           public int SoTinChiHocKy { get; set; }
            public decimal? DiemTBHocKy { get {
                decimal totalPoints = 0;
                decimal totalCredits = 0;
                foreach (var diem in ListDiem)
                {
                    if (diem.DiemHP.HasValue)
                    {
                        int Credit = diem.Class.Cours.Credits;
                        totalPoints += diem.DiemHP.Value * Credit;
                        totalCredits += Credit;
                    }
                }
                return totalCredits > 0 ? Math.Round(totalPoints / totalCredits, 2) : (decimal?)null;
            } }
           public decimal? DiemTBTichLuy { get; set; }
        }
    #endregion

    #region Màn hình xem điểm chi tiết cho từng sinh viên
    // Xem và in bảng điểm chi tiết theo học kì cho sinh viên
    public class XemDiemSinhVienTheoHocKy
        {
            public int StudentId { get; set; }
            public string HocKyNamHoc { get; set; }
            public List<DiemViewModel> ListDiem { get; set; }
            public int SoTinChiHocKy
            {
                get
                {
                    return this.ListDiem.Sum(x => x.Class.Cours.Credits);
                }
            }
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
                            int Credit = diem.Class.Cours.Credits;
                            totalPoints += diem.DiemHP.Value * Credit;
                            totalCredits += Credit;
                        }
                    }
                    return totalCredits > 0 ? Math.Round(totalPoints / totalCredits, 2) : (decimal?)null;
                }
            }
        }
        // Xem và in bảng điểm tổng kết cho tất cả học kỳ cho sinh viên
        public class XemDiemTongKet
        {

            public int StudentId { get; set; }
            public List<XemDiemSinhVienTheoHocKy> ListDiemHocKy { get; set; }
            public XemDiemTongKet()
            {
                ListDiemHocKy = new List<XemDiemSinhVienTheoHocKy>();
            }
            public int SoTinChiTichLuy
            {
                get
                {
                    return this.ListDiemHocKy.Sum(x => x.SoTinChiHocKy);
                }
            }
            public decimal? DiemTBTichLuy
            {
                get
                {
                    decimal totalPoints = 0;
                    decimal totalCredits = 0;
                    foreach (var hk in ListDiemHocKy)
                    {
                        foreach (var diem in hk.ListDiem)
                        {
                            if (diem.DiemHP.HasValue)
                            {
                                int Credit = diem.Class.Cours.Credits;
                                totalPoints += diem.DiemHP.Value * Credit;
                                totalCredits += Credit;
                            }
                        }
                    }
                    return totalCredits > 0 ? Math.Round(totalPoints / totalCredits, 2) : (decimal?)null;
                }
            }
        }
    #endregion

}