using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using AutoStatus = MotorDepot.BLL.Infrastructure.Enums.AutoStatus;
using AutoType = MotorDepot.BLL.Infrastructure.Enums.AutoType;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class AutoMappers
    {
        public static AutoDto ToDto(this Auto auto)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Auto, AutoDto>()
                    .ForMember("Brand", opt => opt.MapFrom(x => x.Brand.ToDto()))
                    .ForMember("Type", opt => opt.MapFrom(x => (AutoType) x.AutoTypeId))
                    .ForMember("Status", opt => opt.MapFrom(x => (AutoStatus) x.StatusId));
            }).CreateMapper().Map<Auto, AutoDto>(auto);
        }

        public static IEnumerable<AutoDto> ToDto(this IEnumerable<Auto> autos)
        {
            return autos.Select(x => x.ToDto());
        }

        public static Auto ToEntity(this AutoDto autoDto)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoDto, Auto>()
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (AutoTypeEnum)x.Type))
                    .ForMember("StatusId", opt => opt.MapFrom(x => (AutoStatusEnum)x.Status))
                    .ForMember("AutoBrandId", opt => opt.MapFrom(x => x.Brand.Id))
                    .ForMember("Status", opt => opt.Ignore())
                    .ForMember("Brand", opt => opt.Ignore())
                    .ForMember("Type", opt => opt.Ignore());
            }).CreateMapper().Map<AutoDto, Auto>(autoDto);
        }

        public static AutoBrand ToEntity(this AutoBrandDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoBrandDto, AutoBrand>()
                    .ForMember("Autos", opt => opt.Ignore());

            }).CreateMapper().Map<AutoBrandDto, AutoBrand>(model);
        }

        public static AutoBrandDto ToDto(this AutoBrand model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoBrand, AutoBrandDto>()
                    .ForMember("Autos", opt => opt.Ignore());

            }).CreateMapper().Map<AutoBrand, AutoBrandDto>(model);
        }

        public static IEnumerable<AutoBrandDto> ToDto(this IEnumerable<AutoBrand> models)
        {
            return models.Select(x => x.ToDto());
        }
    }
}
