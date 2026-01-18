using AutoMapper;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Accounts.Commands.CreateAccount;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
    private readonly IMainRepoistery<Member> _mainRepoistory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAccountRepoistory _accountRepoistory;

    public CreateAccountCommandHandler(IMainRepoistery<Member> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper, IAccountRepoistory accountRepoistory)
    {
        _mainRepoistory = mainRepoistory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accountRepoistory = accountRepoistory;
    }

    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (request.Member == null)
            throw new ArgumentNullException(nameof(request.Member), "Member data cannot be null.");

        // Check if email is already registered
        if (await _accountRepoistory.IsEmailTakenAsync(request.Member.Email))
            throw new ArgumentException("Email is already taken");

        // Map DTO to entity and hash password
        var member = _mapper.Map<Member>(request.Member);
        member.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Member.Password);

        // Save member to database
        await _mainRepoistory.AddAsync(member);
        await _unitOfWork.SaveChangesAsync();
        return member.Id;
    }
}
