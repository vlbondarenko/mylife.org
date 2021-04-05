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
    public class ResetPasswordCommand:IRequest<bool>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
        {
            private readonly UserManager<AppUser> _userManager;

            public ResetPasswordCommandHandler (UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle (ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                if (user == null)
                    throw new NotFoundException($"User id{request.UserId} not found");

                var resultOfReset = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

                return resultOfReset.Succeeded;
            }
        }
    }
}
