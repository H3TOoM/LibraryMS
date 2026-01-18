using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.UpdateAccount;

public record UpdateAccountCommand(int Id, MemberUpdateDto Member) : IRequest<bool>;
