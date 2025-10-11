
using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, CommandResult>
    {
        private readonly IPostServices _blogService;

        public DeletePostCommandHandler(IPostServices blogService)
        {
            _blogService = blogService;
        }
        public Task<CommandResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            return _blogService.DeleteBlog(request.BlogIds);
        }
    }
}
