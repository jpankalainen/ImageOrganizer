using Accord.Imaging;
using ImageOrganizer.Models;
using ImageOrganizer.Models.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace ImageOrganizer.ViewModels
{
    class TargetFolderViewModel : BindableBase
    {
        private static int NEXT_FOLDER_ID = 0;

        public string Name => _directory.Name;
        public int FolderId { get; private set; }
        public double[][] SampleFeatures { get; private set; }

        public IEnumerable<IImage> Images
        {
            get
            {
                return _directory.Files.Where(f => Image.IsImage(f)).Select(f => new Image(f));
            }
        }

        private IDirectory _directory;

        public TargetFolderViewModel(IDirectory directory)
        {
            _directory = directory;
            FolderId = NEXT_FOLDER_ID;
            NEXT_FOLDER_ID++;
        }

        public void CalculateSampleFeatures(BagOfVisualWords bow)
        {
            List<double[]> features = new List<double[]>();
            foreach(var img in Images)
            {
                var vector = bow.GetFeatureVector(img.BitmapImage);
                features.Add(vector);
            }

            SampleFeatures = features.ToArray();
        }
    }
}
