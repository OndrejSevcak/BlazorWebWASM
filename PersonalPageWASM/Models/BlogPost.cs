namespace PersonalPageWASM.Models
{
    public class BlogPost
    {
        public string Id => Title + "_" + Date.ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateOnly Date { get; set; }
        public string Category { get; set; }
        public string Hashtags { get; set; }

        public string HtmlContent { get; set; }
    }
}
