namespace ImageOrganizer.Models.Interfaces
{
    interface IFile
    {
        string FullPath { get; }
        string Extension { get; }
    }
}
