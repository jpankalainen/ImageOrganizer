using ImageOrganizer.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ImageOrganizer.Behaviors
{
    class AddDroppedImagesToListBehavior : Behavior<Grid>
    {
        private IList<string> ImageExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".png", ".webp" };

        public static DependencyProperty TargetListProperty = 
            DependencyProperty.RegisterAttached("TargetList", typeof(IList<ImageViewModel>), typeof(AddDroppedImagesToListBehavior), new PropertyMetadata(null));

        public IList<ImageViewModel> TargetList
        {
            get { return (IList<ImageViewModel>)GetValue(TargetListProperty); }
            set { SetValue(TargetListProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach(var file in files.Select(f => new FileInfo(f)).Where(f => ImageExtensions.Any(ext => ext == f.Extension)))
                {
                    TargetList.Add(new ImageViewModel());
                }
            }
        }
    }
}
