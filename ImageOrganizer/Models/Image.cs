using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ImageOrganizer.Models
{
    class Image
    {
        private static IList<string> ImageExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".png", ".webp" };

        private BitmapImage _image;

        Image(File sourceFile)
        {
            _image = new BitmapImage(new System.Uri(sourceFile.FullPath));
        }

        public static bool IsImage(File file)
        {
            return ImageExtensions.Any(e => e == file.Extension);
        }
    }
}
