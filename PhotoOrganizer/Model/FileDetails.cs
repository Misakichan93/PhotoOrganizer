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

        private string _destinationPathPrefix;
        private string _destinationPathSufix;

        private string _fileName;
        private string _device;
        private DateTime _createDate;
        private int _monthOfCreate;
        private int _yearOfCreate;
        private double _size;
        private string _extension;
        private FileInfo _fileInfo;

        public string SouceFilePath { get => _sourcePath; }

        public string DestinationPathPrefix { get => _destinationPathPrefix; }
        public string DestinationPathSufix { get => _destinationPathSufix; }        
        public string DestinationPathWithFileName { get { return Path.Combine(_destinationPathPrefix, _destinationPathSufix, _fileName); } }

        public string FileName { get => _fileName; }
        public string Device { get => _device; }
        public DateTime CreateDate { get => _createDate; }
        public int MonthOfCreate { get => _monthOfCreate;  }
        public int YearOfCreate { get => _yearOfCreate; }
        public string Extension { get => _extension; }
        public double Size { get => _size; }
        public bool CopyFile { get; set; } = true;

        public FileDetails(string filePath, string destinationPath)
        {
            _fileInfo = new FileInfo(filePath);
            _sourcePath = Directory.GetDirectoryRoot(filePath);
            _fileName = Path.GetFileName(filePath);
            _destinationPathPrefix = destinationPath;
            _extension = Path.GetExtension(filePath).ToLower();
            SetPhotoInfo();
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
            _monthOfCreate = _createDate.Month;
            _yearOfCreate = _createDate.Year;

            var size = picture.Properties.GetProperty(SystemProperties.System.Size).ValueAsObject.ToString();
            _size = double.Parse(size);

            var camera = picture.Properties.GetProperty(SystemProperties.System.Photo.CameraManufacturer)?.ValueAsObject?.ToString().Replace(' ', '_'); 
            var cameraModel = picture.Properties.GetProperty(SystemProperties.System.Photo.CameraModel).ValueAsObject?.ToString().Replace(' ', '_');
            _device = $@"{camera}{((string.IsNullOrWhiteSpace(camera) || string.IsNullOrWhiteSpace(cameraModel)) ? string.Empty : "_")}{cameraModel}";

            _destinationPathSufix = Path.Combine(_yearOfCreate.ToString(), GetMonthName(_monthOfCreate));
        }

        public void AddDeviceToPath()
        {
            _destinationPathSufix = Path.Combine(_destinationPathSufix, _device);
        }

        public bool IsFilePathContainDevice()
        {
            return _sourcePath.Contains(_device);
        }

        public override bool Equals(object obj)
        {
            if(obj==null)
            {
                return false;
            }

            try
            {
                FileDetails other = obj as FileDetails;

                bool eqName = this.FileName == other.FileName;
                bool eqExtension = this.Extension == other.Extension;
                bool eqSize = this.Size == other.Size;
                bool eqDevice = this.Device == other.Device;
                bool eqCreateDate = this.CreateDate == other.CreateDate;

                return (eqName && eqExtension && eqSize && eqDevice && eqCreateDate);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
