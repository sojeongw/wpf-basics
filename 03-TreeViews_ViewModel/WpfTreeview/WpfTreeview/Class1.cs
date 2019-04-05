// view model을 위한 class

using System.ComponentModel;
using System.Threading.Tasks;

namespace WpfTreeview
{
    public class Class1 : INotifyPropertyChanged
    {
        // 이 코드만 있으면 버튼엔 WpfTreeview.Class1 이라고 출력된다.
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        // xaml에서 binding 요소 지정을 위한 코드
        // Test에 담긴 데이터로 지정했기 때문에 아래에 override한 데이터는 나오지 않음.
        public string Test { get; set; } = "My Property";

        public Class1()
        {
            Task.Run(async () =>
            {
            int i = 0;

            while (true)
            {
                // 1초에 5번씩 value를 가져온다.
                await Task.Delay(200);
                Test = (i++).ToString();
            }
            });
        }

        // string을 덮어쓰고 싶다면 따로 return 해준다.
        public override string ToString()
        {
            return "Hello, World";
        }
    }
}
