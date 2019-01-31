using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace PhotoOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _sourcePath;
        private string _destinationPath;
        private RelayCommand _setSoucePathCommand;
        private RelayCommand _setDestinationPathCommand;
        private RelayCommand _executeExportFilesCommand;

        public RelayCommand SetSoucePathCommand
        {
            get {
                if(_setSoucePathCommand==null)
                {
                    _setSoucePathCommand = new RelayCommand(SetSourcePathCommandExecute);
                }
                return _setSoucePathCommand;
            }
          
        }
        public RelayCommand SetDestinationPathCommand
        {
            get
            {
                if(_setDestinationPathCommand==null)
                {
                    _setDestinationPathCommand = new RelayCommand(SetDestinationPathCommandExecute);
                }
                return _setDestinationPathCommand;
            }
        }
        public RelayCommand ExecuteExportFilesCommand
        {
            get
            {
                if(_executeExportFilesCommand==null)
                {
                    _executeExportFilesCommand = new RelayCommand(ExportFiles);
                }
                return _executeExportFilesCommand;
            }
        }

        

        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                if(_sourcePath!=value && !string.IsNullOrEmpty(value))
                {
                    _sourcePath = value;
                    OnPropertyChanged(nameof(SourcePath));
                }
            }
        }
        public string DestinationPath
        {
            get { return _destinationPath; }
            set
            {
                if (_destinationPath != value && !string.IsNullOrEmpty(value))
                {
                    _destinationPath = value;
                    OnPropertyChanged(nameof(DestinationPath));
                }
            }
        }
        
        public MainWindow()
        {    
            InitializeComponent();
            SourcePath=$@"C:\Users\annaj_f1pwpno\Documents\Zdjęcia";
            DestinationPath = $@"C:\Users\annaj_f1pwpno\Documents\Photo";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(string name)
        {
            NotifyPropertyChanged(name);
        }


        private void SetSourcePathCommandExecute()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            SourcePath = dialog.SelectedPath;
        }

        private void SetDestinationPathCommandExecute()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            DestinationPath = dialog.SelectedPath;
        }

        private void ExportFiles()
        {
            Services.FilesExportService filesExportService = new Services.FilesExportService(SourcePath, DestinationPath);
            filesExportService.ExportAndReoganizeFiles();
        }
    }
}
