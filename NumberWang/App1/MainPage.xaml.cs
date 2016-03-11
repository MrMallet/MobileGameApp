using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App1
{

    //when a selection is picked we want to load the game page passing whatever the sign eg.(+,-,X,/) 
    //was associated with each button. also the string to the top of the page. 

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            getScores();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void getScores()
        {
            Item item = new Item { Text = "Awesome item" };
            await App.MobileService.GetTable<Item>().InsertAsync(item);

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), "Addition");
        }

        private void btnMul_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), "Multiplication");
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), "Subtraction");
        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), "Division");
        }
    }
}
