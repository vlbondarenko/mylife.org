using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Queries;

using AutoMapper;

namespace Infrastructure.Identity.Mapping
{
    public class IdentityMappingProfile:Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
        }
    }
}
