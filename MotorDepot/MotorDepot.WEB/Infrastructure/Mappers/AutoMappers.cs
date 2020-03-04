using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class AutoMappers
    {
        public static AutoSetViewModel ToSetViewModel(this AutoDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoDto, AutoSetViewModel>()
                    .ForMember("AutoBrand", opt => opt.MapFrom(x => x.Brand.Name));

            }).CreateMapper().Map<AutoDto, AutoSetViewModel>(model);
        }

        public static IEnumerable<AutoSetViewModel> ToSetViewModels(this IEnumerable<AutoDto> models)
        {
            return models.Select(x => x.ToSetViewModel());
        }

        public static AutoDto ToDto(this AutoCreateViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoCreateViewModel, AutoDto>()
                    .ForMember("Status", opt => opt.MapFrom(x => (AutoType) x.AutoTypeId))
                    .ForMember("Brand", opt => opt.MapFrom(x => new AutoBrandDto {Id = x.AutoBrandId}));
            }).CreateMapper().Map<AutoCreateViewModel, AutoDto>(model);
        }
    }
}