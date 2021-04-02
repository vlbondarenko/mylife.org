using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading;

using MediatR;
using AutoMapper;

using Infrastructure.Identity.Data;
using Infrastructure.Identity.Exceptions;
using Infrastructure.Identity.Interfaces;

namespace Infrastructure.Identity.Queries
{
    public class SignInQuery:IRequest<AppUserDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginQueryHandler : IRequestHandler<SignInQuery, AppUserDTO>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenClaimsService _tokenClaimsService;
            private readonly IMapper _mapper;

            public LoginQueryHandler (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenClaimsService tokenClaimsService,IMapper mapper) 
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenClaimsService = tokenClaimsService;
                _mapper = mapper;
            }

            public async Task<AppUserDTO> Handle (SignInQuery query, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(query.Email);
                if (user is null)
                    throw new UnauthorizedException("Invalid email or password");

                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

                if (loginResult.Succeeded)
                {
                    var appUserInfo = _mapper.Map<AppUserDTO>(user);
                    appUserInfo.AccessToken = _tokenClaimsService.CreateToken(appUserInfo.Id);

                    return appUserInfo;
                }
                else
                {
                    throw new UnauthorizedException("Invalid email or password");
                }            
            }
        }
    }
}
