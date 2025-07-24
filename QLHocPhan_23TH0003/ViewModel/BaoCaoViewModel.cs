namespace QLHocPhan_23TH0003.ViewModel
{
    public class BaoCaoSinhVienViewModel
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string Khoa { get; set; }
        public int SLNam { get; set; }
        public int SLNu { get; set; }
        public int TongSV { get; set; }

    }

    public class BaoCaoHocPhanViewModel
    {
        public string? MaHocPhan { get; set; }
        public string? TenHocPhan { get; set; }
        public int? SoTinChi { get; set; }
        public string? TenKhoa { get; set; }
        public string? HocKy { get; set; }
        public string? NamHoc { get; set; }
        public int SoLopMo { get; set; }
        public int TongSV { get; set; }
    }

    public class BaoCaoKetQuaHocTapViewModel
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string TenLop { get; set; }
        public string TenMon { get; set; }
        public double Diem { get; set; }
        public string DiemChu { get; set; }
        public string XepLoai { get; set; }
    }

    public class BaoCaoHocPhiViewModel
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string TenLop { get; set; }
        public string HocKy { get; set; }
        public string NamHoc { get; set; }
        public decimal TongHocPhi { get; set; }
        public decimal DaDong { get; set; }
        public decimal ConNo => TongHocPhi - DaDong;
    }

}
