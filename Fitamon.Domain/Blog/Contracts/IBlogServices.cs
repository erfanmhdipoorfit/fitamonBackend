using Fitamon.Domain.Blog.Entities;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Domain.Blog.Contracts;

public interface IBlogServices
{
    Task<List<BlogEntity>> GetAllBlog(int pageIndex, int pageSize);
    Task<BlogEntity> GetBlogById(int partyId);
    Task<CommandResult> DeleteBlog(List<int> blogIds);
    Task<CommandResult> UpdateBlogById(int blogIds, BlogEntity blog);
    Task<CommandResult> CreateBlog(BlogEntity blog);
}