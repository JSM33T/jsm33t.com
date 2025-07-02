using JassWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassWebApi.Data
{
    public interface IBlogRepository
    {
        PagedResult<BlogSummary> GetBlogs(BlogFilter filter);
        Blog Insert(Blog blog);
        Blog? GetBySlug(string slug);
        List<Category> GetAll();
    }

    public class BlogFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? CategoryId { get; set; }
        public string? Tag { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<BlogSummary> GetBlogs(BlogFilter filter)
        {
            var query = _dbContext.Blogs
                .Include(b => b.Category)
                .AsQueryable();

            if (filter.FromDate.HasValue)
                query = query.Where(b => b.CreatedAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(b => b.CreatedAt <= filter.ToDate.Value);

            if (filter.CategoryId.HasValue)
                query = query.Where(b => b.CategoryId == filter.CategoryId.Value);

            if (!string.IsNullOrEmpty(filter.Tag))
                query = query.Where(b => b.Tags.Contains(filter.Tag));

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(b => new BlogSummary
                {
                    Id = b.Id,
                    Slug = b.Slug,
                    Title = b.Title,
                    Description = b.Description,
                    Tags = b.Tags,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,
                    CategorySlug = b.Category.Slug,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt
                })
                .ToList();

            return new PagedResult<BlogSummary>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
        public Blog Insert(Blog blog)
        {
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return blog;
        }

        public Blog? GetBySlug(string slug)
        {
            return _dbContext.Blogs
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Slug == slug);
        }

        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
    }
}
