using System.Windows.Media;
using System.Drawing;
using System.Threading.Tasks;

namespace ImageOrganizer.Models.Interfaces
{
    interface IImage
    {
        ImageSource ImageSource { get; }
        Bitmap BitmapImage { get; }
        string Name { get; }
        
        Task<float> Compare(IImage image);
    }
}
