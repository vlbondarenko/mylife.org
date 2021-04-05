using Infrastructure.Identity.Data;
using Infrastructure.Identity.Queries;

using AutoMapper;

namespace Infrastructure.Identity.Mapping
{
    public class IdentityMappingProfile:Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
