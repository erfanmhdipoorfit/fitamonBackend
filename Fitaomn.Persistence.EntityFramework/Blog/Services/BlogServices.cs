using Fitamon.Domain.Blog.Contracts;

namespace Fitamon.Persistence.EntityFramework.Blog.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly BlogDbContext _context;
        public BlogServices(
           BlogDbContext context)
        {

            _context = context;
        }
        //public async Task<List<Blog>> GetAll()
        //{
        //    var res = _context.References.Where(x => x.Status == 1);
        //    if (documentType is not null)
        //        res = res.Where(x => x.DocumentType == (short)documentType);
        //    return await res.ToListAsync();
        //}

    }
}
