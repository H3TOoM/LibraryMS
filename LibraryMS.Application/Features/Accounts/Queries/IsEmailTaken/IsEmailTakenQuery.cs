using MediatR;

namespace LibraryMS.Application.Features.Accounts.Queries.IsEmailTaken;

public record IsEmailTakenQuery(string Email) : IRequest<bool>;
