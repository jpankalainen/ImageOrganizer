using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace ImageOrganizer.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        ObservableCollection<DirectoryViewModel> SourceDirectories { get; }
        ObservableCollection<DirectoryViewModel> TargetDirectories { get; }
    }
}
