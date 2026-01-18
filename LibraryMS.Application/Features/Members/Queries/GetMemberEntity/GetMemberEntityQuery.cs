using LibraryMS.Domain.Entities;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetMemberEntity;

public record GetMemberEntityQuery(int Id) : IRequest<Member?>;
