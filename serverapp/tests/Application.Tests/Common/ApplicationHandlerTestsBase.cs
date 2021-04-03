using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Context;
using AutoMapper;
using Application.Common.Mapping;
using Xunit;

namespace Application.Tests.Common
{
    public class ApplicationHandlerTestsBase:IDisposable
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly IMapper _mapper;

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
