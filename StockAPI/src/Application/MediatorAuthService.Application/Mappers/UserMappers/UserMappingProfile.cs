using AutoMapper;
using StockAPI.Application.Cqrs.Commands.UserCommands;
using StockAPI.Application.Cqrs.Queries.UserQueries;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Domain.Entities;

namespace StockAPI.Application.Mappers.UserMappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<GetUserByIdQuery, User>();
    }
}