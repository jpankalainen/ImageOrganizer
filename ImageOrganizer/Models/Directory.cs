using ImageOrganizer.Models.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageOrganizer.Models
{
    class Directory : IDirectory
    {
        public bool Exists => _directoryInfo.Exists;
        public string Name => _directoryInfo.Name;
        public string Path => _directoryInfo.FullName;

        private List<File> _files;
        public IEnumerable<IFile> Files => _files;

        private DirectoryInfo _directoryInfo;

        public Directory(string path) : this(new DirectoryInfo(path)) { }
        public Directory(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
            _files = directoryInfo.GetFiles().Select(f => new File(f)).ToList();
        }
    }
}
