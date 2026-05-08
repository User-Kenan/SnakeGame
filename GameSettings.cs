using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class GameSettings
    {
        public int Speed {  get; set; }
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public int InitialLength { get; set; }
        public string PlayerName { get; set; }

        public GameSettings(int speed, int width, int height, int length, string playerName)
        {
            Speed = speed;
            FieldWidth = width;
            FieldHeight = height;
            InitialLength = length;
            PlayerName = playerName;
        }

        
    }
}
