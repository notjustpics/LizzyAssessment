using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ninject;

namespace Assessment.ConsoleApplication {
    /// <summary>
    /// This Program generate the two(2) output (.txt) files for SortedCustomerAddresses.txt (file) and SortedCustomerNames.txt (file)
    /// It uses WCF Service to process the files, and the service is injectcted into the program using Ninject
    /// </summary>
    class Program {

        private static CustomerAddressServiceReference.ICustomerAddressService  _customerAddressServiceClient;
        static void Main(string[] args) {
            IKernel kernel = new StandardKernel(new NinjectBindings());
            _customerAddressServiceClient = kernel.Get<CustomerAddressServiceReference.ICustomerAddressService>();

            var baseDirectory = _customerAddressServiceClient.GetBaseDirectory();

            WriteLine(string.Format("Process to show the frequency of the first and last names ordered by frequency " +
                                     "descending and then alphabetically ascending started. The processed results will" +
                                     " be available in {0} folder.", baseDirectory));
            _customerAddressServiceClient.CreateSortedCustomerNamesTxtFile();
            WriteLine("Process successfully completed.");
            WriteLine(Environment.NewLine);

            WriteLine(string.Format("Process to show the addresses sorted alphabetically by street name started. " +
                                    "The processed results will be available in {0} folder.", baseDirectory));
            _customerAddressServiceClient.CreateSortedCustomerAddressesTxtFile();
            WriteLine("Process successfully completed.");
            WriteLine(Environment.NewLine);

            WriteLine(string.Format("Processes to generate files successfully ended. \n\n" +
                      "Press any key to exit the program and view the generated out file in {0} folder.", baseDirectory));
            ReadKey();
        }
    }
}
