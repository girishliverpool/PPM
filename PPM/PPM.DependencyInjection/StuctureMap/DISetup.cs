using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.DataLayer.Interfaces;
using PPM.DataLayer;
using PPM.Service.Services;
using PPM.ServiceInterfaces;
using StructureMap;
using PPM.Dto;
using System.Threading;

namespace PPM.DependencyInjection
{
    public static class DISetup
    {
        public static void Configuration()
        {
            var frameworkSettings = FrameworkSetup.GetSetFrameworkSettings();

            ObjectFactory.Initialize(x =>
            {
                x.For<IPPMTransactionService>().Use<TransactionDataLayer>();
                x.For<ITransactionService>().Use<TransactionService>();
                //x.For<FrameworkSettings>().Use(frameworkSettings);
            });
        }

        public static T GetInstance<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
