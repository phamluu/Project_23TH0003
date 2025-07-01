using QLHocPhan_23TH0003.ViewModel;
using System.Text.Json;
using System.IO;
namespace QLHocPhan_23TH0003.Common.Helpers
{
    public class DropboxParserHelper
    {
        public static DropboxFileViewModel Parse(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            // Fix chuỗi bị double-escaped
            if (json.TrimStart().StartsWith("\"") && json.TrimEnd().EndsWith("\""))
            {
                try
                {
                    json = JsonSerializer.Deserialize<string>(json);
                }
                catch
                {
                    return null; // Nếu vẫn lỗi
                }
            }

            if (!json.TrimStart().StartsWith("{"))
                return null;

            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            string name = root.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "unknown";
            string pathDisplay = root.TryGetProperty("path_display", out var pathProp) ? pathProp.GetString() : "";
            string downloadUrl = root.TryGetProperty("shared_link", out var linkProp) ? linkProp.GetString() : "#";
            long size = root.TryGetProperty("size", out var sizeProp) ? sizeProp.GetInt64() : 0;
            DateTime modified = root.TryGetProperty("client_modified", out var modProp) ? modProp.GetDateTime() : DateTime.MinValue;

            var ext = Path.GetExtension(name).ToLowerInvariant();

            return new DropboxFileViewModel
            {
                Name = name,
                PathDisplay = pathDisplay,
                FileType = ext.TrimStart('.'),
                DownloadUrl = downloadUrl,
                IconUrl = GetIconUrlByExtension(ext),
                ModifiedTime = modified,
                SizeFormatted = $"{(size / 1024.0):0.#} KB"
            };
        }

        private static string GetIconUrlByExtension(string ext)
        {
            return ext switch
            {
                ".pdf" => "/img/pdf.png",
                ".doc" or ".docx" => "/img/word.png",
                ".xls" or ".xlsx" => "/img/excel.png",
                ".ppt" or ".pptx" => "/img/ppt.png",
                ".mp4" => "/img/video.png",
                ".jpg" or ".jpeg" or ".png" => "/img/image.png",
                _ => "/img/file.png"
            };
        }
    }
}
