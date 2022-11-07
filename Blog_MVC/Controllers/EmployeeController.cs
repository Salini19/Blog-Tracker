using Blog_MVC.Models;
using DAL;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core.Metadata.Edm;

namespace Blog_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        Repository<EmpInfo> repo = new Repository<EmpInfo>();
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
                   
            List<Admin> blogs = new List<Admin>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7274/api/");
                var response = client.GetAsync("admin");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Admin[]>();
                    readData.Wait();
                    var blogdata = readData.Result;
                    foreach (var item in blogdata)
                    {
                        blogs.Add(new Admin { EmailId = item.EmailId, Password = item.Password });
                    }
                }
            }
            var found = blogs.Find(x => x.EmailId == admin.EmailId);
            if (found != null)
            {
                if (found.Password == admin.Password)
                {
                    return RedirectToAction("Index", "Employee");
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
        // GET: EmployeeController1
        public IActionResult Index()
        {
            List<Emp> emplist = new List<Emp>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7274/api/");
                var response = client.GetAsync("emp");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<EmpInfo[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new Emp { EmailId = item.EmailId, Name = item.Name, Passcode = item.Passcode, DateofJoining = item.DateofJoining });
                    }
                }
            }
            return View(emplist);
        }

        // GET: EmployeeController1/Details/5
        public Emp Details(int id)
        {
            var emp = repo.GetByID(id);
            Emp model = new Emp();
            model.EmailId = emp.EmailId;
            model.Name = emp.Name;
            model.DateofJoining = emp.DateofJoining;
            model.Passcode = emp.Passcode;


            return model;
            
            
        }

        // GET: EmployeeController1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Emp emp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7274/api/Emp");
                    var emp1 = new Emp { EmailId = emp.EmailId, Name = emp.Name, Passcode = emp.Passcode, DateofJoining = emp.DateofJoining };
                    var postTask = client.PostAsJsonAsync<Emp>(client.BaseAddress, emp1);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readtaskResult = result.Content.ReadAsAsync<Emp>();

                        readtaskResult.Wait();
                        var dataInserted = readtaskResult.Result;
                    }


                }              
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController1/Edit/5
        public IActionResult Edit(int id)
        {
            var emp = repo.GetByID(id);
            Emp model = new Emp();
            model.EmailId = emp.EmailId;
            model.Name = emp.Name;
            model.DateofJoining = emp.DateofJoining;
            model.Passcode = emp.Passcode;


            return View(model);

        }

        // POST: EmployeeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Emp emp)
        {
            try
            {
                EmpInfo emp1 = new EmpInfo();
                emp1.EmailId = emp.EmailId;
                emp1.Name = emp.Name;
                emp1.DateofJoining = emp.DateofJoining;
                emp1.Passcode = emp.Passcode;
                repo.Update(emp1);
            
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var emp = repo.GetByID(id);
            Emp model = new Emp();
            model.EmailId = emp.EmailId;
            model.Name = emp.Name;
            model.DateofJoining = emp.DateofJoining;
            model.Passcode = emp.Passcode;


            return View(model);
        
        }

        // POST: EmployeeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Emp emp)
        {
            try
            {

                if (emp != null)
                {
                     repo.Delete(emp.EmailId);                 
                     return RedirectToAction("Index");

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       

    }
}
