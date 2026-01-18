using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Delete;

public record DeleteFineCommand(int Id) : IRequest<bool>;
