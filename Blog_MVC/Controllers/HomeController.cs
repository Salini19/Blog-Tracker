using Blog_MVC.Models;
using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace Blog_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       

        public IActionResult Index()
        {

            List<Blog> blogs = new List<Blog>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7274/api/");
                var response = client.GetAsync("blog");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<BlogInfo[]>();
                    readData.Wait();
                    var blogdata = readData.Result;
                    foreach (var item in blogdata)
                    {
                        blogs.Add(new Blog { BlogId = item.BlogId, BlogUrl = item.BlogUrl, DateOfCreation = item.DateOfCreation, EmailId = item.EmailId, Subject = item.Subject, Title = item.Title });
                    }
                }
            }
            return View(blogs);
        }

        
        
        public IActionResult Action()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



              