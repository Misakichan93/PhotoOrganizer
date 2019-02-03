using System;
using System.IO;
using System.Threading.Tasks;

namespace PhotoOrganizer.Services
{
    public class CopyService
    {
        public async Task CopyFile(Model.FileDetails file)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file.DestinationPathWithFileName));
                File.Copy(file.SouceFilePath, file.DestinationPathWithFileName, false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task MoveFile(Model.FileDetails file)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file.DestinationPathWithFileName));
                File.Move(file.SouceFilePath, file.DestinationPathWithFileName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
