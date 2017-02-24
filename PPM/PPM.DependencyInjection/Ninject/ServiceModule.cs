using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.DataLayer.Interfaces;
using PPM.DataLayer;
using PPM.Service.Services;
using PPM.ServiceInterfaces;

namespace PPM.DependencyInjection
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITransactionService>().To<TransactionService>();
            Bind<IPPMTransactionService>().To<TransactionDataLayer>();
        }
    }
}
