using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BusinessLogic.Interface {
    /// <summary>
    /// Interface for Managing interaction with CSV File
    /// </summary>
    public interface ICSVFileManager {

        /// <summary>
        /// Search for file files with .csv extension in the provided directory
        /// </summary>
        /// <param name="directoryPath">DirectoryPath e.g C:\ManageAddresses</param>
        /// <returns>String List of matching files</returns>
        List<string> SearchCSVFile(string directoryPath);

        /// <summary>
        /// Read the content of a .csv file
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>String list of file contents</returns>
        List<string> ReadCSVFile(string filePath);

        /// <summary>
        /// Delete .csv file from the provided Path
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>Delete Status</returns>
        bool DeleteFile(string filePath);

        /// <summary>
        /// Create .csv file contents
        /// </summary>
        /// <param name="filePath">FilePath e.g C\ManageAddress\CustomerAddressesSorted.csv</param>
        /// <param name="contents">file Contents</param>
        /// <returns>true/false success status</returns>
        bool CreateCSVFileContents(string filePath, string contents);

        /// <summary>
        /// Create base Directory for managing .csv file
        /// </summary>
        /// <param name="directoryPath">DirectoryPath e.g C\ManageAddress\</param>
        /// <returns>true/false success status</returns>
        bool CreateDirectory(string directoryPath);

        /// <summary>
        /// Check for File Size before reading the contents
        /// </summary>
        /// <param name="filePath">FilePath e.g. C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>true/false success status</returns>
        bool CheckFileSize(string filePath);

        /// <summary>
        /// Get directory from the provide File Path
        /// </summary>
        /// <param name="filePath">C\ManageAddress\CustomerAddresses.csv</param>
        /// <returns>Directory e.g. C\ManageAddress</returns>
        string GetFileDirectory(string filePath);

        /// <summary>
        /// Rename file name
        /// </summary>
        /// <param name="fullFilePath">FilePath</param>
        /// <param name="newFullFilePath">FilePath</param>
        /// <returns>true/false success status</returns>
        bool RenameFile(string fullFilePath, string newFullFilePath);

        /// <summary>
        /// Use the provided file path and contents, and create file contents
        /// </summary>
        /// <param name="filePath">String FilePath</param>
        /// <param name="contents">String FileContents</param>
        /// <returns>true/false success status</returns>
        bool WriteContent(string filePath, string contents);
    }
}
