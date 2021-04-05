using AutoMapper;

using WebApi.Models;
using Infrastructure.Identity.UseCases.Queries;
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
