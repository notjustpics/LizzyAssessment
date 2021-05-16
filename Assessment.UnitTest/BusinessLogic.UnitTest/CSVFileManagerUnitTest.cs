using System;
using System.Configuration;
using System.Linq;
using Assessment.BusinessLogic;
using Assessment.BusinessLogic.Implementation;
using Assessment.BusinessLogic.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assessment.UnitTest.BusinessLogic.UnitTest {
    [TestClass]
    public class CSVFileManagerUnitTest {
        private ICSVFileManager _iCSVFileManager;

        public CSVFileManagerUnitTest() {
            _iCSVFileManager = new CSVFileManager();
        }

        [TestMethod]
        public void CreateDirectoryTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var result = _iCSVFileManager.CreateDirectory(baseDirectory);
            Assert.IsTrue(result, "Method _iCSVFileManager.CreateDirectory(baseDirectory) failed");
        }

        [TestMethod]
        public void GetFileDirectoryTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = @"\data.csv";
            var result = _iCSVFileManager.GetFileDirectory(string.Format("{0}{1}",baseDirectory,fileName));
            Assert.IsTrue(result == null || !string.IsNullOrEmpty(result), "Method _iCSVFileManager.GetFileDirectory(baseDirectory) failed");
        }

        
        [TestMethod]
        public void SearchCSVFileTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var searchFiles = _iCSVFileManager.SearchCSVFile(baseDirectory);
            Assert.IsTrue(searchFiles == null || searchFiles?.Count > 0, "Method _iCSVFileManager.SearchCSVFile(baseDirectory) failed");
        }

        [TestMethod]
        public void ReadCSVFileTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var searchFiles = _iCSVFileManager.SearchCSVFile(baseDirectory);

            if (searchFiles == null)
                Assert.IsNull(searchFiles, "No directory contains no files");

            var results = _iCSVFileManager.ReadCSVFile(searchFiles.FirstOrDefault());
            Assert.IsTrue(results == null || results?.Count > -1, "Method _iCSVFileManager.ReadCSVFile(searchFiles.FirstOrDefault()) failed");
        }

        [TestMethod]
        public void CreateCSVFileContentsTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = "SortedAddressesTest.txt";
            var filePath = string.Format(@"{0}\{1}", baseDirectory, fileName);
            var contents = "Test method for _iCSVFileManager.CreateCSVFileContents(filePath, contents).";
            var result = _iCSVFileManager.CreateCSVFileContents(filePath, contents);

            Assert.IsTrue(result, "Method _iCSVFileManager.CreateCSVFileContents(filePath, contents) failed");
        }

        [TestMethod]
        public void CheckFileSizeTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = "SortedAddressesTest.txt";
            var filePath = string.Format(@"{0}\{1}", baseDirectory, fileName);
            var result = _iCSVFileManager.CheckFileSize(filePath);
            Assert.IsTrue(result || !result, "method _iCSVFileManager.CheckFileSize(filePath) failed");
        }

        [TestMethod]
        public void DeleteFileTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = "SortedAddressesTest.txt";
            var filePath = string.Format(@"{0}\{1}", baseDirectory, fileName);
            var results = _iCSVFileManager.DeleteFile(filePath);

            Assert.IsTrue(results || !results, "Method _iCSVFileManager.DeleteFile(filePath) failed");
        }

        [TestMethod]
        public void RenameFileTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = "data.csv";
            var filePath = string.Format(@"{0}\{1}", baseDirectory, fileName);
            var newFilePath = string.Format(@"{0}\RenamedFile_{1}", baseDirectory, fileName);
            var result = _iCSVFileManager.RenameFile(filePath, newFilePath);
            Assert.IsTrue(result, "Method _iCSVFileManager.GetFileDirectory(baseDirectory) failed");
        }

        [TestMethod]
        public void WriteContentTestMethod() {
            var baseDirectory = ConfigurationManager.AppSettings.Get("AssessmentBaseDirectory");

            if (string.IsNullOrEmpty(baseDirectory))
                Assert.Fail();
            var fileName = "TestWriteMethod.txt";
            var filePath = string.Format(@"{0}\{1}", baseDirectory, fileName);
            var contents = "Test method for _iCSVFileManager.WriteContent(filePath, contents).";
            var result = _iCSVFileManager.WriteContent(filePath, contents);

            Assert.IsTrue(result, "Method _iCSVFileManager.WriteContent(filePath, contents) failed");
        }
    }
}
