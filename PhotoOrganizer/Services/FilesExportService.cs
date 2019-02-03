using PhotoOrganizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
            FilesPrivider sourceFilesPrivider = new FilesPrivider(_sourcePath);           
         

            List<Model.FileDetails> filesSource = CreateFileDetailsFromPaths(sourceFilesPrivider.FilesPaths);

            var yearGroups = filesSource.GroupBy(y => y.YearOfCreate);

            foreach (var yearGroup in yearGroups)
            {                
                var monthGroups = yearGroup.GroupBy(m => m.MonthOfCreate);

                foreach (var monthGroup in monthGroups)
                {
                    string monthDirectory = Path.Combine(_destinationPath, yearGroup.Key.ToString(), monthGroup.Key.ToString());

                    Task.Run(() => CopyFiles(monthDirectory, monthGroup));
                }
            }
        }

        private List<Model.FileDetails>CreateFileDetailsFromPaths(IEnumerable<string> filesPaths)
        {
            List<Model.FileDetails> filesDetails = new List<FileDetails>();

            foreach (var filePath in filesPaths)
            {
                filesDetails.Add(new Model.FileDetails(filePath, _destinationPath));
            }

            filesDetails = filesDetails.Where(x => ImageFormat.imageExtensions.Contains(x.Extension)).ToList();

            return filesDetails;
        }        

        private async Task CopyFiles(string monthDirectory, IEnumerable<Model.FileDetails>files)
        {
            FilesPrivider destinationFilesProvider = new FilesPrivider(_destinationPath);
            CopyService copyService = new CopyService();

            if (destinationFilesProvider.FilesPaths.Count() > 0)
            {
                files.ToList().ForEach(x => x.AddDeviceToPath());
            }

            List<Model.FileDetails> filesInDestination = CreateFileDetailsFromPaths(destinationFilesProvider.FilesPaths);
            foreach (var file in files)
            {
                Task.Run(() => copyService.CopyFile(file));
            }

            foreach (var file in filesInDestination)
            {
                if (file.IsFilePathContainDevice() == false)
                {
                    file.AddDeviceToPath();
                    Task.Run(() => copyService.MoveFile(file));
                }
            }
        }
    }
}
