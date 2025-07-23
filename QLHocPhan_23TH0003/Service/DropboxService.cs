using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.ViewModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace QLHocPhan_23TH0003.Service
{
    public class DropboxService
    {
        private readonly HttpClient _client;
        private readonly string _accessToken;

        public DropboxService(IHttpClientFactory factory, IConfiguration config)
        {
            _client = factory.CreateClient();
            _accessToken = CauHinhHelper.Get("dropbox_token");
            //_accessToken =  config["Dropbox:AccessToken"]; // Đặt trong appsettings.json
        }

        public async Task<string> UploadAndGetResultStringAsync(IFormFile file)
        {
            var tempPath = Path.GetTempFileName();
            try
            {
                using (var stream = System.IO.File.Create(tempPath))
                {
                    await file.CopyToAsync(stream);
                }

                var dropboxPath = "/Uploads/" + file.FileName;
                return await UploadFileAsync(tempPath, dropboxPath);
            }
            finally
            {
                if (System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Delete(tempPath);
                }
            }
        }
        public async Task<string> UploadFileAsync(string localFilePath, string dropboxPath)
        {
            // 1. Upload file
            var uploadUrl = "https://content.dropboxapi.com/2/files/upload";
            var fileBytes = await File.ReadAllBytesAsync(localFilePath);

            var uploadRequest = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
            uploadRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            uploadRequest.Headers.Add("Dropbox-API-Arg", JsonSerializer.Serialize(new
            {
                path = dropboxPath,
                mode = "add",
                autorename = true,
                mute = false
            }));
            uploadRequest.Content = new ByteArrayContent(fileBytes);
            uploadRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var uploadResponse = await _client.SendAsync(uploadRequest);
            var uploadJson = await uploadResponse.Content.ReadAsStringAsync();

            if (!uploadResponse.IsSuccessStatusCode)
                throw new Exception("Upload thất bại: " + uploadJson);

            // Parse kết quả upload
            using var doc = JsonDocument.Parse(uploadJson);
            var name = doc.RootElement.GetProperty("name").GetString();
            var pathDisplay = doc.RootElement.GetProperty("path_display").GetString();
            var pathLower = doc.RootElement.GetProperty("path_lower").GetString();
            var clientModified = doc.RootElement.GetProperty("client_modified").GetDateTime();
            var size = doc.RootElement.GetProperty("size").GetInt64();

            // 2. Tạo link chia sẻ
            string downloadUrl;
            var shareRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.dropboxapi.com/2/sharing/create_shared_link_with_settings");
            shareRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            shareRequest.Content = new StringContent(JsonSerializer.Serialize(new { path = pathLower }));
            shareRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var shareResponse = await _client.SendAsync(shareRequest);
            var shareJson = await shareResponse.Content.ReadAsStringAsync();

            if (!shareResponse.IsSuccessStatusCode)
            {
                if (shareJson.Contains("shared_link_already_exists"))
                {
                    // Lấy link chia sẻ đã tồn tại từ phản hồi lỗi
                    using var errorDoc = JsonDocument.Parse(shareJson);
                    var existingLink = errorDoc.RootElement
                        .GetProperty("error")
                        .GetProperty("shared_link_already_exists")
                        .GetProperty("metadata")
                        .GetProperty("url")
                        .GetString();

                     downloadUrl = existingLink.Replace("?dl=0", "?dl=1");
                }
                else
                {
                    throw new Exception("Tạo link chia sẻ thất bại: " + shareJson);
                }
            }
            else
            {
                using var shareDoc = JsonDocument.Parse(shareJson);
                var sharedUrl = shareDoc.RootElement.GetProperty("url").GetString();
                 downloadUrl = sharedUrl.Replace("?dl=0", "?dl=1");
            }

            

            // 3. Trả về chuỗi JSON gọn nhẹ để lưu DB
            var result = new
            {
                name,
                path_display = pathDisplay,
                size,
                client_modified = clientModified,
                shared_link = downloadUrl
            };

            return JsonSerializer.Serialize(result);
        }


        public async Task<string> CreateSharedLinkAsync(string dropboxPath)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.dropboxapi.com/2/sharing/create_shared_link_with_settings");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Content = new StringContent(JsonSerializer.Serialize(new { path = dropboxPath }));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("Tạo link chia sẻ Dropbox thất bại: " + content);

            using var doc = JsonDocument.Parse(content);
            var url = doc.RootElement.GetProperty("url").GetString();

            // Dropbox link mặc định là `?dl=0` (xem preview), đổi thành `?dl=1` để tải trực tiếp
            return url.Replace("?dl=0", "?dl=1");
        }


    }

}
