using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Create;

public record CreateMemberCommand(MemberCreateDto Member) : IRequest<int>;
