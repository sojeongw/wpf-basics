using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeview
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// DirectoryItemType이라는 클래스를 만들고 get, set으로 가져온다.
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name{ get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        // INotifyCollectionChanged를 가지고 있어 UI나 view model의 변화를 감지한다.
        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>d   
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

       
        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } } // UI의 표현을 위한 것

        public bool IsExpended
        {
            // expand 할지 말지 결정하고
            get
            {
                // null인지 check
                return this.Children?.Count(f => f != null) > 0;
            }
            // expand 클릭하면 보여줄 아이템을 set
            set
            {
                // if the UI tells us to expand
                if(value == true)
                {
                    // Find all children
                    Expand();
                }
                //if the UI tells us to close
                else
                {
                    this.ClearChildren();
                }
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // 미리 아래에 만들어 놓은 Expand를 넘긴다.
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // Setup the children as needed
            this.ClearChildren();

            /*
             위의 코드를 생성자에서 처리하는 이유는
             type을 먼저 받아와야 ClearChildren 메소드에서  
             file 타입인지 체크한 뒤 처리할 수 있기 때문이다.
             */
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // clear item
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // show the expand arrow if we are not a file
            if(this.Type != DirectoryItemType.File)
            {
                this.Children.Add(null);
            }
        }

        #endregion

        /// <summary>
        /// Expands this directory and find all children
        /// </summary>
        private void Expand()
        {
            // we cannot expand a file
            if (this.Type == DirectoryItemType.File)
            {
                return;
            }

            // find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);

            this.Children = new ObservableCollection<DirectoryItemViewModel>
                (children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
        
    }
}
