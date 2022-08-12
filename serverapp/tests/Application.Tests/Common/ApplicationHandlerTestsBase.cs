using System;

using AutoMapper;

using Persistence.Context;
using Application.Common.Mapping;
using Microsoft.Extensions.Localization;
using static Application.UseCases.UserProfiles.Queries.GetUserProfileQuery;

namespace Application.Tests.Common
{
    public class ApplicationHandlerTestsBase:IDisposable
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly IMapper _mapper;
        protected readonly IStringLocalizer<GetUserProfileQueryHandler> _localizer;

        public ApplicationHandlerTestsBase()
        {
            _applicationDbContext = ApplicationDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_applicationDbContext);
        }
    }
}
