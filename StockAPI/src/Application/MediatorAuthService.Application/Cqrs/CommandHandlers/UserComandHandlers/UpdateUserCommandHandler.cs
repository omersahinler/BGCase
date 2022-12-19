using AutoMapper;
using FluentValidation;
using StockAPI.Application.Cqrs.Commands.UserCommands;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Exceptions;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Core.Extensions;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using System.Net;

namespace StockAPI.Application.Cqrs.CommandHandlers.UserComandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);

        if (existUser is null)
            throw new ValidationException("User is not found.");

        if (!await ExistingEMailControlInEMailExchange(existUser.Email, request.Email))
            throw new BusinessException("The entered e-mail address is used.");

        request.Password = string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.Password)
            ? existUser.Password
            : HashingManager.VerifyHashedValue(existUser.Password, request.OldPassword)
                ? HashingManager.HashValue(request.Password)
                : throw new ValidationException("Your password does not match.");

        var mappedUser = _mapper.Map(request, existUser);

        _unitOfWork.GetRepository<User>().Update(mappedUser);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(mappedUser),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.OK,
            TotalItemCount = 1
        };
    }

    private async Task<bool> ExistingEMailControlInEMailExchange(string oldEmail, string newEmail)
    {
        if (oldEmail.Equals(newEmail))
            return true;

        return !await _unitOfWork.GetRepository<User>().AnyAsync(user => user.Email.Equals(newEmail));
    }
}