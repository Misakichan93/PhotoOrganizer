using System.IO;
using System.Xml.Serialization;

namespace PhotoOrganizer.Settings
{
    public class SettingsService
    {

        private const string _settingsFilePath = "../DefaultPathsSettings.config";
        private SettingsModel _settingsModel;

        public string SourceFolder { get => _settingsModel.SourceFolder; set => _settingsModel.SourceFolder = value; }
        public string DestinationFolder { get => _settingsModel.DestinationFolder; set => _settingsModel.DestinationFolder = value; }

        public SettingsService()
        {
            _settingsModel = new SettingsModel();
        }

        public void CreateSettingsConfig()
        {
            SaveSettingsConfig(SourceFolder, DestinationFolder);
        }

        public void ReadSettings()
        {
            if (File.Exists(_settingsFilePath))
            {
                using (FileStream stream = new FileStream(_settingsFilePath, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    if (stream.Length > 0)
                    {
                        Settings settings = (Settings)serializer.Deserialize(stream);

                        SourceFolder = settings.SourceFolder;
                        DestinationFolder = settings.DestinationFolder;
                    }

                    stream.Close();
                }
            }
            else
            {
                CreateSettingsConfig();
            }
        }

        public void SaveSettingsConfig(string sourceFolder, string destinationFolder)
        {
            bool fileExist = File.Exists(_settingsFilePath);
            if (!fileExist)
            {
                File.Create(_settingsFilePath).Close();
            }

            if(sourceFolder!=SourceFolder || destinationFolder!=DestinationFolder || !fileExist)
            {
                using (var stream = new FileStream(_settingsFilePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    Settings settings = new Settings()
                    {
                        SourceFolder = sourceFolder,
                        DestinationFolder = destinationFolder
                    };

                    serializer.Serialize(stream, settings);

                    stream.Close();
                }
            }           
        }
    }
}
