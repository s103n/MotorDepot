using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities.Logging;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class LogMappers
    {
        public static LogException ToEntity(this ExceptionDto model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<ExceptionDto, LogException>())
                .CreateMapper().Map<ExceptionDto, LogException>(model);
        }

        public static ExceptionDto ToDto(this LogException model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<LogException, ExceptionDto>())
                .CreateMapper().Map<LogException, ExceptionDto>(model);
        }
    }
}
