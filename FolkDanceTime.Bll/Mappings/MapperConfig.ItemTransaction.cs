using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureItemTransaction(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ItemTransaction, ItemTransactionDto>()
                .ForMember(dto => dto.ItemName, opt => opt.MapFrom(entity => entity.Item.Name));

            return cfg;
        }

        private static IMapperConfigurationExpression ConfigureDetailedItemTransaction(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ItemTransaction, DetailedItemTransactionDto>()
                .ForMember(dto => dto.ItemName, opt => opt.MapFrom(entity => entity.Item.Name));

            return cfg;
        }
    }
}
