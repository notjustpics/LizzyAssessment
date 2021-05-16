using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.BusinessLogic.DTO;

namespace Assessment.BusinessLogic.Interface {    
    /// <summary>
    /// Interface for interacting with Customer Addresses
    /// </summary>
    public interface ICustomerAddressManager {

        /// <summary>
        /// Get list of customer address from .csv file
        /// </summary>
        /// <returns>List of customer addresse</returns>
        List<CustomerAddress> GetCustomerAddresses();

        /// <summary>
        /// Use the extracted list of contents from .csv file to generate customers
        /// </summary>
        /// <param name="csvFileContents">csvFileContents</param>
        /// <returns>List of Customer Addresses</returns>
        List<CustomerAddress> ExtractCustomerAddressFromCsvFile(List<string> csvFileContents);

        /// <summary>
        /// Format the provided line item into CustomerAddress
        /// </summary>
        /// <param name="lineContent">string .csv line content</param>
        /// <returns>CustomerAddress</returns>
        CustomerAddress FormatLineContents(string lineContent);

        /// <summary>
        /// Extract street number from address column
        /// </summary>
        /// <param name="addressColumn">Address Column</param>
        /// <returns>Street Number</returns>
        int ExtractStreetAddressNumber(string addressColumn);

        /// <summary>
        /// Check if the first character of address is string
        /// </summary>
        /// <param name="firstCharacterOfAddress">FirstCharacterOfAddress</param>
        /// <returns>true/false success status</returns>
        bool IsNumeric(string firstCharacterOfAddress);


        /// <summary>
        /// Create a list of customer firstname and lastname
        /// </summary>
        /// <param name="customerAddresses">CustomerAddress</param>
        /// <returns>List of customer names</returns>
        List<string> BreakdownCustomerNames(List<CustomerAddress> customerAddresses);


        /// <summary>
        /// Sort customer names per frequency
        /// </summary>
        /// <param name="customerNames">List of customer Names</param>
        /// <returns>Sorted Customer Names</returns>
        List<string> SortCountCustomerNames(List<string> customerNames);

        /// <summary>
        /// Sort Addresses Alphabetically
        /// </summary>
        /// <param name="customerAddresses">List of addresses</param>
        /// <returns>Sorted Address</returns>
        List<CustomerAddress> SortAddressesAlphabetically(List<CustomerAddress> customerAddresses);

        /// <summary>
        /// Output the sorted customer addresses in a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        bool CreateSortedCustomerNamesTxtFile();


        /// <summary>
        /// Output the sorted customer addresses to a .txt file
        /// </summary>
        /// <returns>.txt file</returns>
        bool CreateSortedCustomerAddressesTxtFile();

        /// <summary>
        /// Get the base directory for processing customer addresses
        /// </summary>
        /// <returns></returns>
        string GetBaseDirectory();

        /// <summary>
        /// Extract .csv file contents
        /// </summary>
        /// <returns></returns>
        List<string> ExtractCsvFileContents(string filePath);


    }
}
