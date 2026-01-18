using LibraryMS.Application.DTOs.Fines;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetAll;

public record GetAllFinesQuery : IRequest<IEnumerable<FineReadDto>>;
