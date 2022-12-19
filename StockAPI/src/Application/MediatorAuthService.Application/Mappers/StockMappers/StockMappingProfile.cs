using AutoMapper;
using StockAPI.Application.Cqrs.Commands.StockCommands;
using StockAPI.Application.Dtos.StockDtos;
using StockAPI.Domain.Entities;

namespace StockAPI.Application.Mappers.StockMappers;

public class StockMappingProfile : Profile
{
    public StockMappingProfile()
    {
        CreateMap<Stock, StockDto>().ReverseMap();
        CreateMap<CreateStockCommand, Stock>();
        CreateMap<UpdateStockCommand, Stock>();
    }
}