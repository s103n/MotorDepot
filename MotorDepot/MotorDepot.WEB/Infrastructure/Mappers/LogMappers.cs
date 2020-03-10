using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class LogMappers
    {
        public static LogViewModel ToViewModel(this LogEventDto log)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LogEventDto, LogViewModel>()
                        .ForMember(x => x.LogType, opt => opt.MapFrom(x => x.LogType));
                })
                .CreateMapper().Map<LogEventDto, LogViewModel>(log);
        }

        public static IEnumerable<LogViewModel> ToViewModel(this IEnumerable<LogEventDto> logs)
        {
            return logs.Select(x => x.ToViewModel());
        }

        public static LogActionDetailsViewModel ToDetailsAction(this LogEventDto log)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LogEventDto, LogActionDetailsViewModel>();

            }).CreateMapper().Map<LogEventDto, LogActionDetailsViewModel>(log);
        }

        public static LogExceptionDetailsViewModel ToDetailsException(this LogEventDto log)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LogEventDto, LogExceptionDetailsViewModel>();

            }).CreateMapper().Map<LogEventDto, LogExceptionDetailsViewModel>(log);
        }
    }
}