using MediatR;

namespace LibraryMS.Application.Features.Borrows.Commands.Return;

public record ReturnBookCommand(int BorrowRecordId, DateTime ReturnedAt) : IRequest<bool>;
