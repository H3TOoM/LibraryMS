using LibraryMS.Application.Features.Accounts.Commands.Login;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, int>
{
    private readonly IAccountRepoistory _accountRepoistory;

    public LoginCommandHandler(IAccountRepoistory accountRepoistory)
    {
        _accountRepoistory = accountRepoistory;
    }

    public async Task<int> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Retrieve member by email
        var member = await _accountRepoistory.GetUserByEmail(request.Email);

        // Verify credentials
        if (member == null || !BCrypt.Net.BCrypt.Verify(request.Password, member.PasswordHash))
            throw new ArgumentException("Invalid email or password");

        return member.Id;
    }
}
