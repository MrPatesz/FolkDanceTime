using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureItemSetTransaction(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ItemSetTransaction, ItemSetTransactionDto>()
                .ForMember(dto => dto.ItemSetName, opt => opt.MapFrom(entity => entity.ItemSet.Name))
                .ForMember(dto => dto.Items, opt => opt.MapFrom(entity => entity.ItemSet.Items));

            return cfg;
        }
    }
}
