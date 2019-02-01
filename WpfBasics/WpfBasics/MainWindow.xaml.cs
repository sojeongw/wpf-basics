using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfBasics
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

   

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The description is: {this.DescriptionText.Text}");    // 내용 가져오기
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckbox.IsChecked = this.AssemblyCheckbox.IsChecked = this.PlasmaCheckbox.IsChecked = false;
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            this.LengthText.Text += ((CheckBox)sender).Content;   // 체크박스 그 자체를 오브젝트인 sender로 받아오고 checkbox으로 형변환한다. 그리고 그 전체 내용인 content를 string으로 형변환한다. 하지만 이미 string이니까 여기선 생략한다.
        }

        private void FinishDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteText == null)
            {
                return;
            }

            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;
            this.NoteText.Text = (string)value.Content;
        }

        // 처음 이니셜 값 세팅
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           FinishDropdown_SelectionChanged(this.FinishDropdown, null);  // note에 값이 들어있는 것을 기본으로 함
        }

        private void SupplierNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MassText.Text = this.SupplierNameText.Text;
        }
    }
}
