using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.UpdateUserDetail;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailsCommandHandler(
        ILogger<UpdateUserDetailsCommandHandler> _logger,
        IUserContext _userContext,
        IUserStore<User> _userStore
    ) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(
            UpdateUserDetailsCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

            var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken);
            if (dbUser is null)
                throw new NotFoundException(nameof(User), user!.Id.ToString());
            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;
            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
