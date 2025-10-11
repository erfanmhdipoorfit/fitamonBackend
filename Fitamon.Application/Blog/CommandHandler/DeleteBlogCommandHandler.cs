
using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, CommandResult>
    {
        private readonly IBlogServices _blogService;

        public DeleteBlogCommandHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }
        public Task<CommandResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            return _blogService.DeleteBlog(request.BlogIds);
        }
    }
}
