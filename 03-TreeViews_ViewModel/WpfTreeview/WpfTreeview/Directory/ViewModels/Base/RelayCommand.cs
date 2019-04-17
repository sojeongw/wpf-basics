using System;
using System.Windows.Input;

namespace WpfTreeview
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        // 액션을 저장하는 Action. 파라미터나 리턴값이 없다. blank function.
        /// <summary>
        /// the action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action) // run할 action을 넘겨줌 
        {
            // 위에서 만든 mAction에 action 할당
            mAction = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
