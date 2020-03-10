using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities.Logging;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class LogMappers
    {
        public static LogEvent ToEntity(this LogEventDto model)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LogEventDto, LogEvent>()
                        .ForMember(x => x.LogTypeLookupId, opt => opt.MapFrom(x => x.LogType))
                        .ForMember(x => x.LogType, opt => opt.Ignore());
                })
                .CreateMapper().Map<LogEventDto, LogEvent>(model);
        }

        public static LogEventDto ToDto(this LogEvent model)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LogEvent, LogEventDto>()
                        .ForMember(x => x.LogType, opt => opt.MapFrom(x => x.LogType.Name));
                })
                .CreateMapper().Map<LogEvent, LogEventDto>(model);
        }

        public static IEnumerable<LogEventDto> ToDto(this IEnumerable<LogEvent> models)
        {
            return models.Select(x => x.ToDto());
        }
    }
}
