using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Food
    {
        private Random random = new Random();

        public int x {  get; private set; }
        public int y { get; private set; }


        private int fieldWidth;
        private int fieldHeight;

        public Food(int fieldWidth, int fieldHeight)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            Respawn();
        }

        public void Respawn() 
        {
            x = random.Next(0, fieldWidth);
            y = random.Next(0, fieldHeight);
        }

        public (int, int) GetPosition()
        {
            return (x, y);
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }




    }
}
