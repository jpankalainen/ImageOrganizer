using ImageOrganizer.Models.Interfaces;
using System.IO;

namespace ImageOrganizer.Models
{
    class Directory : IDirectory
    {
        private DirectoryInfo _directoryInfo;

        public bool Exists => _directoryInfo.Exists;

        public Directory(string path) : this(new DirectoryInfo(path)) { }
        public Directory(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }
    }
}
