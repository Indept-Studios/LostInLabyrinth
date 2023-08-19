using System;
using Hommage;


namespace LIL
{
    public class Game
    {
        // public
        public string Title { get; } = "Lost in Labyrinth";

        //private
        private Rectangle _rectangle;
        private Rectangle _rectangle2;
        private Texture _texture;

        public Game()
        {
            using Window game = new Window(600, 480, Title);
            game.Run();

            Initialisation();
        }

        private void Initialisation()
        {
            _rectangle = new(_texture, 100, 100);
            _rectangle.Color = Texture.CreateColor(110, 110, 110);

            _rectangle2 = new Rectangle(Texture.WhitePixel, 50, 50);
            _rectangle2.Position = new Vector3(350f, 350f, 0f);
            _rectangle2.Color = Texture.CreateColor(0, 0, 238);
        }
    }
}
