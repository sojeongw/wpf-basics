using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTreeview
{
    /// <summary>
    /// The view model for the application main Directory view
    /// 실제 tree 구조 UI를 담는 곳
    /// </summary>

    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties 

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region Contructor 

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            // create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
               children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        #endregion

    }
}
