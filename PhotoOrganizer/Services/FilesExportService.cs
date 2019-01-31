using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Services
{
    public class FilesExportService
    {
        private string _sourcePath;
        private string _destinationPath;       

        private FilesPrivider _filesPrivider;

        public FilesExportService(string sourcePath,  string destinationPath)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;            
        }

        public void ExportAndReoganizeFiles()
        {
            FilesPrivider filesPrivider = new FilesPrivider(_sourcePath);
            IEnumerable<string> filesSourcePaths = filesPrivider.FilesPaths;

            List<Model.FileDetails> filesDetails = new List<Model.FileDetails>();

            foreach (var filePath in filesSourcePaths)
            {
                filesDetails.Add(new Model.FileDetails(filePath));
            }
        }
    }
}
