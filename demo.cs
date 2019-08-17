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
            draw(2, 3, 'o');
        }
    }
}
