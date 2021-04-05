using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MediatR;
using FluentValidation;

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
                    throw new NotCreatedException($"Email {request.Email} already taken.");

                if (await _identityDbContext.Users.Where(user => user.UserName == request.UserName).AnyAsync())
                    throw new NotCreatedException($"Username {request.UserName} already taken.");

                var user = new AppUser()
                {
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(error => error.Description);
                    throw new NotCreatedException(errors);
                }

                return user.Id;
            }
        }
    }
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
