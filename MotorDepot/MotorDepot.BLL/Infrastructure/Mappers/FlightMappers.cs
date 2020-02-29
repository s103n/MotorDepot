using System.Collections.Generic;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static Flight ToFlight(this FlightDto model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<FlightDto, Flight>())
                .CreateMapper()
                .Map<FlightDto, Flight>(model);
        }

        public static FlightDto ToFlightDto(this Flight model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightDto>())
                .CreateMapper()
                .Map<Flight, FlightDto>(model);
        }

        public static IEnumerable<FlightDto> ToFlightDtos(this IEnumerable<Flight> models)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightDto>())
                .CreateMapper()
                .Map<IEnumerable<Flight>, IEnumerable<FlightDto>>(models);
        }
    }
}
