using ImageOrganizer.ViewModels;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using ImageOrganizer.Models;

namespace ImageOrganizer.Behaviors
{
    class AddDroppedFileSystemItemsToListBehavior : Behavior<Grid>
    {
        public enum ItemType
        {
            Images,
            Folders,
            Both
        }
        
        public static DependencyProperty ActiveItemTypeProperty = 
            DependencyProperty.RegisterAttached("ActiveItemType", typeof(ItemType), typeof(AddDroppedFileSystemItemsToListBehavior), new PropertyMetadata(null));

        public ItemType ActiveItemType
        {
            get { return (ItemType)GetValue(ActiveItemTypeProperty); }
            set { SetValue(ActiveItemTypeProperty, value); }
        }

        public static DependencyProperty TargetListProperty =
            DependencyProperty.RegisterAttached("TargetList", typeof(IList), typeof(AddDroppedFileSystemItemsToListBehavior), new PropertyMetadata(null));

        public IList TargetList
        {
            get { return (IList)GetValue(TargetListProperty); }
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
                if(ActiveItemType == ItemType.Images || ActiveItemType == ItemType.Both)
                { 
                    foreach(var file in files.Select(f => new File(f)).Where(f => ImageOrganizer.Models.Image.IsImage(f)))
                    {
                        TargetList.Add(new ImageViewModel(file));
                    }
                }

                if (ActiveItemType == ItemType.Folders || ActiveItemType == ItemType.Both)
                {
                    foreach (var dir in files.Select(f => new Directory(f)).Where(d => d.Exists))
                    {
                        TargetList.Add(new TargetFolderViewModel(dir));
                    }
                }
            }
        }
    }
}
