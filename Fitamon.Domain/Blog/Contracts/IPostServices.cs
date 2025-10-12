using Fitamon.Domain.Blog.Entities;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Domain.Blog.Contracts;

public interface IPostServices
{
    Task<List<PostEntity>> GetAllBlog(int pageIndex, int pageSize);
    Task<PostEntity> GetBlogById(int partyId);
    Task<CommandResult> DeleteBlog(List<int> blogIds);
    Task<CommandResult> UpdateBlogById(int blogIds, string name);
    Task<CommandResult> CreateBlog(string name);
}