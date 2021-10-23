﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Elmish.Uno.Samples
{
    public sealed partial class Shell : UserControl, INavigate
    {
        public Shell()
        {
            this.InitializeComponent();

#if !(NET5_0 && WINDOWS)
            SystemNavigationManager.GetForCurrentView().BackRequested += OnSystemNavigationManagerBackRequested;
#endif
            KeyboardAccelerator GoBack = new KeyboardAccelerator()
            {
                Key = VirtualKey.GoBack
            };
            GoBack.Invoked += BackInvoked;
            KeyboardAccelerator AltLeft = new KeyboardAccelerator()
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            AltLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(GoBack);
            this.KeyboardAccelerators.Add(AltLeft);

            KeyboardAccelerator GoForward = new KeyboardAccelerator()
            {
                Key = VirtualKey.GoForward
            };
            GoForward.Invoked += ForwardInvoked;
            KeyboardAccelerator AltRight = new KeyboardAccelerator()
            {
                Key = VirtualKey.Right,
                Modifiers = VirtualKeyModifiers.Menu
            };
            AltRight.Invoked += ForwardInvoked;
            this.KeyboardAccelerators.Add(GoForward);
            this.KeyboardAccelerators.Add(AltRight);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
        }

        private bool OnBackRequested()
        {
            if (this.RootFrame.CanGoBack)
            {
                this.RootFrame.GoBack();
                return true;
            }
            return false;
        }

        private bool OnForwardRequested()
        {
            if (this.RootFrame.CanGoForward)
            {
                this.RootFrame.GoForward();
                return true;
            }
            return false;
        }

#if !(NET5_0 && WINDOWS)
        private void OnSystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            OnBackRequested();
            e.Handled = true;
        }
#endif

        private void OnBackButtonClick(object sender, RoutedEventArgs e) => OnBackRequested();

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs e)
        {
            OnBackRequested();
            e.Handled = true;
        }

        private void ForwardInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs e)
        {
            OnForwardRequested();
            e.Handled = true;
        }


        public bool Navigate(Type sourcePageType) => this.RootFrame.Navigate(sourcePageType, null);

        public bool Navigate(Type sourcePageType, object parameter) => this.RootFrame.Navigate(sourcePageType, parameter);
    }
}
