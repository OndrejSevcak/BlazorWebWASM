using System.Text.Json.Serialization;

namespace PersonalPageWASM.Models
{
    public class GitHubFile
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Sha { get; set; }
        public string? Url { get; set; }

        [JsonPropertyName("download_url")]
        public string? DownloadUrl { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string FileExtension
        {
            get
            {
                var index = Name.LastIndexOf('.');
                return index != -1 ? Name.Substring(index + 1) : string.Empty;
            }
        }
    }
}
