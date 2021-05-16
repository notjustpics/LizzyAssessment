using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Assessment.BusinessLogic;
using Assessment.BusinessLogic.Implementation;
using Assessment.BusinessLogic.Interface;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;

namespace Assessment.Service {
    public class Global : NinjectHttpApplication {
        protected override IKernel CreateKernel() {
            var kernel = new StandardKernel();
            kernel.Bind<ICustomerAddressManager>().To<CustomerAddressManager>();
            kernel.Bind<INinjectStandardKernel>().To<NinjectStandardKernel>();
            return kernel;
        }
    }
}