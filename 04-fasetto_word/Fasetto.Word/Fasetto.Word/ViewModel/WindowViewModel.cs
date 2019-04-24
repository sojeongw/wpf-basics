using System.Windows;
using System.Windows.Input;


namespace Fasetto.Word
{
    /// <summary>
    /// 윈도우 자체의 사이즈 등을 정하기 위한 파일
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private member

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        #endregion

        #region Public Properties

        /* 
          정상 출력 확인용
          public string Test { get; set; } = "My String";
        */

        /// <summary>
        /// The smallest width the window can go to
        /// 줄일 수 있는 최소 크기 설정 후 MainWindow.xaml 상단에 binding 해준다.
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// Border는 OuterMargin을 포함하고 있는데
        /// 우리는 지금 actual 사이즈에서 border 사이즈를 정하고 싶은 것이므로
        /// OuterMargin 사이즈를 포함해서 설정한다.
        /// 
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder+OuterMarginSize); } }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }


        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// Window의 실제 사이즈는 그보다 더 넓은 영역이다. 그래서 그 안에 그림자 등을 넣어 표현할 수 있는 것이다.
        /// 윈도우가 최대화 되면 마진이 없어지고 그렇지 않으면 기본 설정해놓은 outer margin 사이즈를 넘긴다.
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight+ResizeBorder); } }

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                // 사이즈가 변할 때마다 recalculate
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));

            };

            // Create commands
            // WindowState 정의를 보면 normal=0, minimize=1, maximize=2로 되어있다.
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            // XOR 연산을 해서 Window state가 maximized된 상태라면 0이 되고 아니면 maximized 값이 된다.
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            // 기존에 있는 resizer 코드를 불러와 적용한다. 기존에 page content에 꽉 차게 버튼을 넣으면 최대화 시켰을 때 패딩이 사라지고 완전히 꽉 차는 현상을 방지한다.
            var resizer = new WindowResizer(mWindow);
        }

        #endregion

        #region Private Helpers

        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion
    }
}
