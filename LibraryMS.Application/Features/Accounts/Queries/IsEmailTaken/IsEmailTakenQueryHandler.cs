using LibraryMS.Application.Features.Accounts.Queries.IsEmailTaken;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Queries.IsEmailTaken;

public class IsEmailTakenQueryHandler : IRequestHandler<IsEmailTakenQuery, bool>
{
    private readonly IAccountRepoistory _accountRepoistory;

    public IsEmailTakenQueryHandler(IAccountRepoistory accountRepoistory)
    {
        _accountRepoistory = accountRepoistory;
    }

    public async Task<bool> Handle(IsEmailTakenQuery request, CancellationToken cancellationToken)
    {
        return await _accountRepoistory.IsEmailTakenAsync(request.Email);
    }
}
