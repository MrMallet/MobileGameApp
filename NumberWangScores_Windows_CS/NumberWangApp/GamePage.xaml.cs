using NumberWangApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace NumberWangApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private string op;


        public GamePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.navigationHelper.OnNavigatedTo(e);
            string op = e.ToString();
            gameHeader.Text = op.ToString();
            //fillGame(op);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            
            fillGame(op);
        }

        private void fillGame(string op)
        {
            //random number generator generates 2 numbers for the question

            //easy/medium/hard levels taken into consideration 
            //if(level == "easy") or if score or level == certain height then second random parameter = 12, 20, 50


            Random rnd = new Random();
            int num1 = rnd.Next(1, 13);
            int num2 = rnd.Next(1, 13);
            tbk1.Text = num1.ToString();
            tbk2.Text = num2.ToString();

            //get larger of the two integers = rangeNum
            //take result and whatever the operator just make the answer a random number between (result-rangeNum) and (result + rangeNum)
            //put them in an array and spit them out randomly to answertextbox buttons
            int rangeNum;
            if (num1 > num2)
                rangeNum = num1;
            else rangeNum = num2;

            int result;
            //not so random number generator generates the answers 
            if (op == "Addition")
            {
                tbktextSign.Text = "+";
                result = num1 + num2;
                answers(result, rangeNum);
            }
            else if (op == "Subtraction")
            {
                result = num1 - num2;
                answers(result, rangeNum);
            }
            else if (op == "Multiplication")
            {
                result = num1 * num2;
                answers(result, rangeNum);
            }

            else if (op == "Division")
            {
                result = num1 / num2;
                answers(result, rangeNum);
            }
            //how to pass an operator as a parameter. 
            //scrub that, go with a switch statment. more accurate alternate options for the user to choose from. 

        }

        private void answers(int result, int rangeNum)
        {
            Random rnd = new Random();
            btnAns1.Content = rangeNum; 
//                = rnd.Next((result - rangeNum), (result + rangeNum)).ToString();

            //create a for loop that fills an array with the answer and three non repeating randoms

            //shuffle them and assign them to the answer buttons

            //think about how to check the answer against the chosen block



        }

    }
}
