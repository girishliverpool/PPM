using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace PPMWebApplication
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    return base.GetControllerInstance(requestContext, controllerType);
                }

                var controller = ObjectFactory.GetInstance(controllerType) as Controller;
                controller.ActionInvoker = new StructureMapActionInvoker();
                return controller;
            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }

        }
    }
}