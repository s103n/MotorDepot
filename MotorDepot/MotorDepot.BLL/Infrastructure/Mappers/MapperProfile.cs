using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public class MapperProfile : Profile
    {

        public override string ProfileName => GetType().Name;
    }
}
