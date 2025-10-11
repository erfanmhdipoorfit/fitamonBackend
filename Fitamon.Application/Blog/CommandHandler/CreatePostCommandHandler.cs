using Fitamon.Application.Blog.Command;
using Fitamon.Domain.Blog.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.CommandHandler
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CommandResult>
    {
        private readonly IPostServices _blogService;

        public CreatePostCommandHandler(IPostServices blogService)
        {
            _blogService = blogService;
        }
        public Task<CommandResult> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return _blogService.CreateBlog(request.Name);
        }
    }
}
