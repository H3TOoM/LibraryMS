using LibraryMS.Application.DTOs.BorrowRecords;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Commands.Borrow;

public record BorrowBookCommand(BorrowRecordCreateDto BorrowRecord) : IRequest<int>;
