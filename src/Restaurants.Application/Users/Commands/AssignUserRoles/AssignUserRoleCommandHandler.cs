using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRoles
{
    public class AssignUserRoleCommandHandler(
        ILogger<AssignUserRoleCommandHandler> _logger,
        UserManager<User> _userManager,
        RoleManager<IdentityRole> _roleManager
    ) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Assigning role {RoleName} to user {UserEmail}",
                request.RoleName,
                request.UserEmail
            );
            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            if (user is null)
                throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role is null)
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            await _userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
