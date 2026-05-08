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
using System.Windows.Threading;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game game;
        private DispatcherTimer gameTimer;
        private string playerName;




        public GameWindow(string playerName, int speed, int fieldWidth, int fieldHeight, int startLength)
        {

            InitializeComponent();
            this.Focus(); // Tastatur Focus

            this.playerName = playerName;

            // Spieldfeldgroeße
            GameCanvas.Width = fieldWidth * 20;
            GameCanvas.Height = fieldHeight * 20;

            // Neues spiel mit Settings
            game = new Game(new GameSettings(speed, fieldWidth, fieldHeight, startLength, playerName));
            game.Start();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(500 / speed);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Snake bewegt und Spielzustand geändert 
            game.Update();
            DrawGame();

            if (!game.IsSnakeAlive()) // Wenn Snake tot ist
            {
                gameTimer.Stop();
                WindowGameOver window = new WindowGameOver(game, playerName);
                window.Show();
                this.Close();


            }
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();
            int cellSize = 20;

            for (int x = 0; x <= GameCanvas.Width; x += cellSize)
            {
                Line line = new Line // Linien für Spielfeld zeichnen Vertikal
                {
                    Stroke = Brushes.DarkBlue,
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = GameCanvas.Height,
                    StrokeThickness = 1
                };
                GameCanvas.Children.Add(line);
            }

            for (int y = 0; y <= GameCanvas.Height; y += cellSize)
            {
                Line line = new Line // Horitontal
                {
                    Stroke = Brushes.DarkBlue,
                    X1 = 0,
                    Y1 = y,
                    X2 = GameCanvas.Width,
                    Y2 = y,
                    StrokeThickness = 1
                };
                GameCanvas.Children.Add(line);
            }

            

            bool isHead = true;
            foreach (var segment in game.GetSnakeBodySegments()) // Jedes segment der Snake kennzeichnen
            {
                Rectangle rect = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Fill = isHead ? Brushes.LightGreen : Brushes.Green
                };
                Canvas.SetLeft(rect, segment.X * 20);
                Canvas.SetTop(rect, segment.Y * 20);
                GameCanvas.Children.Add(rect);

                if (isHead)
                {
                    // Augen auf Kopf zeichnen
                    Ellipse eye1 = new Ellipse
                    {
                        Width = 5,
                        Height = 5,
                        Fill = Brushes.Black
                    };
                    Canvas.SetLeft(eye1, segment.X * 20 + 5);
                    Canvas.SetTop(eye1, segment.Y * 20 + 5);
                    GameCanvas.Children.Add(eye1);

                    Ellipse eye2 = new Ellipse
                    {
                        Width = 5,
                        Height = 5,
                        Fill = Brushes.Black
                    };
                    Canvas.SetLeft(eye2, segment.X * 20 + 10);
                    Canvas.SetTop(eye2, segment.Y * 20 + 5);
                    GameCanvas.Children.Add(eye2);

                    isHead = false;
                }
            }

            // Essen zeichnen
            var foodPosition = game.GetFoodPosition();
            Ellipse food = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(food, foodPosition.Item1 * 20);
            Canvas.SetTop(food, foodPosition.Item2 * 20);
            GameCanvas.Children.Add(food);

            // Punktestand aktualisieren
            ScoreText.Text = $"Score: {game.GetScore()}";

        }
        


        private void Window_KeyDown(object sender, KeyEventArgs e) // Steuerung 
        {
            if (e.Key == Key.W) game.ChangeSnakeDirection(Direction.Up);
            if (e.Key == Key.S) game.ChangeSnakeDirection(Direction.Down);
            if (e.Key == Key.A) game.ChangeSnakeDirection(Direction.Left);
            if (e.Key == Key.D) game.ChangeSnakeDirection(Direction.Right);
        }
    }

}