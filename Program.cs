using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpConsoleGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {

            Demo inst = new Demo();
            inst.Create(10, 10);

            inst.run();
        }
    }
}

