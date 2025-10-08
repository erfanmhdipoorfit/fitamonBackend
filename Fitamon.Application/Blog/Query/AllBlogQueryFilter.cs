using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.Query
{
   public class AllBlogQueryFilter:IRequest<List<BlogEntity>>
    {
        public AllBlogQueryFilter(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get;  set; }
        public int PageSize { get;  set; }
    }
}
