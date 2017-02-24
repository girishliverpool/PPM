using System.Web.Http.Controllers;
//using Raven.Client;
//using Raven.Client.Embedded;
using StructureMap.Configuration.DSL;
//using ContactManager.Controllers;

namespace PPM.DependencyInjection
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(p =>
            {
                p.TheCallingAssembly();
                p.AddAllTypesOf<IHttpController>();
            });
        }
    }
}