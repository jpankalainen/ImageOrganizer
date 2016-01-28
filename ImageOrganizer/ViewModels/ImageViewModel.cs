using ImageOrganizer.Models.Interfaces;
using Prism.Mvvm;
using System.Windows.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
using Accord.Imaging;

namespace ImageOrganizer.ViewModels
{
    class ImageViewModel : BindableBase
    {
        public ImageSource Image => _image.ImageSource;
        public string Name => _image.Name;
        public double HighestScore { get; set; }
        public TargetFolderViewModel CurrentTarget { get; set; }
        public double[] FeatureVector { get; private set; }

        private IImage _image;
        public ImageViewModel(IImage image)
        {
            _image = image;
            HighestScore = 0;
        }

        public void CalculateFeatureVector(BagOfVisualWords bow)
        {
            FeatureVector = bow.GetFeatureVector(_image.BitmapImage);
        }

        public void Classify(ObservableCollection<TargetFolderViewModel> targetDirectories)
        {
            //foreach(var dir in targetDirectories)
            //{
            //    var similarity = 0.0f;
            //    var imgCount = 0;
            //    dir.Images.Do(async i =>
            //    {
            //        similarity += await i.Compare(_image);
            //        ++imgCount;
            //    });

            //    var avg = similarity / imgCount;
            //    if(avg > HighestScore)
            //    {
            //        HighestScore = similarity;
            //        CurrentTarget = dir;
            //        OnPropertyChanged(() => HighestScore);
            //        OnPropertyChanged(() => CurrentTarget);
            //    }
            //}
        }
    }
}
