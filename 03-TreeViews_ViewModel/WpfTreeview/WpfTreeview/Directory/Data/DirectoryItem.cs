using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Information about a directory item such as a drive, a file or a folder
/// </summary>

namespace WpfTreeview
{
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// DirectoryItemType이라는 클래스를 만들고 get, set으로 가져온다.
        /// </summary>
        public DirectoryItemType Type {get;set;}

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type ==DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
