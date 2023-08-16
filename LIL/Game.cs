using System;
using Hommage;


namespace LIL
{
    public class Game
    {
        public string Title { get; } = "Lost in Labyrinth";

        public Game()
        {
            using Window game = new Window(600, 480, Title);
            game.Run();
        }
    }
}
