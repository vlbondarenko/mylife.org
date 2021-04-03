using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.UseCases.UserProfiles.Queries;

using AutoMapper;

namespace Application.Common.Mapping
{
    public class ApplicationMappingProfile:Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDto>();
        }
    }
}
