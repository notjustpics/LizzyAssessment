using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Assessment.Service {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerAddressService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerAddressService {
        [OperationContract]
        /// <summary>
        /// Output the sorted customer addresses in a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        bool CreateSortedCustomerNamesTxtFile();

        [OperationContract]
        /// <summary>
        /// Output the sorted customer addresses to a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        bool CreateSortedCustomerAddressesTxtFile();

        [OperationContract]
        /// <summary>
        /// Get the base directory for processing customer addresses
        /// </summary>
        /// <returns></returns>
        string GetBaseDirectory();
    }
}
