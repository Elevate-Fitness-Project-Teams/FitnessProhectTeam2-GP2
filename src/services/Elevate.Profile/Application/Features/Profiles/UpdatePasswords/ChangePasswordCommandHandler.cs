using Elevate.Profile.Application.Common;
using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.UpdatePasswords
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly ICurrentUser _currentUser;

        public ChangePasswordCommandHandler(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            throw new NotImplementedException();
        }

        Task IRequestHandler<ChangePasswordCommand>.Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
