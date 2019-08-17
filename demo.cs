using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace cSharpConsoleGameEngine
{
    class Demo : Cscge
    {
        public override void onCreate()
        {

        }

        public override void update(float dTime)
        {


            drawLine(2, 3, 12, 20, Pixel.create('0', ConsoleColor.DarkMagenta));

            if (isKeyPressed('A'))
            {
                draw(2, 3, Pixel.create('o', ConsoleColor.Cyan));
            }

        }
    }
}
