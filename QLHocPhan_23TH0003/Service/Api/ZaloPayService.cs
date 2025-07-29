
using RestSharp;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace QLHocPhan_23TH0003.Service.Api
{
    public class ZaloPayService
    {
        private readonly IConfiguration _config;

        public ZaloPayService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> CreateOrderAsync(decimal amount, string orderId)
        {
            string appId = _config["ZaloPay:AppId"];
            string key1 = _config["ZaloPay:Key1"];
            string endpoint = _config["ZaloPay:CreateOrderUrl"];

            string embeddata = "{}";
            string items = "[]";
            string appTransId = DateTime.Now.ToString("yyMMdd") + "_" + orderId;

            var order = new Dictionary<string, string>
        {
            { "app_id", appId },
            { "app_trans_id", appTransId },
            { "app_user", "testuser" },
            { "app_time", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
            { "amount", ((int)amount).ToString() },
            { "item", items },
            { "description", $"Thanh toán đơn hàng #{orderId}" },
            { "embed_data", embeddata },
            { "bank_code", "" },
            { "callback_url", "https://yourdomain.com/zalo/callback" },
            { "redirect_url", "https://yourdomain.com/zalo/return" }
        };

            // Tạo MAC
            string dataMac = appId + "|" + appTransId + "|" + order["app_user"] + "|" +
                             order["amount"] + "|" + order["app_time"] + "|" + embeddata + "|" + items;
            order.Add("mac", HmacSHA256(dataMac, key1));

            // Gửi request
            var client = new RestClient(endpoint);
            var request = new RestRequest("", Method.Post);
            request.AddJsonBody(order);

            var response = await client.ExecuteAsync(request);
            var json = System.Text.Json.JsonDocument.Parse(response.Content);
            var orderUrl = json.RootElement.GetProperty("order_url").GetString();

            return orderUrl;
        }

        private string HmacSHA256(string message, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using var hmacsha256 = new HMACSHA256(keyBytes);
            var hashMessage = hmacsha256.ComputeHash(messageBytes);
            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}
