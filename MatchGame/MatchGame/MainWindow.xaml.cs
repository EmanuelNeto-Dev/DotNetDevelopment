using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            //Create a new random generator
            Random random = new Random();

            //Find every TextBlock in the main grid and repeat the following statements
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
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
    }
}
