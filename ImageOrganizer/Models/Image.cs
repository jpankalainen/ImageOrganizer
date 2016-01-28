using ImageOrganizer.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Windows.Media;
using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading.Tasks;

namespace ImageOrganizer.Models
{
    class Image : IImage
    {
        public ImageSource ImageSource => _source;
        public Bitmap BitmapImage => _image;
        public string Name => _sourceFile.Name;

        private static IList<string> ImageExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".png", ".webp" };

        private BitmapImage _source;
        private Bitmap _image;
        private IFile _sourceFile;

        public Image(IFile sourceFile)
        {
            var temp = (Bitmap)System.Drawing.Image.FromFile(sourceFile.FullPath);
            _image = new Bitmap(temp.Width, temp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(_image))
            {
                gr.DrawImage(temp, new Rectangle(0, 0, _image.Width, _image.Height));
            }

            _source = new BitmapImage(new Uri(sourceFile.FullPath));
            _sourceFile = sourceFile;
        }

        public static bool IsImage(IFile file)
        {
            return ImageExtensions.Any(e => e == file.Extension);
        }

        public async Task<float> Compare(IImage image)
        {
            throw new NotImplementedException();
        }
    }
}
