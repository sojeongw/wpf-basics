using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfTreeview
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // UI 데이터 바인딩의 root를 먼저 만들어준다.
            // this.DataContext = new Class1();

            // 모든 UI는 DirectoryStructureViewModel에 바인딩된다.
            this.DataContext = new DirectoryStructureViewModel();

            /*
            DirectoryStructureViewModel 클래스에 바인딩 하기 전에 children이 잘 나오는지 보는 테스트 코드
            var d = new DirectoryStructureViewModel();
            var Item1 = d.Items[0];

            d.Items[0].ExpandCommand.Execute(null);
            */

        }

        #endregion

        /* DirectoryStructure로 옮기고 xaml에 있는 Loaded="Window_Loaded를 지워준다. */
        #region On Loaded
        /// <summary>
        ///  when the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // get every logical drive on the machine
        //    foreach (var drive in Directory.GetLogicalDrives())
        //    {
        //        /*
        //        // create a new item for it
        //        var item = new TreeViewItem();

        //        // set the header
        //        // 기본 값은 textblock에 있는 text가 들어가지만, xaml에서 binding을 해주면 item header에 실제 드라이브 값이 들어간다.
        //        item.Header = drive;

        //        // and the full path
        //        item.Tag = drive;
        //        */

        //        // 간략하게 이렇게 표현 가능 
        //        var item = new TreeViewItem()
        //        {
        //            // set the header
        //            Header = drive,
        //            // and the full path
        //            Tag = drive
        //        };

        //        // add a dummy item
        //        item.Items.Add(null);   // 자식 노드를 Items로 접근해서 추가 

        //        // listen out for item being expanded
        //        item.Expanded += Folder_Expanded;   // tab키 누르면 이벤트 핸들러 자동 생성

        //        // add it to the main tree-view
        //        FolderView.Items.Add(item);
        //    }
        //}

        #endregion

        /* ui 부분을 제외하고 Directory Structure로 이동한다. */
        #region Folder Expanded

        /// <summary>
        /// When a folder is expanded, find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void Folder_Expanded(object sender, RoutedEventArgs e)
        //{
        //    #region Initial Checks

        //    var item = (TreeViewItem)sender;

        //    // If the item only contains the dummy data
        //    if (item.Items.Count != 1 || item.Items[0] != null)
        //    {
        //        return;
        //    }


        //    //clear dummy data
        //    item.Items.Clear();

        //    // get full path
        //    var fullPath = (string)item.Tag;

        //    #endregion

        //    #region Get Directories

        //    var directories = new List<string>();

        //    // try and get directories from the folder
        //    // ignoring any issues doing so
        //    try
        //    {
        //        var dirs = Directory.GetDirectories(fullPath);
        //        if (dirs.Length > 0)
        //        {
        //            directories.AddRange(dirs);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //    // for each directory
        //    directories.ForEach(directoryPath =>
        //    {
        //        var subItem = new TreeViewItem()
        //        {

        //            // set header as folder name
        //            // 전체 디렉토리 패스에서 맨 끝에 있는 해당 이름만 가져오는 과정
        //            // Header= Path.GetDirectoryName(directoryPath),
        //            Header = GetFileFolderName(directoryPath),

        //            // and tag as full path
        //            Tag = directoryPath
        //        };



        //        // add dummy item so we can expand folder
        //        subItem.Items.Add(null);

        //        // handle expanding
        //        subItem.Expanded += Folder_Expanded;

        //        // add this item to the parent
        //        item.Items.Add(subItem);
        //    });

        //    #endregion

        //    #region Get Files

        //    // Create a blank list for files
        //    var files = new List<string>();

        //    // try and get directories from the folder
        //    // ignoring any issues doing so
        //    try
        //    {
        //        var fs = Directory.GetFiles(fullPath);

        //        if (fs.Length > 0)
        //        {
        //            files.AddRange(fs);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //    // for each file
        //    files.ForEach(filePath =>
        //    {
        //        // create file item
        //        var subItem = new TreeViewItem()
        //        {

        //            // set header as file name
        //            // path의 마지막을 불러오는 함수는 디렉토리와 공동으로 사용한다.
        //            Header = GetFileFolderName(filePath),

        //            // and tag as full path
        //            Tag = filePath
        //        };

        //        // add this item to the parent
        //        item.Items.Add(subItem);
        //    });

        //    #endregion


        //}

        #endregion

        /* DirectoryStructure.cs로 이동 */
        //#region Helpers

        ///// <summary>
        ///// Find the file or folder name from a full path
        ///// </summary>
        ///// <param name="path">The full path</param>
        ///// <returns></returns>
        //public static string GetFileFolderName(string path)
        //{
        //    // if we have no path, return emp
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return string.Empty;
        //    }

        //    // path에서 슬래시와 백슬래시의 혼동을 방지하기 위함
        //    // 특수 문자라서 \\로 입력해 그냥 single charater라는 것을 표현함 
        //    var normalizedPath = path.Replace('/', '\\');

        //    // 맨 마지막 폴더 이름 string을 가져오기 위해 맨 마지막 백슬래쉬 index값을 받음
        //    var lastIndex = normalizedPath.LastIndexOf('\\');

        //    // 하지만 path가 스트링으로만 이루어져 있으면 LastIndexOfAny는 -1을 반환하므로 체크 구문을 만든다.
        //    // If we don't find a backslash, return the path itself
        //    if (lastIndex <= 0)
        //    {
        //        return path;
        //    }

        //    // Return the name after the last backslash
        //    return path.Substring(lastIndex + 1);

        //}

        //#endregion
    }
}
