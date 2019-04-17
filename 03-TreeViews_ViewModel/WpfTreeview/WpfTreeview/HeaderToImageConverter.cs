using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeview
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>
    /// 

    // [ValueConversion] 을 붙여주면 xaml에 써있는 클래스를 찾아준다.
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]  // ValueConversion(a, b): a라는 소스를 b로 바꿔줌 
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return new BitmapImage(new Uri($"pack://application:,,,/Images/{value}.png"));

            // By default, we presume an image
            var image = "Images/file.png";

            // if the name is blank, we presume it's a drive as we cannot have a black file or folder name
            switch((DirectoryItemType)value){
                case DirectoryItemType.Drive:
                image = "Images/drive.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Images/folder-closed.png";
                    break;
            }

            // 미리 불러왔던 이미지 경로를 넣어준다.
            // Wpf 안에 있는 리소스에 접근하려면 pack://application:,,,를 써줘야 한다.
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
