using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace QLHocPhan_23TH0003.Service.Api
{
    public class VNPayService
    {
        private readonly IConfiguration _config;

        public VNPayService(IConfiguration config)
        {
            _config = config;
        }

        public string CreatePaymentUrl(HttpContext context, decimal amount, string orderId)
        {
            string vnp_Url = _config["VNPay:vnp_Url"];
            string vnp_ReturnUrl = _config["VNPay:vnp_ReturnUrl"];
            string vnp_TmnCode = _config["VNPay:vnp_TmnCode"];
            string vnp_HashSecret = _config["VNPay:vnp_HashSecret"];

            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                throw new Exception("Thiếu cấu hình VNPay");
            }

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            string ipAddress = context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "127.0.0.1";

            var vnpData = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", ((long)amount * 100).ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", orderId },
                { "vnp_OrderInfo", $"Thanh toan don hang {orderId}" },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_IpAddr", ipAddress },
                { "vnp_CreateDate", vnTime.ToString("yyyyMMddHHmmss") }
            };

            // Tạo chuỗi để ký (KHÔNG được encode)
            string signData = string.Join("&", vnpData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string secureHash = HmacSHA512(vnp_HashSecret, signData);
            vnpData.Add("vnp_SecureHash", secureHash);

            // Tạo query string (CÓ encode)
            var query = new StringBuilder();
            foreach (var kv in vnpData)
            {
                query.Append($"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}&");
            }
            query.Length -= 1; // Xoá ký tự & cuối

            // Debug output
            Console.WriteLine("==== VNPay Request ====");
            Console.WriteLine("SignData: " + signData);
            Console.WriteLine("SecureHash: " + secureHash);
            Console.WriteLine("=======================");

            return $"{vnp_Url}?{query}";
        }

        public static string HmacSHA512(string key, string inputData)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using var hmac = new HMACSHA512(keyBytes);
            byte[] hashValue = hmac.ComputeHash(inputBytes);
            return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        }

        // Xác minh phản hồi từ VNPay
        public bool ValidateResponse(IQueryCollection query)
        {
            string vnp_HashSecret = _config["VNPay:vnp_HashSecret"];
            string vnp_SecureHash = query["vnp_SecureHash"];

            var vnpData = query
                .Where(q => !string.Equals(q.Key, "vnp_SecureHash", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(q.Key, "vnp_SecureHashType", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(k => k.Key, v => v.Value.ToString());

            var sorted = new SortedDictionary<string, string>(vnpData);
            string rawData = string.Join("&", sorted.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string myHash = HmacSHA512(vnp_HashSecret, rawData);

            Console.WriteLine("==== VNPay Response Validation ====");
            Console.WriteLine("RawData: " + rawData);
            Console.WriteLine("VNPay SecureHash: " + vnp_SecureHash);
            Console.WriteLine("Computed Hash: " + myHash);
            Console.WriteLine("===================================");

            return myHash.Equals(vnp_SecureHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
