using System;
using OpenTK;
using OpenTK.Graphics;

namespace LIL
{
    public class Programm
    {
        private static string title = "Lost in Labyrinth";

        public static void Main(string[] args)
        {
            using Window game = new Window(600, 480, title);
            game.Run();
        }
    }
}

