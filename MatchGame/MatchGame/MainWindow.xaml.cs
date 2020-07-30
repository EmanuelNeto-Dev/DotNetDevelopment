using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Create a list of eight pairs of emoji
        List<string> animalEmoji = new List<string>()  
        {
             "🐶","🐶"
            ,"🦍","🦍"
            ,"🦊","🦊"
            ,"🐴","🐴"
            ,"🦒","🦒"
            ,"🐘","🐘"
            ,"🐼","🐼"
            ,"🐔","🐔"
        };

        TextBlock lastTextBlockClicked;
        //This variable indicates that the player is still searching for the righ match
        bool findingMatch = false;

        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text = TimeTextBlock.Text + " - Play Again?";
            }
        }

        private void SetUpGame() 
        {
            //Create a new random generator
            Random random = new Random();

            //Find every TextBlock in the main grid and repeat the following statements
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {

                if (textBlock.Name != "TimeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;

                    //Pick a random number between 0 and the number of emoji left in the list and 
                    //call it "index"
                    int index = random.Next(animalEmoji.Count);

                    //Use the random number called "Index" to get 
                    //a random emoji from the list
                    string nextEmoji = animalEmoji[index];

                    //Update the TextBlock with the random emoji from the list
                    textBlock.Text = nextEmoji;

                    //Use the random number called "index" to get a random emoji from the list
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            TextBlock textBlock = sender as TextBlock;

            //This "IF Statement" verifies if the player already found the right match
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            //When the player finds the right match
            else if(textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            //When the player got the wrong match
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
