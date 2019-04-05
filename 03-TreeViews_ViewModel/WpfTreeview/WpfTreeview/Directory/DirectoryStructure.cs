using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A helper class to query information about directories
/// 실제 일어날 data logic을 구현하는 곳
/// </summary>

namespace WpfTreeview
{
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {

            // get every logical drive on the machine
          return  Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
            
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullpath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Create empty list
            var items = new List<DirectoryItem>();

            // Main Windows에 있던 내용을 여기로 이동
            #region Get Directories

            // try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir => new DirectoryItem {FullPath = dir, Type = DirectoryItemType.Folder }));
                }
            }
            catch
            {

            }

            /* ui 부분이므로 삭제한다. */
            // for each directory
            /* directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {

                    // set header as folder name
                    // 전체 디렉토리 패스에서 맨 끝에 있는 해당 이름만 가져오는 과정
                    // Header= Path.GetDirectoryName(directoryPath),
                    Header = GetFileFolderName(directoryPath),

                    // and tag as full path
                    Tag = directoryPath
                };



                // add dummy item so we can expand folder
                subItem.Items.Add(null);

                // handle expanding
                subItem.Expanded += Folder_Expanded;

                // add this item to the parent
                item.Items.Add(subItem);
            });
            */

            #endregion

            #region Get Files

            // try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
                }
            }
            catch
            {

            }

            /* ui 관련이므로 삭제 */
            /* 
            // for each file
            files.ForEach(filePath =>
            {
                // create file item
                var subItem = new TreeViewItem()
                {

                    // set header as file name
                    // path의 마지막을 불러오는 함수는 디렉토리와 공동으로 사용한다.
                    Header = GetFileFolderName(filePath),

                    // and tag as full path
                    Tag = filePath
                };

                // add this item to the parent
                item.Items.Add(subItem);
            });
            */

            #endregion

            return items;

        }

        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // if we have no path, return emp
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // path에서 슬래시와 백슬래시의 혼동을 방지하기 위함
            // 특수 문자라서 \\로 입력해 그냥 single charater라는 것을 표현함 
            var normalizedPath = path.Replace('/', '\\');

            // 맨 마지막 폴더 이름 string을 가져오기 위해 맨 마지막 백슬래쉬 index값을 받음
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // 하지만 path가 스트링으로만 이루어져 있으면 LastIndexOfAny는 -1을 반환하므로 체크 구문을 만든다.
            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
            {
                return path;
            }

            // Return the name after the last backslash
            return path.Substring(lastIndex + 1);

        }

        #endregion
    }
}


// tutorials: 03, 13:32