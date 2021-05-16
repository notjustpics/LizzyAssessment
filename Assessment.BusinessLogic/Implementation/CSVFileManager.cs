using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Assessment.BusinessLogic.Interface;

namespace Assessment.BusinessLogic.Implementation {
    public class CSVFileManager : ICSVFileManager {
        
        /// <summary>
        /// CSVFileManager constructor
        /// </summary>
        public CSVFileManager() {

        }

        /// <summary>
        /// Search for file files with .csv extension in the provided directory
        /// </summary>
        /// <param name="directoryPath">DirectoryPath e.g C:\ManageAddresses</param>
        /// <returns>String List of matching files</returns>
        public List<string> SearchCSVFile(string directoryPath) {
            var supportedExtentions =  ConfigurationManager.AppSettings.Get("SupportedExtentions");
            var searchFileName = ConfigurationManager.AppSettings.Get("SearchFileName");

            if (Directory.Exists(@directoryPath)) {
                return (Directory.GetFiles(@directoryPath, "*.*", SearchOption.AllDirectories)).
                        Where(f => supportedExtentions.Contains(Path.GetExtension(f).ToLower()) && f.ToLower().Contains(searchFileName.ToLower())).ToList();
            }
            return (null);
        }

        /// <summary>
        /// Read the content of a .csv file
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>String list of file contents</returns>
        public List<string> ReadCSVFile(string filePath) {
            if (File.Exists(@filePath)) {
                FileInfo fileInfo = new FileInfo(filePath);
                return ((fileInfo.Length > 0) ? File.ReadAllLines(@filePath).ToList() : null);
            }
            return (null);
        }

        /// <summary>
        /// Delete .csv file from the provided Path
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>Delete Status</returns>
        public bool DeleteFile(string filePath) {
             if (File.Exists(@filePath)) {
                File.Delete(@filePath);
                return (true);
            }
            return (false);
        }

        /// <summary>
        /// Create .csv file contents
        /// </summary>
        /// <param name="filePath">FilePath e.g C\ManageAddress\CustomerAddressesSorted.csv</param>
        /// <param name="contents">file Contents</param>
        /// <returns>true/false success status</returns>
        public bool CreateCSVFileContents(string filePath, string contents) {
            if (!File.Exists(@filePath)) {
                return (WriteContent(@filePath, contents));
            }
            else {
                if (DeleteFile(filePath)) {
                    return (WriteContent(@filePath, contents));
                }
            }
            return (false);
        }

        ///// <summary>
        ///// Create .csv file contents
        ///// </summary>
        ///// <param name="filePath">FilePath e.g C\ManageAddress\CustomerAddressesSorted.csv</param>
        ///// <param name="contents">file Contents</param>
        ///// <returns>true/false success status</returns>
        //public bool CreateCSVFileContents(string filePath, string contents) {
        //    if (!File.Exists(@filePath)) {
        //        return (WriteContent(@filePath, contents));
        //    }
        //    else {
        //        var path = Path.GetDirectoryName(@filePath);
        //        var fileName = Path.GetFileNameWithoutExtension(@filePath);
        //        var fileResponseName = string.Format(@"{0}\{1}{2}.txt", path, fileName,
        //                                              DateTime.Now.ToString("yyyyMMddhhmmss"));
        //        if (RenameFile(@filePath, @fileResponseName)) {
        //            return (WriteContent(@filePath, contents));
        //        }
        //    }
        //    return (false);
        //}

        /// <summary>
        /// Create base Directory for managing .csv file
        /// </summary>
        /// <param name="directoryPath">DirectoryPath e.g C\ManageAddress\</param>
        /// <returns>true/false success status</returns>
        public bool CreateDirectory(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                var directory = Directory.CreateDirectory(directoryPath);
            }
            return (true);
        }

        /// <summary>
        /// Check for File Size before reading the contents
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>true/false success status</returns>
        public bool CheckFileSize(string filePath) {
            if (File.Exists(@filePath)) {
                FileInfo fileInfo = new FileInfo(filePath);
                return ((fileInfo.Length > 0) ? true : false);
            }
            return (false);
        }

        /// <summary>
        /// Get directory from the provide File Path
        /// </summary>
        /// <param name="filePath">C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>Directory e.g. C\ManageAddress</returns>
        public string GetFileDirectory(string filePath) {
            if (File.Exists(filePath)) {
                var directory = Path.GetDirectoryName(filePath).Split('\\').LastOrDefault();
                return (directory);
            }
            return (null);
        }

        /// <summary>
        /// Rename file name
        /// </summary>
        /// <param name="fullFilePath">FilePath</param>
        /// <param name="newFullFilePath">FilePath</param>
        /// <returns>true/false success status</returns>
        public bool RenameFile(string fullFilePath, string newFullFilePath) {
            File.Move(fullFilePath, newFullFilePath);
            return (true);
        }

        /// <summary>
        /// Use the provided file path and contents, and create file contents
        /// </summary>
        /// <param name="filePath">String FilePath</param>
        /// <param name="contents">String FileContents</param>
        /// <returns>true/false success status</returns>
        public bool WriteContent(string filePath, string contents) {
            File.WriteAllText(@filePath, contents);
            return (true);
        }
    }
}
