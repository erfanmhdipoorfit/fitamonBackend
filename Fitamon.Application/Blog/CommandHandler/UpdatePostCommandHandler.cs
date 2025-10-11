using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdateBlogCommand, CommandResult>
    {
        private readonly IPostServices _blogService;

        public UpdatePostCommandHandler(IPostServices blogService)
        {
            _blogService = blogService;
        }


        public Task<CommandResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            return _blogService.UpdateBlogById(request.BlogId, request.Name);
        }
    }
}
