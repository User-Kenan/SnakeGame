using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;


namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for BestenListeWindow.xaml
    /// </summary>
    public partial class BestenListeWindow : Window
    {
        public BestenListeWindow()
        {
            InitializeComponent();
            LoadBestenListe(); // BestenListe geladen und gezeigt 
        }

        private void LoadBestenListe()
        {
            string path = "walloffame.json";
            string json = File.ReadAllText(path); // path wird als Text gelesen
            var eintraege = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json); // Wird in Daten umgewandelt. Jede eintrag => gespeicherter spieler

            // Nach Scorer sortieren 
            var sortierteEintraege = eintraege.OrderByDescending(e => e["Score"].GetInt32());

            foreach (var eintrag in sortierteEintraege) // Schleife durch alle Speiler 
            {
                // Für jeden Spieler foltgende Werte holen
                string name = eintrag["Name"].GetString();
                int score = eintrag["Score"].GetInt32(); 
                int speed = eintrag["Speed"].GetInt32();
                int width = eintrag["FieldWidth"].GetInt32();
                int height = eintrag["FieldHeight"].GetInt32();

                WallOfFameList.Items.Add($"Name: {name} | Points: {score} | Speed: {speed} | Area: {width}x{height}"); // Formatierung 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
