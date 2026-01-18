using LibraryMS.Application.DTOs.Members;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetById;

public record GetMemberByIdQuery(int Id) : IRequest<MemberReadDto?>;
