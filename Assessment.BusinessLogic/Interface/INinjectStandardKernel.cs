using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Assessment.BusinessLogic.Interface {
    /// <summary>
    /// Interface for Ninject Ikernel
    /// </summary>
    public interface INinjectStandardKernel {

        /// <summary>
        /// Get Ninject Kernel bindings
        /// </summary>
        /// <returns></returns>
        IKernel GetStandardKernel();
    }
}
