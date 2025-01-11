using Microsoft.Extensions.Options;
using PersonalPageWASM.Models;
using System.Text.Json;

namespace PersonalPageWASM.Services
{
    public class GitHubService
    {
        private readonly GitHubSettings _settings;
        private readonly HttpClient _httpClient;

        public GitHubService(IOptions<GitHubSettings> storageSettings, HttpClient httpClient)
        {
            _settings = storageSettings.Value;
            _httpClient = httpClient;
        }

        //GET: /repos/{owner}/{repo}/contents/{path}
        public async Task<List<GitHubFile>?> GetFilesAsync(string folder)
        {
            var response = await _httpClient.GetAsync($"{_settings.ApiBaseUrl}/repos/{_settings.UserName}/{_settings.RepositoryName}/contents/{folder}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseContent))
                {
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var files = JsonSerializer.Deserialize<List<GitHubFile>>(responseContent, jsonOptions);

                    if (files == null)
                    {
                        throw new Exception("No data returned from GitHub repository");
                    }

                    foreach (var file in files)
                    {
                        file.Content = await _httpClient.GetStringAsync(file.DownloadUrl);
                    }

                    return files;
                }
            }

            throw new Exception($"Could not retrieve posts from GitHub, status code: {response.StatusCode}, error message: {response.ReasonPhrase}");
        }
    }
}
