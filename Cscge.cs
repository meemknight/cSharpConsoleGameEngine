using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace cSharpConsoleGameEngine
{
    public struct Color
    {
        public Color(char c, ConsoleColor b = ConsoleColor.Black, ConsoleColor f = ConsoleColor.White)
        {
            background = b;
            foreground = f;
            character = c;
        }

        public ConsoleColor background;
        public ConsoleColor foreground;
        public char character;

        public static Color defaultColor()
        {
            Color c;
            c.background = ConsoleColor.Black;
            c.foreground = ConsoleColor.White;
            c.character = ' ';
            return c;
        }
    }

    abstract class Cscge
    {
        Color[,] pixels;
        public int width { get; private set; }
        public int height { get; private set; }

        public void Create(int width, int height)
        {
            pixels = new Color[width, height];
            this.width = width;
            this.height = height;
            Clear();
        }

        public void Clear()
        {
            Clear(Color.defaultColor());
        }

        public void Clear(Color c)
        {
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    pixels[i, j] = c;
                }
        }

        public abstract void update(float dTime);

        public abstract void onCreate();

        public void draw(int x, int y, char c)
        {
            pixels[x, y].character = c;
        }

        private void render()
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write(pixels[i, j].character);
                }
                Console.WriteLine();
            }
        }

        public void run()
        {
            Console.SetWindowSize(width, height);

            onCreate();
            var time = new Stopwatch();
            time.Start();
            float deltaTime = 0;

            float counter = 0;


            while (true)
            {

                deltaTime = time.ElapsedMilliseconds;
                deltaTime /= 1000f;
                time.Restart();

                update(deltaTime);

                render();

                counter += deltaTime;
                if (counter >= 1)
                {
                    counter = 0;
                    Console.Title = (1f / deltaTime).ToString();

                }
            }
        }

    }
}
