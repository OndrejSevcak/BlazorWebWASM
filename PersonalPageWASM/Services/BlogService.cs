using Markdig;
using PersonalPageWASM.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PersonalPageWASM.Services
{
    public class BlogService
    {
        private readonly GitHubService _githubService;

        public List<BlogPost>? Posts { get; private set; }

        public BlogService(GitHubService githubService)
        {
            _githubService = githubService;
        }

        public async Task<List<BlogPost>?> GetPostsAsync(string folderName)
        {
            List<BlogPost> posts = new List<BlogPost>();
            
            var files = await _githubService.GetFilesAsync(folderName);

            if (files == null) return null;

            foreach (var file in files)
            {
                //YAML header deserialization
                var header = GetYamlHeader(file.Content);

                var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var post = deserializer.Deserialize<BlogPost>(header);

                post = post ?? new BlogPost();  //yaml header is optional

                //Markdown to HTML transformation
                var markdown = GetMarkdownContent(file.Content);
                post.HtmlContent = TransformMarkdownToHtml(markdown);
                posts.Add(post);
            }

            Posts = posts;

            return posts;
        }

        private string TransformMarkdownToHtml(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(markdown, pipeline);
        }

        private string GetYamlHeader(string content)
        {
            var lines = content.Split("\n");
            var yamlLines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "---")
                {
                    for (int j = i + 1; j < lines.Length; j++)
                    {
                        if (lines[j].Trim() == "---")
                        {
                            return string.Join("\n", yamlLines);
                        }
                        yamlLines.Add(lines[j]);
                    }
                }
            }
            return string.Empty;
        }

        private string GetMarkdownContent(string content)
        {
            var lines = content.Split("\n");
            var markdownLines = new List<string>();
            var isYaml = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "---")
                {
                    if (isYaml)
                    {
                        isYaml = false;
                    }
                    else
                    {
                        isYaml = true;
                    }
                }
                else if (!isYaml)
                {
                    markdownLines.Add(lines[i]);
                }
            }
            return string.Join("\n", markdownLines);
        }
    }
}
