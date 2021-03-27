using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading;

using MediatR;

using Infrastructure.Identity.Data;
using Infrastructure.Identity.Exceptions;

namespace Infrastructure.Identity.Commands
{
    public class ConfirmEmailCommand:IRequest
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public class ConfirmEmailCommandHandler: IRequestHandler<ConfirmEmailCommand>
        {
            private readonly UserManager<AppUser> _userManager;

            public ConfirmEmailCommandHandler (UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle (ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);

                if (user is null)
                    throw new UserNotFoundException($"User id{request.Id} not found.");

                var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                if (!confirmResult.Succeeded)
                {
                    var errors = confirmResult.Errors.Select(error => error.Description);
                    throw new IdentityException(errors);
                }

                return Unit.Value;
            }
        }
    }
}
