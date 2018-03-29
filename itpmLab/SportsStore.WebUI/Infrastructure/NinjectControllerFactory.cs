using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Routing;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure {
    //реалізація користувацької фабрики контроллерів
    //наслідується від фабрики по замовчуванню
    public class NinjectControllerFactory : DefaultControllerFactory {               
        private IKernel ninjectKernel;
        public NinjectControllerFactory() {
            //створення котейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            //отримання обєкта контроллера
            //використовуючи його тип
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings() {
            //конфігурація контейнера    
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>(); 
        }
    }
}