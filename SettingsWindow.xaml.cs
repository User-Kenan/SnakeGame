using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {


        private int speed;
        private int fieldSize;
        private int startLength;
        private string playerName;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.playerName = InputName.Text;
        }

        private void InputSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.speed = int.Parse(InputSpeed.Text);
                if (speed <= 0)
                {
                    throw new Exception();
                }
                    
                InputSpeed.Background = Brushes.LightBlue;
            }
            catch
            {
                InputSpeed.Background = Brushes.Red;
            }
        }

        private void InputPlaygroundSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.fieldSize = int.Parse(InputPlaygroundSize.Text);
                if (fieldSize <= 0)
                {
                    throw new Exception();
                }
                InputPlaygroundSize.Background = Brushes.LightBlue;
            }
            catch
            {
                InputPlaygroundSize.Background = Brushes.Red;
            }
        }

        private void InputStartlength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.startLength = int.Parse(InputStartLength.Text);
                if (startLength <= 0) throw new Exception();
                InputStartLength.Background = Brushes.LightBlue;
            }
            catch
            {
                InputStartLength.Background = Brushes.Red;
            }
        }

        private void ButtonGameStart_Click(object sender, RoutedEventArgs e)
        {
            

            if (string.IsNullOrEmpty(playerName) || speed <= 0 || fieldSize <= 0 || startLength <= 0)
            {
                MessageBox.Show("Bitte gültige Werte eingeben!");
                return;
            }

            int fieldWidth = fieldSize / 20;
            int fieldHeight = fieldSize / 20;

           
            GameWindow gameWindow = new GameWindow(playerName, speed, fieldWidth, fieldHeight, startLength);
            gameWindow.Show();
            this.Close();

            


        }

        private void ButtonBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
