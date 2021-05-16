using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace Assessment.ConsoleApplication {
    public class NinjectBindings : NinjectModule {
        public override void Load() {
            Bind<CustomerAddressServiceReference.ICustomerAddressService>().To<CustomerAddressServiceReference.CustomerAddressServiceClient>();
        }
    }
}
