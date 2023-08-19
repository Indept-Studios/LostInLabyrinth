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
            Initialisation(game);
            game.Run();
        }

        private void Initialisation(Window game)
        {
            _texture = new Texture("Assets/Textures/tux.png");
            _rectangle = new Rectangle(_texture, 100, 100);
            _rectangle.Color = Texture.CreateColor(255, 255, 255);
            game.AttachRectangel(_rectangle);
            
            _rectangle2 = new Rectangle(Texture.WhitePixel, 50, 50);
            _rectangle2.Position = new Vector3(350f, 350f, 0f);
            _rectangle2.Color = Texture.CreateColor(0, 0, 238);
            _rectangle2.Position.X = game.ClientSize.X - _rectangle2.Width;
            game.AttachRectangel(_rectangle2);
        }
    }
}
