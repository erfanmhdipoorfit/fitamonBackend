using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, CommandResult>
    {
        private readonly IBlogServices _blogService;

        public UpdateBlogCommandHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }


        public Task<CommandResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            return _blogService.UpdateBlogById(request.BlogId, request.Name);
        }
    }
}
