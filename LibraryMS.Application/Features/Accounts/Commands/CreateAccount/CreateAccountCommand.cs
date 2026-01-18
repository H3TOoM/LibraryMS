using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(MemberCreateDto Member) : IRequest<int>;
