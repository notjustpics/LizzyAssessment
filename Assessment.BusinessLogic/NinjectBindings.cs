using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.BusinessLogic.Implementation;
using Assessment.BusinessLogic.Interface;
using Ninject.Modules;

namespace Assessment.BusinessLogic {
    /// <summary>
    /// Ninject bindings
    /// </summary>
    public class NinjectBindings : NinjectModule {
        public override void Load() {
            Bind<ICSVFileManager>().To<CSVFileManager>();
            Bind<ICustomerAddressManager>().To<CustomerAddressManager>();
        }
    }
}
