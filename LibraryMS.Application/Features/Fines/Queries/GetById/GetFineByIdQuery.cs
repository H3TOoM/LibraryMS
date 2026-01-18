using LibraryMS.Application.DTOs.Fines;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetById;

public record GetFineByIdQuery(int Id) : IRequest<FineReadDto?>;
