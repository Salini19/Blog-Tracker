using DAL.Repository;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Blog_WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        Repository<AdminInfo> repo;
        public AdminController()
        {
            repo = new Repository<AdminInfo>();
        }

        // GET: api/<AdminController>


        [HttpGet]
        public IEnumerable<AdminInfo> Get()
        {
           return repo.GetAll();
           
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public AdminInfo Get(int id)
        {
            return repo.GetByID(id);
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] AdminInfo admin)
        {
            repo.Insert(admin);
            repo.Save();
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put( [FromBody] AdminInfo admin)
        {
            repo.Update(admin);
            repo.Save();
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }
    }
}
