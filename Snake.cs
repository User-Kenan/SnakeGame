using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeGame
{
    public enum Direction { Up, Down, Left, Right }

    class Snake
    {
        public List<BodySegment> BodySegments { get; private set; }
        public Direction Direction { get; private set; }
        public bool IsAlive { get; set; }

        private int startLength;
        private int fieldWidth;
        private int fieldHeight;

        public Snake(int startLength, int fieldWidth, int fieldHeight)
        {
            this.startLength = startLength;
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            BodySegments = new List<BodySegment>();
            
        }

        public void ChangeDirection(Direction dir) // Wird aufgerufen, wenn der Spieler Richtungswechsel macht 
        {
            // Verhindert das Schlange sich selbst isst
            if ((Direction == Direction.Up && dir != Direction.Down) || (Direction == Direction.Down && dir != Direction.Up) || (Direction == Direction.Left && dir != Direction.Right) || (Direction == Direction.Right && dir != Direction.Left))
            {
                Direction = dir; // Nur wenn es geht wird die Richtung mit dir geeändert
            }
        }

        public void Move()
        {
            if (BodySegments.Count == 0) return; // Wenn Schlange kein Körperteil hat sofort beenden

            var head = BodySegments[0]; // Kopf deer Schlange
            int newX = head.X; // Neue Position
            int newY = head.Y; // Neue Position

            if (Direction == Direction.Up) // Wenn oben gesteuert wird => y Richtungswechsel
            {
                newY--;
            }
            else if (Direction == Direction.Down)
            {
                newY++;
            }
            else if (Direction == Direction.Left)
            {
                newX--;
            }
            else if (Direction == Direction.Right)
            {
                newX++;
            }


            BodySegments.Insert(0, new BodySegment(newX, newY)); // Neue Kopf an erstelle stelle einfügen
            BodySegments.RemoveAt(BodySegments.Count - 1); // Letzes Elemt wird gelöscht 
        }

        public void Grow()
        {
            if (BodySegments.Count == 0) // Prüft ob schlange segmente hat
            {
                return; 
            }

            var lastSegment = BodySegments[BodySegments.Count - 1]; // Holt letze segmenten der Schlange
            BodySegments.Add(new BodySegment(lastSegment.X, lastSegment.Y)); // Hängt neues Segment an der Positionen des letzten segmentes 
        }

        public bool CheckCollision() // Checkt ob Schlange kooldiert
        {
            if (BodySegments.Count == 0)
            {
                return false;
            }

            var head = BodySegments[0]; // Holt den Kopf der schlange

            
            for (int i = 1; i < BodySegments.Count; i++) // Prüft ob schlange mit sich selbst koolidiert
            {
                if (head.X == BodySegments[i].X && head.Y == BodySegments[i].Y)
                {
                    return true;
                }
            }

            if (head.X < 0 || head.Y < 0 || head.X >= fieldWidth || head.Y >= fieldHeight) // Prüft Kolision mit Spielerand
            {
                return true;
            }
            return false;
        }

        public void Reset() // Löscht Schlange
        {

            // Startpositionen neue schlange
            BodySegments.Clear();

            int startX = fieldWidth / 2; 
            int startY = fieldHeight / 2;

            for (int i = 0; i < startLength; i++) // Baut schlange neu auf
            {
                BodySegments.Add(new BodySegment(startX - i, startY)); 
            }

            Direction = Direction.Right; // Startrichtung rechts setzen
            IsAlive = true; // Schlange lebt 
        }
    

        public bool IsEating((int, int) foodPosition)
        {
            if (BodySegments.Count == 0) // Hat die Schlange einen Kopf
            {
                return false ;
            }

            // Vergleicht positionen Kopf und essen => wenn gleich dann isst schlange
            var head = BodySegments[0]; 
            return (head.X, head.Y) == foodPosition;
        }

        public List<BodySegment> GetBodySegments()
        {
            return BodySegments; // Gibt Liste der Körperteile zurück
        }
    }
}








