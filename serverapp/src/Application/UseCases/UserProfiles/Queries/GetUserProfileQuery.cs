using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;
using FluentValidation;

using Persistence.Interfaces;
using Application.Exceptions;

namespace Application.UseCases.UserProfiles.Queries
{
    public class GetUserProfileQuery:IRequest<UserProfileDto>
    {
        public string Id { get; set; }

        public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
        {
            private readonly IUserProfileDbContext _userProfileDbContext;
            private readonly IMapper _mapper;

            public GetUserProfileQueryHandler(IUserProfileDbContext userProfileDbContext, IMapper mapper)
            {
                _userProfileDbContext = userProfileDbContext;
                _mapper = mapper;
            }

            public async Task<UserProfileDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
            {
                var userProfile =await _userProfileDbContext.UserProfiles.FindAsync(query.Id);

                if (userProfile is null)
                    throw new NotFoundException($"User profile {query.Id} not found.");

                return _mapper.Map<UserProfileDto>(userProfile);
            }
        }
    }

    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
