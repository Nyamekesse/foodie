using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantUserClaimsPrincipalFactory(
        UserManager<User> _userManager,
        RoleManager<IdentityRole> _roleManager,
        IOptions<IdentityOptions> _options
    ) : UserClaimsPrincipalFactory<User, IdentityRole>(_userManager, _roleManager, _options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);
            if (user.Nationality is not null)
            {
                id.AddClaim(new Claim("Nationality", user.Nationality));
            }
            if (user.DateOfBirth is not null)
            {
                id.AddClaim(
                    new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd"))
                );
            }

            return new ClaimsPrincipal(id);
        }
    }
}
