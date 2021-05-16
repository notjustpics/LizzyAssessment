using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.BusinessLogic.DTO;
using Assessment.BusinessLogic.Interface;
using Ninject;

namespace Assessment.BusinessLogic.Implementation {
    /// <summary>
    /// Customer Address interface implementation, exposes functionality for processing customer addresses and names
    /// </summary>
    public class CustomerAddressManager : ICustomerAddressManager {
        private INinjectStandardKernel _iNinjectStarndardKernel;
        private ICSVFileManager _csvFileManager;

        /// <summary>
        /// CustomerAddressManager Constructor, it accept Ninject Kernel
        /// </summary>
        /// <param name="injectNinjectStandardKernel">INinjectStandardKernel</param>
        public CustomerAddressManager(INinjectStandardKernel injectNinjectStandardKernel) {
            _iNinjectStarndardKernel = injectNinjectStandardKernel;
            //Create Ninject Kernel and get interface bindings
            IKernel kernel = _iNinjectStarndardKernel.GetStandardKernel();
            _csvFileManager = kernel.Get<ICSVFileManager>();
        }

        /// <summary>
        /// Get list of customer address from .csv files
        /// </summary>
        /// <returns>List of customer addresse</returns>
        public List<CustomerAddress> GetCustomerAddresses() {
            var customerAddresses = new List<CustomerAddress>();
            var rootDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var files = _csvFileManager.SearchCSVFile(rootDirectory);

            foreach (var filePath in files) {
                if (_csvFileManager.CheckFileSize(filePath)) {
                    var filecontents = _csvFileManager.ReadCSVFile(filePath);
                    customerAddresses.AddRange(ExtractCustomerAddressFromCsvFile(filecontents));
                }
            }
            return (customerAddresses);
        }

        /// <summary>
        /// Use the extracted list of contents from .csv file to generate customers
        /// </summary>
        /// <param name="csvFileContents">csvFileContents</param>
        /// <returns>List of Customer Addresses</returns>
        public List<CustomerAddress> ExtractCustomerAddressFromCsvFile(List<string> csvFileContents) {
            var customerAddresses = new List<CustomerAddress>();
            //Assumes that the first line of the contents is a hearder
            var fileContents = csvFileContents.Skip(1).ToList();

            fileContents.ForEach(lineItem => {
                customerAddresses.Add(FormatLineContents(lineItem));
            });
            return (customerAddresses);
        }

        /// <summary>
        /// Format the provided line item into CustomerAddress
        /// </summary>
        /// <param name="lineContent">string .csv line content</param>
        /// <returns>CustomerAddress</returns>
        public CustomerAddress FormatLineContents(string lineContent) {
            var lineItemColumns = lineContent.Split(',');
            var streetNumber = ExtractStreetAddressNumber(lineItemColumns[2]);
            var streetName = lineItemColumns[2];

            var contactNumber = 0L;
            if (IsNumeric(lineItemColumns[3]))
                contactNumber = long.Parse(lineItemColumns[3]);

            if (streetNumber > 0)
                streetName = streetName.Replace(streetNumber.ToString(), string.Empty);

            var customerAddress = new CustomerAddress() {
                FirstName = lineItemColumns[0],
                LastName = lineItemColumns[1],
                AddressStreetNumber = streetNumber,
                AddressStreetName = streetName,
                PhoneNumber = contactNumber
            };
            return (customerAddress);
        }

        /// <summary>
        /// Extract street number from address column
        /// </summary>
        /// <param name="addressColumn">Address Column</param>
        /// <returns>Street Number</returns>
        public int ExtractStreetAddressNumber(string addressColumn) {
            if (IsNumeric(addressColumn.Substring(0, 1))) {
                var extractStreetNumber = addressColumn.SkipWhile(c => !Char.IsDigit(c)).TakeWhile(Char.IsDigit).ToArray();
                return (int.Parse(new string(extractStreetNumber)));
            }
            return 0;
        }

        /// <summary>
        /// Check if the first character of address is string
        /// </summary>
        /// <param name="firstCharacterOfAddress">FirstCharacterOfAddress</param>
        /// <returns>true/false success status</returns>
        public bool IsNumeric(string firstCharacterOfAddress) {
            long output;
            return (long.TryParse(firstCharacterOfAddress, out output));
        }

        /// <summary>
        /// Create a list of customer firstname and lastname
        /// </summary>
        /// <param name="customerAddresses">CustomerAddress</param>
        /// <returns>List of customer names</returns>
        public List<string> BreakdownCustomerNames(List<CustomerAddress> customerAddresses) {
            var customerNames = new List<string>();
            foreach (var customer in customerAddresses) {
                customerNames.Add(customer.FirstName);
                customerNames.Add(customer.LastName);
            }
            return (customerNames);
        }

        /// <summary>
        /// Sort customer names per frequency
        /// </summary>
        /// <param name="customerNames">List of customer Names</param>
        /// <returns>Sorted Customer Names</returns>
        public List<string> SortCountCustomerNames(List<string> customerNames) {
            var sortedNames = new List<string>();
            var groupedNames = customerNames.GroupBy(x => x).Select(group =>
                               new {
                                   Name = group.Key,
                                   Count = group.Count()
                               }).OrderByDescending(x => x.Count).ThenBy(x => x.Name).ToList();
            groupedNames.ForEach(row => {
                sortedNames.Add(string.Format("{0},{1}", row.Name, row.Count));
            });
            return (sortedNames);
        }

        /// <summary>
        /// Sort Addresses Alphabetically
        /// </summary>
        /// <param name="customerAddresses">List of addresses</param>
        /// <returns>Sorted Address</returns>
        public List<CustomerAddress> SortAddressesAlphabetically(List<CustomerAddress> customerAddresses) {
            return (customerAddresses.OrderBy(x => x.AddressStreetName).ToList());
        }

        /// <summary>
        /// Output the sorted customer addresses in a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        public bool CreateSortedCustomerNamesTxtFile() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var contents = string.Empty;
            string responseFilePath = string.Format(@"{0}\SortedCustomerNames.txt", baseDirectory);
            var customerNames = BreakdownCustomerNames(GetCustomerAddresses());
            var sortCountCustomerNames = SortCountCustomerNames(customerNames);

            foreach (var line in sortCountCustomerNames) {
                contents = contents + string.Format("{0} \n", line);
            }
            return (_csvFileManager.CreateCSVFileContents(responseFilePath, contents));
        }

        /// <summary>
        /// Output the sorted customer addresses to a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        public bool CreateSortedCustomerAddressesTxtFile() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            string responseFilePath = string.Format(@"{0}\SortedCustomerAddresses.txt", baseDirectory);
            var contents = string.Empty;
            var fileContents = SortAddressesAlphabetically(GetCustomerAddresses());

            foreach (var line in fileContents) {
                contents = contents + string.Format("{0} {1}\n", line.AddressStreetNumber, line.AddressStreetName);
            }
            return (_csvFileManager.CreateCSVFileContents(responseFilePath, contents));
        }

        /// <summary>
        /// Get the base directory for processing customer addresses
        /// </summary>
        /// <returns></returns>
        public string GetBaseDirectory() {
            return (ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory"));
        }

        /// <summary>
        /// Extract .csv file contents
        /// </summary>
        /// <returns></returns>
        public List<string> ExtractCsvFileContents(string filePath) {
            if (_csvFileManager.CheckFileSize(filePath))
                return (_csvFileManager.ReadCSVFile(filePath));
            return null;
        }
    }
}
