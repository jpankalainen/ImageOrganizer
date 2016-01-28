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
    }
}
