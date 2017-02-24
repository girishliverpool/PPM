using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPMWebApplication
{
    public class StructureMapActionInvoker : ControllerActionInvoker
    {
        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            foreach (IActionFilter actionFilter in filters)
            {
                ObjectFactory.BuildUp(actionFilter);
            }

            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }
    }
}