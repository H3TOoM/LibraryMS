using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Update;

public record UpdateMemberCommand(int Id, MemberUpdateDto Member) : IRequest<bool>;
