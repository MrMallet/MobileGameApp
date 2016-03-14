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
        private int rangeNum; 
        private int i =0;
        private int[] answers = new int[4];
        private int result;
        private int gameScore = 0;
        private int gameCounter = 1;
        private int gameLimit = 20;

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
            op = e.Parameter as string;
            gameHeader.Text = op.ToString();
            fillGame(op);
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

        //get rangeNum

        private int getRangeNum(int num1, int num2)
        {
            int rangeNum;
            if (num1 > num2)
                rangeNum = num1;
            else rangeNum = num2;
            return rangeNum;
        }

        public int checkArr(int[] answers, int checknum)
        {
            int checkednum = 0;
            for (int i = 0; i < 4; i++)
            {
                if (answers[i] == checknum)
                {
                    checknum++;
                    checkednum = checkArr(answers, checknum);
                }
                else checkednum = checknum;
            }
            return checkednum;
        }
       
        //private bool checkArr(int[] answers, int checkNum)
        //{
        //    bool truth = false ;
        //    for (int i = 0; i < 4; i++) {
        //        if (answers[i] == checkNum)
        //        {
        //            truth = true;
        //        }         
        //    }
        //    return truth; 
        //}


        private void fillGame(string op)
        {
            //random number generator generates 2 numbers for the question

            //easy/medium/hard levels taken into consideration 
            //if(level == "easy") or if score or level == certain height then second random parameter = 12, 20, 50


            Random rnd = new Random();
            int num1 = rnd.Next(1, 13);
            int num2 = rnd.Next(2, 13);// could sort both numbers so as to put large and small in order using num1 as rangeNum as well. 
            tbk1.Text = num1.ToString();
            tbk2.Text = num2.ToString();

            //get larger of the two integers = rangeNum
            //take result and whatever the operator just make the answer a random number between (result-rangeNum) and (result + rangeNum)
            //put them in an array and spit them out randomly to answertextbox buttons
            rangeNum=getRangeNum(num1, num2);

            
            //not so random number generator generates the answers 
            if (op.Equals("Addition"))
            {
                
                tbktextSign.Text = "+";
                result = num1 + num2;
                getAnswers(result, rangeNum);
                
            }
            else if (op == "Subtraction")
            {
                tbktextSign.Text = "-";
                result = num1 - num2;
                getAnswers(result, rangeNum);
            }
            else if (op == "Multiplication")
            {
                tbktextSign.Text = "x";
                result = num1 * num2;
                getAnswers(result, rangeNum);
            }

            else if (op == "Division")
            {
                tbktextSign.Text = "/";
                //will need to make sure of have a higher number divisble by another. might need to use a 2d array
                result = num1 / num2;
                getAnswers(result, rangeNum);
            }
            //how to pass an operator as a parameter. 
            //scrub that, go with a switch statment. more accurate alternate options for the user to choose from. 

        }


        private void getAnswers(int result, int rangeNum)
        {
            Random rnd = new Random();
            int lowNum = (result - rangeNum);
            int highNum = (result + rangeNum);
            //populate the array 
            

            // = rnd.Next((result - rangeNum), (result + rangeNum));
            answers[0] = result;
            //create a for loop that fills an array with the answer and three non repeating randoms
            for(int i = 1; i < 4; i++)
            {
                int checkNum = rnd.Next(lowNum, highNum);
                int checkedNum = checkArr(answers, checkNum);
                
                answers[i] = checkedNum;

            }
            //shuffle them and assign them to the answer buttons knuths/ fischer-yates shuffle
            for (int i = answers.Length- 1; i > 0; i--)
            {
                int index = rnd.Next(i + 1);
                int a = answers[index];
                answers[index] = answers[i];
                answers[i] = a;
            }
            //think about how to check the answer against the chosen block
            btnAns1.Content = answers[0];
            btnAns2.Content = answers[1];
            btnAns3.Content = answers[2];
            btnAns4.Content = answers[3];

        }

        private void calculateScore(int arPos)
        {
            if (answers[arPos] == result)
                gameScore++;
            gameCounter++; 
            tbkScore.Text = ("Score: " + gameScore);
            tbkGameCounter.Text = (gameCounter + "/" + gameLimit);
            if (gameCounter > gameLimit)
                //Frame.Navigate(typeof(MainPage), ArgumentNullException);
            Frame.Navigate(typeof(MainPage), gameScore);

        }

        private void btnAns1_Click(object sender, RoutedEventArgs e)
        {
            //get either the array value or the content string and compare it to result  
            int arPos = 0;
            calculateScore(arPos);
            fillGame(op);
        }

        private void btnAns2_Click(object sender, RoutedEventArgs e)
        {
            int arPos = 1;
            calculateScore(arPos);
            fillGame(op);
        }

        private void btnAns4_Click(object sender, RoutedEventArgs e)
        {
            int arPos = 3;
            calculateScore(arPos);
            fillGame(op);
        }

        private void btnAns3_Click(object sender, RoutedEventArgs e)
        {
            int arPos = 2;
            calculateScore(arPos);
            fillGame(op);
        }
    }
}
