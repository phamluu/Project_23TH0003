namespace QLHocPhan_23TH0003.Service.Api
{
    public class VietQRPaymentService
    {
        public string PaymentQRCode(int AMOUNT, string INFO)
        {
            string BANK = "vietcombank";
            string ACCOUNT = "0061001084333";
            return "https://img.vietqr.io/image/" + $"{BANK}-{ACCOUNT}-compact2.png?amount={AMOUNT}&addInfo={INFO}";
        }
    }
}
