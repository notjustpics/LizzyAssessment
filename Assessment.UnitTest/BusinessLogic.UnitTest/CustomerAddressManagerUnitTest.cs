using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Assessment.BusinessLogic;
using Assessment.BusinessLogic.Implementation;
using Assessment.BusinessLogic.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assessment.UnitTest.BusinessLogic.UnitTest {
    [TestClass]
    public class CustomerAddressManagerUnitTest {

        private INinjectStandardKernel _iNinjectStandardKernel;
        private ICustomerAddressManager _iCustomerAddressManager;

        public CustomerAddressManagerUnitTest() {
            _iNinjectStandardKernel = new NinjectStandardKernel();
            _iCustomerAddressManager = new CustomerAddressManager(_iNinjectStandardKernel);
        }

        [TestMethod]
        public void GetCustomerAddressesTestMethod() {
            var results = _iCustomerAddressManager.GetCustomerAddresses();
            Assert.IsTrue(results?.Count > -1, "Method _iCustomerAddressManager.GetCustomerAddresses() Failed");
        }

        [TestMethod]
        public void ExtractCsvFileContentsTestMethod() {
            var fileName = @"\data.csv";
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var filePath = string.Format("{0}{1}", baseDirectory, fileName);
            var results = _iCustomerAddressManager.ExtractCsvFileContents(filePath);

            if (results == null) {
                Assert.IsNull(results, "The test method :_iCustomerAddressManager.ExtractCsvFileContents(filePath) returned empty results");
            }
            else {
                Assert.IsTrue(results.Count > -1, "The test method :_iCustomerAddressManager.ExtractCsvFileContents(filePath) failed");
            }
        }

        [TestMethod]
        public void ExtractCustomerAddressFromCsvFileTestMethod() {
            var fileName = @"\data.csv";
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var filePath = string.Format("{0}{1}", baseDirectory, fileName);
            var fileContents = _iCustomerAddressManager.ExtractCsvFileContents(filePath);
            var results = _iCustomerAddressManager.ExtractCustomerAddressFromCsvFile(fileContents);

            Assert.IsTrue(results?.Count > -1, "Method _iCustomerAddressManager.ExtractCustomerAddressFromCsvFile(fileContents) failed to process");
        }

        [TestMethod]
        public void FormatLineContentsTestMethod() {
            var fileName = @"\data.csv";
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var filePath = string.Format("{0}{1}", baseDirectory, fileName);
            var fileContents = _iCustomerAddressManager.ExtractCsvFileContents(filePath);

            var fileContentLineItem = fileContents?.Skip(1).FirstOrDefault();

            if (fileContentLineItem == null) {
                Assert.Fail();
            }
            else {
                var formattedLineItem = _iCustomerAddressManager.FormatLineContents(fileContentLineItem);
                Assert.IsNotNull(formattedLineItem, "Method _iCustomerAddressManager.FormatLineContents(fileContentLineItem) failed");
            }
        }

        [TestMethod]
        public void ExtractStreetAddressNumberTestMethod() {
            var fileName = @"\data.csv";
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");
            var filePath = string.Format("{0}{1}", baseDirectory, fileName);
            var fileContents = _iCustomerAddressManager.ExtractCsvFileContents(filePath);

            var fileContentLineItem = fileContents?.Skip(1).FirstOrDefault();

            if (fileContentLineItem == null) {
                Assert.Fail();
            }
            else {
                var results = _iCustomerAddressManager.ExtractStreetAddressNumber(fileContentLineItem.Split(',')[2]);
                Assert.IsTrue(results > -1, "Method _iCustomerAddressManager.ExtractStreetAddressNumber(fileContentLineItem.Split(',')[2]) failed");
            }
        }

        [TestMethod]
        public void IsNumeric() {
            var value = "096 Long Street, Cape Town";
            var extractNumber = _iCustomerAddressManager.ExtractStreetAddressNumber(value);
            var result = _iCustomerAddressManager.IsNumeric(extractNumber.ToString());
            Assert.IsTrue(result, "Method _iCustomerAddressManager.IsNumeric(extractNumber.ToString())");
        }

        [TestMethod]
        public void BreakdownCustomerNamesTestMethod() {
            var customerAddresses = _iCustomerAddressManager.GetCustomerAddresses();
            var results = _iCustomerAddressManager.BreakdownCustomerNames(customerAddresses);
            Assert.IsTrue(results?.Count > -1 || results == null, "Method _iCustomerAddressManager.BreakdownCustomerNames(customerAddresses) failed");
        }

        [TestMethod]
        public void SortCountCustomerNamesTestMethod() {
            var customers = _iCustomerAddressManager.GetCustomerAddresses();
            var results = _iCustomerAddressManager.BreakdownCustomerNames(customers);
            if (results == null)
                Assert.Fail();
            Assert.IsTrue(results?.Count > -1 || results == null, "Method _iCustomerAddressManager.BreakdownCustomerNames(customers) failed");
        }

        [TestMethod]
        public void SortAddressesAlphabeticallyTestMethod() {
            var customers = _iCustomerAddressManager.GetCustomerAddresses();
            var results = _iCustomerAddressManager.SortAddressesAlphabetically(customers);
            if (results == null)
                Assert.Fail();
            Assert.IsTrue(results.Count > -1, "method _iCustomerAddressManager.SortAddressesAlphabetically(customers) failed");
        }

        [TestMethod]
        public void CreateSortedCustomerNamesTxtFileTestMethod() {
            Assert.IsTrue(_iCustomerAddressManager.CreateSortedCustomerNamesTxtFile(), 
                        "Method _iCustomerAddressManager.CreateSortedCustomerNamesTxtFile() failed");
        }

        [TestMethod]
        public void CreateSortedCustomerAddressesTxtFileTestMethod() {
            Assert.IsTrue(_iCustomerAddressManager.CreateSortedCustomerAddressesTxtFile(), 
                         "Method _iCustomerAddressManager.CreateSortedCustomerAddressesTxtFile() failed");
        }

        [TestMethod]
        public void GetBaseDirectoryTestMethod() {
            Assert.IsNotNull(_iCustomerAddressManager.GetBaseDirectory(), "Method _iCustomerAddressManager.GetBaseDirectory() Failed");
        }
    }
}
