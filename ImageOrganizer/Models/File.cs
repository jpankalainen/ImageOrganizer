using ImageOrganizer.Models.Interfaces;
using System.IO;

namespace ImageOrganizer.Models
{
    class File : IFile
    {
        private FileInfo _fileInfo;

        public string Name => _fileInfo.Name;
        public string FullPath => _fileInfo.FullName;
        public string Extension => _fileInfo.Extension;

        public File(string path) : this(new FileInfo(path)) {}

        public File(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }
    }
}
