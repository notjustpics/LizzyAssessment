using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.BusinessLogic.Interface;
using Ninject;

namespace Assessment.BusinessLogic {
    /// <summary>
    /// Ninject Kernel implemetation
    /// </summary>
    public class NinjectStandardKernel : INinjectStandardKernel {

        /// <summary>
        /// Get Ninject Kernel bindings
        /// </summary>
        /// <returns></returns>
        public IKernel GetStandardKernel() {
            IKernel kernel = new StandardKernel(new NinjectBindings());
            return (kernel);
        }
    }
}
