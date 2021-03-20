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
using Infrastructure.Exceptions;
using ApplicationCore.Entities;

namespace Infrastructure.Identity.Commands
{
    public class CreateUserCommand:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
        {
            private IApplicationDbContext _appDbContext;
            private IdentityDbContext _identityDbContext;
            private UserManager<ApplicationUser> _userManager;

            public CreateUserCommandHandler(IApplicationDbContext appDbContext, UserManager<ApplicationUser> userManager, IdentityDbContext identityDbContext)
            {
                _appDbContext = appDbContext;
                _userManager = userManager;
                _identityDbContext = identityDbContext;
            }

            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                if (await _identityDbContext.Users.Where(user => user.Email == request.Email).AnyAsync())
                    throw new IdentityException() { ErrorMessage = "Email already exist" };

                var user = new ApplicationUser()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var userProfile = new UserProfile()
                    {
                        Id = user.Id
                    };

                   await _appDbContext.UserProfiles.AddAsync(userProfile,cancellationToken);
                   await _appDbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    throw new IdentityException() { ErrorMessage = "" };
                }

                return Unit.Value;
            }
        }
    }
}
