using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class AutoMappers
    {
        public static AutoDto ToAutoDto(this Auto auto)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Auto, AutoDto>()
            });
        }
    }
}
