using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    
    /// <summary>
    /// The MonitorPassword attached property for a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // get the caller
            var passwordBox = sender as PasswordBox;

            // make sure it is a password box
            if(passwordBox == null)
            {
                return;
            }

            // remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // if the caller set MonitorPassword to true...
            if ((bool)e.NewValue)
            {
                // set default value
                HasTextProperty.SetValue(passwordBox);

                // start listening out for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Fired when the password box password value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // set the attached HasText value 
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// The HasText attached property for a <see cref="PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// sets the HasText proeprty based on if the caller <see cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);

        }
    }

}
