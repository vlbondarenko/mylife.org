using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MediatR;
using Infrastructure.Identity.Exceptions;
using Infrastructure.Identity.Data;

namespace Infrastructure.Identity.Commands
{
    public class CreateAppUserCommand:IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, string>
        {
            private IdentityDbContext _identityDbContext;
            private UserManager<AppUser> _userManager;

            public CreateAppUserCommandHandler(UserManager<AppUser> userManager, IdentityDbContext identityDbContext)
            {
                _userManager = userManager;
                _identityDbContext = identityDbContext;
            }

            public async Task<string> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
            {
                if (await _identityDbContext.Users.Where(user => user.Email == request.Email).AnyAsync())
                    throw new UserNotCreatedException($"Email {request.Email} already taken.");

                if (await _identityDbContext.Users.Where(user => user.UserName == request.UserName).AnyAsync())
                    throw new UserNotCreatedException($"Username {request.UserName} already taken.");

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
                    throw new IdentityException(errors);
                }

                return user.Id;
            }
        }
    }
}
