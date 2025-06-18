namespace Training.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.IO;

    public class IndexModel : PageModel
    {
        public List<ExampleInfo> Examples { get; set; } = [];

        public void OnGet()
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\content");
            var exampleDirs = Directory.GetDirectories(rootPath).Where(d => System.IO.File.Exists(Path.Combine(d, "info.html")));
            
            foreach (var dir in exampleDirs)
            {
                var name = Path.GetFileName(dir);
                var infoHtmlPath = $"/{name}/info.html";
                var filePath = $"/{name}/Downloads/";

                Examples.Add(new ExampleInfo(name, infoHtmlPath, filePath));
            }
        }

        public class ExampleInfo(string name, string infohtmlpath, string filedownloadpath)
        {
            public string Name { get; set; } = name;
            public string InfoHtmlPath { get; set; } = infohtmlpath;
            public string DownloadsPath { get; set; } = filedownloadpath;
        }
    }
}
