using System.Collections.Generic;

namespace ImageOrganizer.Models.Interfaces
{
    interface IDirectory
    {
        string Name { get; }
        IEnumerable<IFile> Files { get; }
    }
}
