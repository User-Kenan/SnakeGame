using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;




namespace SnakeGame
{
    public class Game
    {
        private Snake snake1;
        private Food food;
        private GameSettings settings;
        private int score1;
       

        public Game(GameSettings settings)
        {
            this.settings = settings;
            snake1 = new Snake(settings.InitialLength, settings.FieldWidth, settings.FieldHeight);
            food = new Food(settings.FieldWidth, settings.FieldHeight);
            score1 = 0;
           
        }

        public void Start() 
        {
            snake1.Reset();
            food.Respawn();
            score1 = 0;
        }

        public void Update()
        {
            if(snake1 != null && snake1.IsAlive) // Wenn Snake noch Lebt
            {
                snake1.Move();

                if (snake1.CheckCollision()) // Wenn Snake koolidiert
                {
                    snake1.IsAlive = false;
                }

                if (snake1.IsEating(food.GetPosition())) // Wenn Snake isst
                {
                    snake1.Grow();
                    food.Respawn();
                    score1++;
                }
            }

        }


       

        public List<BodySegment> GetSnakeBodySegments() // Gibt alle Körperteile der Schlange zurück 
        {
            return snake1.GetBodySegments();
        }

        public void ChangeSnakeDirection(Direction newDirection) // Ändert Richtung von Schlange
        {
            snake1.ChangeDirection(newDirection);
        }

        public (int, int) GetFoodPosition() // Gibt die Position des Essens zurück
        {
            return food.GetPosition();
        }

        public bool IsSnakeAlive() // Prüft ob Schlange noch Lebt
        {
            return snake1.IsAlive;
        }

        public int GetScore() // Gibt den Score zurück
        {
            return score1;
        }


        public void SaveToBestenliste(string path, string playerName)
        {
            var eintrag = new Dictionary<string, object> // Es wird ein Dictonary erstellt mit folgenden Daten:
    {
        { "Name", playerName },
        { "Score", score1 },
        { "Speed", settings.Speed },
        { "FieldWidth", settings.FieldWidth },
        { "FieldHeight", settings.FieldHeight }
    };

            List<Dictionary<string, object>> bestehendeEintraege;// Liste in der alle Einträge gespeichert werden

            if (File.Exists(path)) // Überprüft ob File exestiert
            {
                string bestehenderInhalt = File.ReadAllText(path); // Inhalt der Datei wird gelesen

                if (!string.IsNullOrWhiteSpace(bestehenderInhalt)) 
                {
                    bestehendeEintraege = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(bestehenderInhalt); // Inahalt wird in Liste umgewandelt
                }
                else
                {
                    bestehendeEintraege = new List<Dictionary<string, object>>(); // wenn Datei Leer ist, wird eine neue Leere Liste erstellt
                }
            }
            else
            {
                bestehendeEintraege = new List<Dictionary<string, object>>(); // Wenn Datei nicht exestiert, wird eine Neue Leere Liste erstellt
            }

            bestehendeEintraege.Add(eintrag); // Einträge in Liste speichern

            string neuerJson = JsonSerializer.Serialize(bestehendeEintraege, new JsonSerializerOptions { WriteIndented = true }); // Liste wird in Json umgewandelt 
            File.WriteAllText(path, neuerJson); // Sie wird neu gespeichert 
        }




    }
}
