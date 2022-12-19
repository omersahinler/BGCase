using AutoMapper;
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

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool isExistUserByEmail = await _unitOfWork.GetRepository<User>().AnyAsync(x => x.Email.Equals(request.Email));

        if (isExistUserByEmail)
            throw new BusinessException("There is a record of the e-mail address.");

        request.Password = HashingManager.HashValue(request.Password);

        var userEntity = _mapper.Map<User>(request);

        userEntity.RefreshToken = HashingManager.HashValue(Guid.NewGuid().ToString());

        await _unitOfWork.GetRepository<User>().AddAsync(userEntity);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(userEntity),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.Created,
            TotalItemCount = 1
        };
    }
}