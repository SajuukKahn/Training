namespace Training.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

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
        public FileResult SaveFile(string data)
        {
            using var stream = System.IO.File.OpenRead(data);
            return File(stream, "application/json");
        }

        public FileResult OnGetDownloadFile(string path, string file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path, file);
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/octet-stream", file);
        }

        public class ExampleInfo(string name, string infohtmlpath, string filedownloadpath)
        {
            public string Name { get; set; } = name;
            public string InfoHtmlPath { get; set; } = infohtmlpath;
            public string DownloadsPath { get; set; } = filedownloadpath;
        }
    }
}
