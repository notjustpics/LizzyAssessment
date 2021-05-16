using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BusinessLogic.DTO {
    public class CustomerAddress {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressStreetNumber { get; set; }
        public string AddressStreetName { get; set; }
        public long PhoneNumber { get; set; }
    }
}
