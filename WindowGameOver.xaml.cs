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
    /// Interaction logic for WindowGameOver.xaml
    /// </summary>
    public partial class WindowGameOver : Window
    {
        private Game currentGame;
        private string currentPlayerName; 

        public WindowGameOver(Game game, string playerName) 
        {
            InitializeComponent();
            this.currentGame = game;
            this.currentPlayerName = playerName; 
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            currentGame.SaveToBestenliste("walloffame.json", currentPlayerName);
            MessageBox.Show("Spiel gespeichert!");
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
 