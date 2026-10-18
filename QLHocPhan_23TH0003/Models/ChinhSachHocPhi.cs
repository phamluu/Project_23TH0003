namespace QLHocPhan_23TH0003.Models
{
    public class ChinhSachHocPhi
    {
        public int Id { get; set; }
        public int IdHocKy { get; set; }
        public decimal? DonGiaTinChi { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public HocKy HocKy { get; set; } = null;
    }
}
