using JassWebApi.Base.Filters;
using JassWebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController(IBlogRepository blogRepository) : JsmtBaseController
    {
        private readonly IBlogRepository _blogRepository = blogRepository;

        // POST: api/blog/filter
        [HttpPost("filter")]
        public async Task<IActionResult> GetBlogs([FromBody] BlogFilter filter)
        {
            var blogs = _blogRepository.GetBlogs(filter);
            return RESP_Success(blogs);
        }

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
        public async Task<IActionResult> GetBySlug(string slug)
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
