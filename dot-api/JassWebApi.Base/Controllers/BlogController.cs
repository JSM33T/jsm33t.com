using JassWebApi.Base.Filters;
using JassWebApi.Data;
using JassWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JassWebApi.Base.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController(IBlogRepository blogRepository) : JsmtBaseController
    {
        private readonly IBlogRepository _blogRepository = blogRepository;

        // POST: api/blog/filter
        [HttpPost("filter")]
        public IActionResult GetBlogs([FromBody] BlogFilter filter)
        {
            var blogs = _blogRepository.GetBlogs(filter);
            return RESP_Success(blogs);
        }

        // POST: api/blog
        //[HttpPost]
        //public IActionResult Insert([FromBody] Blog blog)
        //{
        //    var inserted = _blogRepository.Insert(blog);
        //    return RESP_Success(inserted, "Inserted successfully");
        //}

        // GET: api/blog/{id}
        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            var blog = _blogRepository.GetBlogs(new BlogFilter { PageSize = 1, Page = 1 })
                .Items.FirstOrDefault(b => b.Id == id);
            if (blog == null)
                return RESP_NotFoundResponse($"Blog with id {id} not found");
            return RESP_Success(blog);
        }

        [Persist(10)]
        [HttpGet("view/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var blog = _blogRepository.GetBySlug(slug);
            if (blog == null)
                return RESP_NotFoundResponse($"Blog with slug '{slug}' not found");
            return RESP_Success(blog);
        }

        // DELETE: api/blog/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // You can implement Delete in repository if needed
            return RESP_BadRequestResponse("Delete not implemented yet");
        }

        [Persist(100)]
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _blogRepository.GetAll()
          .Select(c => new
          {
              c.Id,
              c.Name,
              c.Slug
          })
          .ToList();

            return RESP_Success(categories);
        }
    }
}
