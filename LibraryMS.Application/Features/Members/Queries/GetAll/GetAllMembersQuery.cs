using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetAll;

public record GetAllMembersQuery : IRequest<IEnumerable<MemberReadDto>>;
