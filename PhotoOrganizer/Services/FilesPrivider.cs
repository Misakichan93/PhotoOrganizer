using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Services
{
    public class FilesPrivider
    {
        private List<string> _filesPaths;
        private string _sourcePath;

        public IEnumerable<string> FilesPaths
        {
            get {

                if(_filesPaths.Count()==0)
                {
                    GetFilesFromSourceDirectory(_sourcePath);
                }
                return _filesPaths;
            }
        }

        public FilesPrivider(string sourcePath)
        {
            _sourcePath = sourcePath;
            _filesPaths = new List<string>();
        }

        private void GetFilesFromSourceDirectory(string sourcePath)
        {
            if(Directory.Exists(sourcePath))
            {
                var directoryPaths = Directory.GetDirectories(sourcePath).ToList();
                foreach(var directoryPath in directoryPaths)
                {
                    GetFilesFromSourceDirectory(directoryPath);
                }
            }
            _filesPaths.AddRange(Directory.GetFiles(sourcePath).ToList());
        }

    }
}
