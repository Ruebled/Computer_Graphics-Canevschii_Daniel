using System;
using System.Drawing;

namespace Graphics_Homework
{
    class Randomizer
    {
        private Random r;
        public Randomizer()
        {
            r = new Random();
        }

        public int GetRandomOffsetPositive(int maxval)
        {
            int getInteger = r.Next (0, maxval);
            
            return getInteger;
        }

        public int GetRandomOffsetRanged(int maxval)
        {
            int getInteger = r.Next(-1*maxval, maxval);
            
            return getInteger;
        }

        public Color GetRandomColor1()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);
            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        public Color GetRandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);
            int genA = 0;
            Color col = Color.FromArgb(genA,genR, genG, genB);

            return col;
        }
    }
}