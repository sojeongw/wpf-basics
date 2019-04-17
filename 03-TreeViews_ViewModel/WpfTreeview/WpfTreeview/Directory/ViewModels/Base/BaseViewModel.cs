using PropertyChanged;
using System.ComponentModel;


namespace WpfTreeview
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    [ImplementPropertyChanged]  // UI와 비즈니스 로직을 자동으로 연결해준다.
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
