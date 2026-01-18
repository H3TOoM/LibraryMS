using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Pay;

public record PayFineCommand(int FineId, DateTime PaidAt) : IRequest<bool>;
