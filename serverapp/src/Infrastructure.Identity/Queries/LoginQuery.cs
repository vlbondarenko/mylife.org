using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading;

using MediatR;

using Infrastructure.Identity.Data;
using Infrastructure.Identity.Exceptions;
using Infrastructure.Identity.Interfaces;

namespace Infrastructure.Identity.Queries
{
    public class LoginQuery:IRequest<AppUserDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, AppUserDTO>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenClaimsService _tokenClaimsService;

            public LoginQueryHandler (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenClaimsService tokenClaimsService) 
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenClaimsService = tokenClaimsService;
            }

            public async Task<AppUserDTO> Handle (LoginQuery query, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(query.Email);
                if (user is null)
                    throw new UserNotFoundException($"User {query.Email} not found.");

                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

                if (loginResult.Succeeded)
                {
                    return new AppUserDTO()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        CreatedAt = user.CreatedAt,
                        AccessToken = _tokenClaimsService.CreateToken(user.Id)
                    };
                }
                else
                {
                    throw new UserNotFoundException("Invalid email or password");
                }            
            }
        }
    }
}
