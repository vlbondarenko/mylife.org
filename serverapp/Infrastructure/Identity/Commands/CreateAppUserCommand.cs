using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MediatR;
using Persistence.Interfaces;
using Infrastructure.Identity.Exceptions;
using ApplicationCore.Entities;

namespace Infrastructure.Identity.Commands
{
    public class CreateAppUserCommand:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateAppUserCommand>
        {
            private IdentityDbContext _identityDbContext;
            private UserManager<AppUser> _userManager;

            public CreateUserCommandHandler(UserManager<AppUser> userManager, IdentityDbContext identityDbContext)
            {
                _userManager = userManager;
                _identityDbContext = identityDbContext;
            }

            public async Task<Unit> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
            {
                if (await _identityDbContext.Users.Where(user => user.Email == request.Email).AnyAsync())
                    throw new UserNotCreatedException("Email already taken.");

                if (await _identityDbContext.Users.Where(user => user.UserName == request.UserName).AnyAsync())
                    throw new UserNotCreatedException("Username already taken.");

                var user = new AppUser()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(error => error.Description);
                    throw new UserNotCreatedException (errors);
                }

                return Unit.Value;
            }
        }
    }
}
