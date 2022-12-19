using AutoMapper;
using FluentValidation;
using StockAPI.Application.Cqrs.Commands.AuthCommands;
using StockAPI.Application.Cqrs.Queries.AuthQueries;
using StockAPI.Application.Dtos.AuthDtos;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace StockAPI.Application.Cqrs.CommandHandlers.AuthCommandHandlers;

public class CreateTokenByRefreshTokenCommandHandler : IRequestHandler<CreateTokenByRefreshTokenCommand, ApiResponse<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateTokenByRefreshTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<TokenDto>> Handle(CreateTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);

        var existUser = await _unitOfWork.GetRepository<User>()
            .Where(user => user.Id.Equals(userId) && user.RefreshToken.Equals(request.RefreshToken) && user.IsActive)
            .SingleOrDefaultAsync(cancellationToken);

        if (existUser is null)
            throw new ValidationException("User or refresh token not found.");

        var userDto = _mapper.Map<UserDto>(existUser);

        var generatedToken = await _mediator.Send(new GenerateTokenQuery(userDto), cancellationToken);

        existUser.RefreshToken = generatedToken.Data.RefreshToken;
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<TokenDto>
        {
            Data = generatedToken.Data,
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.OK,
            TotalItemCount = 1
        };
    }
}