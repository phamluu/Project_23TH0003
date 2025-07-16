﻿namespace QLHocPhan_23TH0003.Service.Api
{
    public class VietQRPaymentService
    {
        public string PaymentQRCode(int AMOUNT, string INFO)
        {
            string BANK = "ACB";
            string ACCOUNT = "250830909";
            return "https://img.vietqr.io/image/" + $"{BANK}-{ACCOUNT}-compact2.png?amount={AMOUNT}&addInfo={INFO}";
        }
    }
}
