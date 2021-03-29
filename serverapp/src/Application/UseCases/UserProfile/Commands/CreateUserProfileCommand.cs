using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            private readonly IApplicationDbContext _applicationDbContext;

            public CreateUserProfileCommandHandler (IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle (CreateUserProfileCommand request, CancellationToken cancellationToken)
            {
                var userProfile = new UserProfile() 
                { 
                    Id = request.Id 
                };

                await _applicationDbContext.UserProfiles.AddAsync(userProfile, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
