using LibraryMS.Application.DTOs.Fines;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Create;

public record CreateFineCommand(FineCreateDto Fine) : IRequest<int>;
