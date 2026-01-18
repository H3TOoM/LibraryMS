using LibraryMS.Application.Helpers;
using LibraryMS.Domain.Entities;
using MediatR;

namespace LibraryMS.Application.Features.Tokens.Commands.GenerateToken;

public record GenerateTokenCommand(Member Member) : IRequest<TokenResult>;
