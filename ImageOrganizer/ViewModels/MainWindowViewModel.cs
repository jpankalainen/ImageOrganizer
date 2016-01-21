using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace ImageOrganizer.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<ImageViewModel> SourceFiles { get; }
        public ObservableCollection<TargetFolderViewModel> TargetDirectories { get; }

        public MainWindowViewModel()
        {
            SourceFiles = new ObservableCollection<ImageViewModel>();
            TargetDirectories = new ObservableCollection<TargetFolderViewModel>();
        }
    }
}
