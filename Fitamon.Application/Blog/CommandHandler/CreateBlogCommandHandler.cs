using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CommandResult>
    {
        private readonly IBlogServices _blogService;

        public CreateBlogCommandHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }
        public Task<CommandResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            return _blogService.CreateBlog(request.Blog);
        }
    }
}
