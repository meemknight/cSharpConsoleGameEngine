using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace cSharpConsoleGameEngine
{

    public struct Pixel
    {
        public static Pixel create(char c, ConsoleColor f = ConsoleColor.White, ConsoleColor b = ConsoleColor.Black)
        {
            Pixel p;
            p.background = b;
            p.foreground = f;
            p.character = c;
            return p;
        }

        public ConsoleColor background;
        public ConsoleColor foreground;
        public char character;

        public static Pixel defaultColor()
        {
            Pixel c;
            c.background = ConsoleColor.Black;
            c.foreground = ConsoleColor.White;
            c.character = ' ';
            return c;
        }

        public static Pixel defaultColor(char chr)
        {
            Pixel c;
            c.background = ConsoleColor.Black;
            c.foreground = ConsoleColor.White;
            c.character = chr;
            return c;
        }

        public static implicit operator Pixel(char c)
        {
            return Pixel.defaultColor(c);
        }
    }



    abstract class Cscge
    {

        [DllImport("User32.dll")]
        public static extern Int32 GetAsyncKeyState(Int32 i);

        public bool isKeyPressed(char key)
        {
            return GetAsyncKeyState(key) != 0;
        }

        Pixel[,] pixels;
        public int width { get; private set; }
        public int height { get; private set; }
        public Pixel clearColor = Pixel.defaultColor();

        public void Create(int width, int height)
        {
            pixels = new Pixel[width, height];
            this.width = width;
            this.height = height;
            Clear();
        }

        private void Clear()
        {
            Clear(Pixel.defaultColor());
        }

        private void Clear(Pixel c)
        {
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    pixels[i, j] = c;
                }
        }

        public abstract void update(float dTime);

        public virtual void onCreate() { }

        public void draw(int x, int y, Pixel p)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
                pixels[x, y] = p;
        }

        public void fill(int x, int y, int w, int h, Pixel p)
        {
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    draw(i, j, p);
                }
            }
        }

        public void drawLine(int x1, int y1, int x2, int y2, Pixel p)
        {
            /*
             source:
             https://github.com/OneLoneCoder/videos/blob/master/olcConsoleGameEngine.h
            */
            int x, y, dx, dy, dx1, dy1, px, py, xe, ye, i;
            dx = x2 - x1; dy = y2 - y1;
            dx1 = Math.Abs(dx); dy1 = Math.Abs(dy);
            px = 2 * dy1 - dx1; py = 2 * dx1 - dy1;
            if (dy1 <= dx1)
            {
                if (dx >= 0)
                {
                    x = x1; y = y1; xe = x2;
                }
                else
                {
                    x = x2; y = y2; xe = x1;
                }

                draw(x, y, p);

                for (i = 0; x < xe; i++)
                {
                    x = x + 1;
                    if (px < 0)
                        px = px + 2 * dy1;
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) y = y + 1; else y = y - 1;
                        px = px + 2 * (dy1 - dx1);
                    }
                    draw(x, y, p);
                }
            }
            else
            {
                if (dy >= 0)
                {
                    x = x1; y = y1; ye = y2;
                }
                else
                {
                    x = x2; y = y2; ye = y1;
                }

                draw(x, y, p);

                for (i = 0; y < ye; i++)
                {
                    y = y + 1;
                    if (py <= 0)
                        py = py + 2 * dx1;
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) x = x + 1; else x = x - 1;
                        py = py + 2 * (dx1 - dy1);
                    }
                    draw(x, y, p);
                }
            }

        }

        private void render()
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.BackgroundColor = pixels[i, j].background;
                    Console.ForegroundColor = pixels[i, j].foreground;
                    Console.Write(pixels[i, j].character);
                }
                Console.WriteLine();
            }
        }

        public void run()
        {
            Console.SetWindowSize(width + 1, height + 1);

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
                Clear(clearColor);

                counter += deltaTime;
                if (counter >= 1)
                {
                    counter = 0;
                    Console.Title = (1f / deltaTime).ToString();
                    Console.CursorVisible = false;
                }
            }
        }

    }
}
