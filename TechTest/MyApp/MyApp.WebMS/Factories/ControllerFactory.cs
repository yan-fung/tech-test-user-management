using System;
using System.Web.Mvc;
using MyApp.Services.Factories.Implementations;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;

namespace MyApp.WebMS.Factories
{
    public class ControllerFactory : DefaultControllerFactory
    {
        // Keep a static instance of the service factory so the data layer is maintained in memory
        private static readonly IServiceFactory _serviceFactory = new ServiceFactory();

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = Activator.CreateInstance(controllerType, new [] { _serviceFactory }) as BaseController;

            return controller;
        }
    }
}