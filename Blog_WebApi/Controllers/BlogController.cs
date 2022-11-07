
using Blog_WebApi.Models;
using DAL;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        Repository<BlogInfo> repo;
        public BlogController()
        {
            repo = new Repository<BlogInfo>();
        }
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            var list= repo.GetAll();
            List<Blog> blogs = new List<Blog>();
            foreach (var item in list)
            {
                blogs.Add(new Blog { BlogId = item.BlogId, Subject = item.Subject, Title = item.Title,EmailId=item.EmailId, BlogUrl = item.BlogUrl, DateOfCreation = item.DateOfCreation });
            }
            return blogs;
        }


        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public Blog Get(int id)
        {
            var blog= repo.GetByID(id);
            Blog blog1 = new Blog();
            blog1.Subject = blog.Subject;
            blog1.EmailId = blog.EmailId;
            blog1.BlogId = blog.BlogId;
            blog1.BlogUrl=blog.BlogUrl;
            blog1.DateOfCreation = blog.DateOfCreation;
            blog1.Title = blog.Title;

            return blog1;

           //List<BlogInfo> list = context.BlogInfos.ToList();
           //BlogInfo blog= list.Find(x => x.BlogId == id)!;
           // return blog;
        }

        // POST api/<BlogController>
        [HttpPost]
        public void Post([FromBody] Blog blog)
        {
            BlogInfo blog1 = new BlogInfo();
            blog1.Subject = blog.Subject;
            blog1.EmailId = blog.EmailId;
            blog1.BlogId = blog.BlogId;
            blog1.BlogUrl = blog.BlogUrl;
            blog1.DateOfCreation = blog.DateOfCreation;
            blog1.Title = blog.Title;
            repo.Insert(blog1);
            repo.Save();
        //    context.BlogInfos.Add(blog);
        //    context.SaveChanges();
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put( [FromBody] Blog blog)
        {
            BlogInfo blog1 = new BlogInfo();
            blog1.Subject = blog.Subject;
            blog1.EmailId = blog.EmailId;
            blog1.BlogId = blog.BlogId;
            blog1.BlogUrl = blog.BlogUrl;
            blog1.DateOfCreation = blog.DateOfCreation;
            blog1.Title = blog.Title;

            repo.Update(blog1);
            
            //List<BlogInfo> list=context.BlogInfos.ToList();
            //BlogInfo blog1= list.Find(x => x.BlogId == id)!;
            //list.Remove(blog1);
            //list.Add(blog);
            //context.SaveChanges();

        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
            //List<BlogInfo> list = context.BlogInfos.ToList();
            //BlogInfo blog1 = list.Find(x => x.BlogId == id)!;
            //list.Remove(blog1);
            //context.SaveChanges();
        }
    }
}
