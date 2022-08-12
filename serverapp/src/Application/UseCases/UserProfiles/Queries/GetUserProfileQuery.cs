using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Persistence.Interfaces;

namespace Application.UseCases.UserProfiles.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public string Id { get; set; }
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserProfileDbContext _userProfileDbContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetUserProfileQueryHandler> _localizer;

        public GetUserProfileQueryHandler(
            IUserProfileDbContext userProfileDbContext,
            IMapper mapper,
            IStringLocalizer<GetUserProfileQueryHandler> localizer)
        {
            _userProfileDbContext = userProfileDbContext;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileDbContext.UserProfiles.FindAsync(query.Id);

            var localizedError = _localizer["userNotFound", query.Id];

            if (userProfile is null)
                throw new NotFoundException(localizedError);

            return _mapper.Map<UserProfileDto>(userProfile);
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
