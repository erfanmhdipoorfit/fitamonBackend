using Fitamon.Domain.Blog.Entities;

namespace Fitamon.Domain.Blog.Contracts;

public interface IBlogServices
{
    Task<List<BlogEntity>> GetAllBlog(int pageIndex, int pageSize);
}