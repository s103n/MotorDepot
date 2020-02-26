using MotorDepot.DAL.Data;
using MotorDepot.DAL.Interfaces;
using Ninject.Modules;

namespace MotorDepot.BLL.Util
{
    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("MotorDepot");
        }
    }
}
