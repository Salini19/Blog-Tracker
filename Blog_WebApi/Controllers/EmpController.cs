using Blog_WebApi.Models;
using DAL;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        Repository<EmpInfo> repo;
        public EmpController()
        {
           repo= new Repository<EmpInfo>();
        }


        // GET: api/<EmpController>
       
        [HttpGet]
        public IEnumerable<Emp> Get()
        {
            var list= repo.GetAll();
            List<Emp> emps = new List<Emp>();
            foreach (var item in list)
            {
                emps.Add(new Emp { EmailId = item.EmailId, Name = item.Name, Passcode = item.Passcode, DateofJoining = item.DateofJoining });
            }
            return emps;
        }

        // GET api/<EmpController>/5
       
        [HttpGet("{id}")]
        public Emp Get(string id)
        {

            EmpInfo emp= repo.GetByID(id);
            Emp emp1 = new Emp();
            emp1.EmailId=emp.EmailId;
            emp1.Name=emp.Name;
            emp1.DateofJoining = emp.DateofJoining;
            emp1.Passcode = emp.Passcode;

            return emp1;
            //List<EmpInfo> list= context.EmpInfos.ToList();
            //EmpInfo emp =list.Find(x => x.EmailId == id)!;
            //return emp;

        }

        // POST api/<EmpController>
     
        [HttpPost]
        public void Post([FromBody] Emp emp)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo.EmailId = emp.EmailId;
            empInfo.Name = emp.Name;
            empInfo.DateofJoining = emp.DateofJoining;
            empInfo.Passcode = emp.Passcode;

            repo.Insert(empInfo);
            repo.Save();
            //context.EmpInfos.Add(emp);
            //context.SaveChanges();
        }

        // PUT api/<EmpController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Emp emp )
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo.EmailId = emp.EmailId;
            empInfo.Name = emp.Name;
            empInfo.DateofJoining = emp.DateofJoining;
            empInfo.Passcode = emp.Passcode;
            repo.Update(empInfo);
            
            //List<EmpInfo> list = context.EmpInfos.ToList();
            //EmpInfo emp1 = list.Find(x => x.EmailId == id)!;
            //list.Remove(emp1);
            //list.Add(emp);
            //context.SaveChanges();
        }

        // DELETE api/<EmpController>/5
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            repo.Delete(id);
            repo.Save();
            //    List<EmpInfo> list = context.EmpInfos.ToList();
            //    EmpInfo emp1 = list.Find(x => x.EmailId == id)!;
            //    list.Remove(emp1);
            //    context.SaveChanges();
        }
    }
}
