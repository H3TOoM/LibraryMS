using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<int>;
