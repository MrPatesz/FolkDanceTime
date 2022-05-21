using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureItem(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Item, ItemDto>()
                .ForMember(dto => dto.Properties, opt => opt.MapFrom(entity => entity.PropertyValues));

            return cfg;
        }

        private static IMapperConfigurationExpression ConfigureItemSetItem(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Item, ItemSetItemDto>()
                .ForMember(dto => dto.IsInItemSet, opt => opt.MapFrom(entity => entity.ItemSetId != null))
                .ForMember(dto => dto.IsInTransaction, opt => opt.MapFrom(entity => entity.ItemTransactions != null && entity.ItemTransactions.Any(t => t.Status == Shared.Enums.Status.Pending)));

            return cfg;
        }
    }
}
