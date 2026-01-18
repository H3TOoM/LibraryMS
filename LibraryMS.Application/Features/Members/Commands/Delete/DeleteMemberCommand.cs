using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Delete;

public record DeleteMemberCommand(int Id) : IRequest<bool>;
