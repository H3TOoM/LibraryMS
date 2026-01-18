using LibraryMS.Application.DTOs.Fines;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Update;

public record UpdateFineCommand(int Id, FineUpdateDto Fine) : IRequest<bool>;
