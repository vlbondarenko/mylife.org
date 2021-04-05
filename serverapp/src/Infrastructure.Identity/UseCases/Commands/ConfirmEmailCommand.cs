using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using FluentValidation;
using MediatR;

using Infrastructure.Identity.Data;
using Infrastructure.Identity.Exceptions;

namespace Infrastructure.Identity.UseCases.Commands
{
    public class ConfirmEmailCommand:IRequest<bool>
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public class ConfirmEmailCommandHandler: IRequestHandler<ConfirmEmailCommand,bool>
        {
            private readonly UserManager<AppUser> _userManager;

            public ConfirmEmailCommandHandler (UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle (ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user is null)
                    throw new NotFoundException($"User id{request.Id} not found.");

                var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                return confirmResult.Succeeded;
            }
        }
    }

    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
