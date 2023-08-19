using Hommage;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hommage
{
    public class Window : GameWindow
    {
        //public 

        //private
        private Shader _shader;
        private Stopwatch _timer;
        private Matrix4 _projection;
        private readonly Color4<Rgba> _backgroundColor;

        private List<Rectangle> _rectangles;

        public Window(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            base.RenderFrequency = 60;
            _backgroundColor = new Color4<Rgba>(0.5f, .5f, .5f, 1f);
            _rectangles = new List<Rectangle>();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(_backgroundColor);
            _timer = new Stopwatch();
            _timer.Start();

            
            _shader = new("Shader/shader.vert", "Shader/shader.frag");
            _shader.Use();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Console.WriteLine("Game.OnLoad() done");
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();

            foreach (Rectangle rect in _rectangles)
            {
                rect.Draw(_shader);
            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            _projection = Matrix4.CreateOrthographicOffCenter(0, e.Width, e.Height, 0, -1, 1);

            int projectionLoc = GL.GetUniformLocation(_shader.handle, "Projection");
            GL.UniformMatrix4f(projectionLoc, false, _projection);
           
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            _shader.Dispose();
        }

        public void AttachRectangel(Rectangle rectangle)
        {
            _rectangles.Add(rectangle);
        }

        public void DettachRectangel(Rectangle rectangle)
        {
            _rectangles.Remove(rectangle);
        }
    }
}