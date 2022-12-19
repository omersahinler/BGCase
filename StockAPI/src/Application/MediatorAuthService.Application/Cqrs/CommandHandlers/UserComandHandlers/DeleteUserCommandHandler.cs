using FluentValidation;
using StockAPI.Application.Cqrs.Commands.UserCommands;
using StockAPI.Application.Dtos.ResponseDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using MediatR;
using System.Net;
using StockAPI.Infrastructure.UnitOfWork;

namespace StockAPI.Application.Cqrs.CommandHandlers.UserComandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<NoDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<NoDataDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);

        if (existUser is null)
            throw new ValidationException("User is not found.");

        _unitOfWork.GetRepository<User>().Remove(existUser);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<NoDataDto>
        {
            StatusCode = (int)HttpStatusCode.NoContent,
            IsSuccessful = true,
        };
    }
}