﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    public class PasswordBoxProperties
    {
        //public bool HasText { get; set; } = false;

        public static readonly DependencyProperty MonitorPasswordProperty =
            DependencyProperty.RegisterAttached("MonitorPassword", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false, OnMonitorPasswordChanged));

        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (d as PasswordBox);

            if (passwordBox == null)
                return;

           /*
            * 여기서 NewValue는 texts.xaml에 있는 Value 값
            <Setter Property="local:PasswordBoxProperties.MonitorPassword" Value="True"/>
            */
            if((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += passwordBox_PasswordChanged;
            }
        }


        private static void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        public static void SetMonitorPasswordProperty(PasswordBox element, bool value)
        {
            element.SetValue(MonitorPasswordProperty, value);
        }

        public static bool GetMonitorPasswordProperty(PasswordBox element)
        {
            return (bool)element.GetValue(MonitorPasswordProperty);
        }



        // public bool HasText {get; set;} = false;

        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false));

        private static void SetHasText(PasswordBox element)
        {
            element.SetValue(HasTextProperty, element.SecurePassword.Length > 0);
        }

        public static bool GetHasText(PasswordBox element)
        {
            return (bool)element.GetValue(HasTextProperty);
        }
    }
}