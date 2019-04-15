// view model을 위한 class

using PropertyChanged;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WpfTreeview
{
    // public인 속성을 모두 detect한 뒤 PropertyChanged에 inject한다. 버전 업 되면서 없어짐.
    //[ImplementPropertyChanged]
    public class Class1 : INotifyPropertyChanged
    {

        private string mTest;

        // 이 코드만 있으면 버튼엔 WpfTreeview.Class1 이라고 출력된다.
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        // xaml에서 binding 요소 지정을 위한 코드
        // Test에 담긴 데이터로 지정했기 때문에 아래에 override한 데이터는 나오지 않음.
        // getter setter를 이용해 바로 위에서 만든 event를 가지고 Test안에 있는 값을 반영하게 된다.
        /*public string Test {
            get
            {
                return mTest;
            }

            set
            {
                if(mTest == value)
                {
                    return;
                }

                mTest = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Test)));
            }

        }*/

        // fody를 쓰면서 자동으로 데이터를 get/set 시켜줌
        public string Test { get; set; }

        public Class1()
        {
            Task.Run(async () =>
            {
            int i = 0;

            while (true)
            {
                // 1초에 5번씩 value를 가져온다.
                await Task.Delay(200);

                // 여기만 바꿔주면 UI에 바로 반영된다.
                Test = (i++).ToString();

                // Test에 담긴 값이 실시간으로 표시되는지 확인하는 테스트 코드
                // PropertyChanged(this, new PropertyChangedEventArgs("Test"));
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
