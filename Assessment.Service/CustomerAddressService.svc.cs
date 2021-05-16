using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Assessment.BusinessLogic.Interface;

namespace Assessment.Service {
    /// <summary>
    /// WCF Service for manage with Custom Address .csv file
    /// </summary>
    public class CustomerAddressService : ICustomerAddressService {

        private ICustomerAddressManager _customerAddressManager;

        public CustomerAddressService(ICustomerAddressManager iCustomerAddressManagerLogic) {
            _customerAddressManager = iCustomerAddressManagerLogic;
        }

        /// <summary>
        /// Output the sorted customer addresses in a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        public bool CreateSortedCustomerNamesTxtFile() {
            return (_customerAddressManager.CreateSortedCustomerNamesTxtFile());
        }

        /// <summary>
        /// Output the sorted customer addresses to a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        public bool CreateSortedCustomerAddressesTxtFile() {
            return _customerAddressManager.CreateSortedCustomerAddressesTxtFile();
        }

        /// <summary>
        /// Get the base directory for processing customer addresses
        /// </summary>
        /// <returns></returns>
        public string GetBaseDirectory() {
            return (_customerAddressManager.GetBaseDirectory());
        }
    }
}
