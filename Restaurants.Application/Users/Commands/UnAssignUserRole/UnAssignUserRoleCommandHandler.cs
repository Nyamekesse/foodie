using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole
{
    public class UnAssignUserRoleCommandHandler(
        ILogger<UnAssignUserRoleCommandHandler> _logger,
        UserManager<User> _userManager,
        RoleManager<IdentityRole> _roleManager
    ) : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(
            UnAssignUserRoleCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation(
                "Un-assigning role {RoleName} to user {UserEmail}",
                request.RoleName,
                request.UserEmail
            );
            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            if (user is null)
                throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role is null)
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
