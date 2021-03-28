﻿using System;
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
    public class ResetPasswordCommand:IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
        {
            private readonly UserManager<AppUser> _userManager;

            public ResetPasswordCommandHandler (UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle (ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                if (user == null)
                    throw new UserNotFoundException($"User id{request.UserId} not found");

                var resultOfReset = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

                if (resultOfReset.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Reset password failure"); 
            }
        }
    }
}
