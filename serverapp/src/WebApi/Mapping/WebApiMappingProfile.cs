using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using WebApi.Models;
using Infrastructure.Identity.Queries;
using Application.UseCases.UserProfiles.Queries;

namespace WebApi.Mapping
{
    public class WebApiMappingProfile:Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<UserProfileDto, UserModel>();
            CreateMap<AppUserDto, UserModel>();
        }
    }
}
