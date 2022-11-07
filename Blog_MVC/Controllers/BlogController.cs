using Blog_MVC.Models;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Blog_MVC.Controllers
{
    public class BlogController : Controller
    {

        public ActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpLogin(Emp emp)
        {
            List<Emp> blogs = new List<Emp>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7274/api/");
                var response = client.GetAsync("emp");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Emp[]>();
                    readData.Wait();
                    var blogdata = readData.Result;
                    foreach (var item in blogdata)
                    {
                        blogs.Add(new Emp { EmailId = item.EmailId, Passcode = item.Passcode });
                    }
                }
            }
            var found = blogs.Find(x => x.EmailId == emp.EmailId);
            if (found != null)
            {
                if (found.Passcode == emp.Passcode)
                {
                    TempData["EmailId"] = found.EmailId;
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ViewBag.msg = "Incorrect Paasword";
                }
            }
            else
            {
                ViewBag.msg = "Invalid Credentials";
            }
            return View();
        }

        // GET: BlogController
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
                    var readData = result.Content.ReadAsAsync<Blog[]>();
                    readData.Wait();
                    var blogdata = readData.Result;
                    var ans = TempData["EmailId"].ToString();
                    foreach (var item in blogdata)
                    {
                        if (item.EmailId==ans)
                        {
                            blogs.Add(new Blog { BlogId = item.BlogId, BlogUrl = item.BlogUrl, DateOfCreation = item.DateOfCreation, EmailId = item.EmailId, Subject = item.Subject, Title = item.Title });

                        }
                    }
                }
            }
            return View(blogs);
        }

        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44365/api/blog");
                    var blog1 = new Blog { BlogId = blog.BlogId, BlogUrl = blog.BlogUrl, DateOfCreation = blog.DateOfCreation, EmailId = TempData["EmailId"].ToString(), Subject = blog.Subject, Title = blog.Title };
                    var postTask = client.PostAsJsonAsync<Blog>(client.BaseAddress, blog1);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readtaskResult = result.Content.ReadAsAsync<Blog>();
                        readtaskResult.Wait();
                        var dataInserted = readtaskResult.Result;
                    }
                }

                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
