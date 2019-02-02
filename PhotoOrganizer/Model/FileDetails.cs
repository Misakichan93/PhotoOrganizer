using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Model
{
    public class FileDetails
    {
        private string _sourcePath;
        private string _destinationPath;
        private string _fileName;
        private string _device;
        private DateTime _createDate;
        private int _monthOfCreate;
        private int _yearOfCreate;
        private FileInfo _fileInfo;

        public string SouceFilePath { get => _sourcePath; }
        public string DestinationPath { get => _destinationPath; }
        public string FileName { get => _fileName; }
        public string Device { get => _device; }
        public DateTime CreateDate { get => _createDate; }
        public int MonthOfCreate { get => _monthOfCreate;  }
        public int YearOfCreate { get => _yearOfCreate; }

        public FileDetails(string filePath)
        {
            _fileInfo = new FileInfo(filePath);
            _sourcePath = filePath;
            _fileName = Path.GetFileName(filePath);                       
        }

        private string GetMonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        private void SetPhotoInfo()
        {
            ShellObject picture = ShellObject.FromParsingName(_sourcePath);
            var createDate = picture.Properties.GetProperty(SystemProperties.System.Photo.DateTaken).ValueAsObject?.ToString() ?? _fileInfo.CreationTime.Date.ToString();
            _createDate = DateTime.Parse(createDate);
            _monthOfCreate = _fileInfo.CreationTime.Month;
            _yearOfCreate = _fileInfo.CreationTime.Year;

            var camera = picture.Properties.GetProperty(SystemProperties.System.Photo.CameraManufacturer)?.ValueAsObject?.ToString();
            var cameraModel = picture.Properties.GetProperty(SystemProperties.System.Photo.CameraModel).ValueAsObject?.ToString();
            _device = $@"{camera}{((string.IsNullOrWhiteSpace(camera) || string.IsNullOrWhiteSpace(cameraModel)) ? string.Empty : "_")}{cameraModel}";

            _destinationPath = Path.Combine(_yearOfCreate.ToString(), _monthOfCreate.ToString(), GetMonthName(_monthOfCreate), _device, _fileName);
        }
    }
}
