﻿using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    internal interface ITokenClaimsService
    {
        Task<string> CreateToken(string userId);
    }
}
