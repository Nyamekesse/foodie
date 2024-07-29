using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(
        ILogger<MinimumAgeRequirement> _logger,
        IUserContext _userContext
    ) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumAgeRequirement requirement
        )
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation(
                "User {Email}, date of birth is {DateOfBirth} - Handling MinimumAgeRequirement",
                currentUser.Email,
                currentUser.DateOfBirth
            );
            if (currentUser.DateOfBirth is null)
            {
                _logger.LogInformation("User Date of Birth is null");
                return Task.CompletedTask;
            }
            if (
                currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge)
                <= DateOnly.FromDateTime(DateTime.Today)
            )
            {
                _logger.LogInformation("Authorization passed for user {Email}", currentUser.Email);
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
