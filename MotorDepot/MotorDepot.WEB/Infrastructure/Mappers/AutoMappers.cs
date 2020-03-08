using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Models.Auto;
using System.Collections.Generic;
using System.Linq;

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
                    .ForMember("Status", opt => opt.MapFrom(x => (AutoType)x.AutoTypeId))
                    .ForMember("Brand", opt => opt.MapFrom(x => new AutoBrandDto { Id = x.AutoBrandId }));
            }).CreateMapper().Map<AutoCreateViewModel, AutoDto>(model);
        }

        public static AutoEditViewModel ToEditViewModel(this AutoDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoDto, AutoEditViewModel>()
                    .ForMember("AutoBrandId", opt => opt.MapFrom(x => x.Brand.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int)x.Type));
            }).CreateMapper().Map<AutoDto, AutoEditViewModel>(model);
        }

        public static AutoDto ToDto(this AutoEditViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoEditViewModel, AutoDto>()
                    .ForMember("Brand", opt => opt.MapFrom(x => new AutoBrandDto { Id = model.AutoBrandId }))
                    .ForMember("Type", opt => opt.MapFrom(x => (AutoType)x.AutoTypeId));
            }).CreateMapper().Map<AutoEditViewModel, AutoDto>(model);
        }

        public static IEnumerable<AutoDisplayViewModel> ToDisplayViewModel(
            this IEnumerable<AutoDto> model,
            IEnumerable<FlightDto> flights)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AutoDto, AutoDisplayViewModel>()
                    .ForMember("Brand", opt => opt.MapFrom(x => x.Brand.Name))
                    .ForMember("Type", opt => opt.MapFrom(x => x.Type.ToString()))
                    .ForMember("Status", opt => opt.MapFrom(x => x.Status.ToString()))
                    .ForMember("UsedInFlightNow", opt => opt.MapFrom(x => flights.IsInFlightByAutoId(x.Id)));
            }).CreateMapper().Map<IEnumerable<AutoDto>, IEnumerable<AutoDisplayViewModel>>(model);
        }

        private static bool IsInFlightByAutoId(this IEnumerable<FlightDto> flights, int autoId)
        {
            return flights.Any(flight => flight.Auto != null
                                         && flight.Auto.Id == autoId
                                         && flight.Status == FlightStatus.Occupied
                                         || flight.Status == FlightStatus.Performed);
        }
    }
}