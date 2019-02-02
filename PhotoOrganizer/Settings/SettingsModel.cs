using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Settings
{
    public class SettingsModel
    {
        private static readonly string _defaultSourceFolder;
        private static readonly string _defaultDestinationFolder;
        private string _sourceFolder;
        private string _destinationFolder;

        public string SourceFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sourceFolder))
                {
                    return _defaultSourceFolder;
                }
                return _sourceFolder;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _sourceFolder = value;
                }
            }
        }
        public string DestinationFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_destinationFolder))
                {
                    return _defaultDestinationFolder;
                }
                return _destinationFolder;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _destinationFolder = value;
                }
            }
        }

        static SettingsModel()
        {
            _defaultSourceFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _defaultDestinationFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }
}
