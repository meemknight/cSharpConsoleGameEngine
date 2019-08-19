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
        public override bool onCreate()
        {
            return true;
        }


        public override bool update(float dTime)
        {

            drawLine(2, 3, 12, 20, Pixel.create('0', ConsoleColor.DarkMagenta));


            if (isKeyPressed('A'))
            {
                drawString(2, 2, "Test here", Pixel.create(' ', ConsoleColor.DarkCyan));
            }


            return true;
        }
    }
}
