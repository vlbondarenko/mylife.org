using System.Threading.Tasks;
using System.Threading;

using MediatR;

using Persistence.Interfaces;
using Domain.Entities;

namespace Application.UseCases.UserProfiles.Commands
{
    public class CreateUserProfileCommand:IRequest
    {
        public string Id { get; set; }

        public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand>
        {
            private readonly IUserProfileDbContext _userProfileDbContext;

            public CreateUserProfileCommandHandler (IUserProfileDbContext userProfileDbContext)
            {
                _userProfileDbContext = userProfileDbContext;
            }

            public async Task<Unit> Handle (CreateUserProfileCommand request, CancellationToken cancellationToken)
            {
                var userProfile = new UserProfile() 
                { 
                    Id = request.Id 
                };

                await _userProfileDbContext.UserProfiles.AddAsync(userProfile, cancellationToken);
                await _userProfileDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
