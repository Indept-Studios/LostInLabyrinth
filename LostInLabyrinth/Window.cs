using LostInLabyrinth;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;
using System.Drawing;
using System.Windows;

namespace LIL
{
    internal class Window : GameWindow
    {
        //public 

        //private
        private BufferHandle _vertexBufferObject;
        private VertexArrayHandle _vertexArrayObject;
        private BufferHandle elementBufferObject;
        private Shader _shader;
        private Stopwatch _timer;
        private Matrix4 _projection;

        private readonly float[] _vertices =
        {
          // positions        // colors
          450f,  120f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
          150f,  120f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
          300f,  360f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
        };

        public Window(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            base.RenderFrequency = 60;
            _projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1, 1);
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
            var backgroundColor = new Color4<Rgba>(0.2f, .3f, .3f, 1f);
            GL.ClearColor(backgroundColor);
            _timer = new Stopwatch();
            _timer.Start();

            //initialization code goes here
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTargetARB.ArrayBuffer, _vertices, BufferUsageARB.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);


            _shader = new("Shader/shader.vert", "Shader/shader.frag");
            _shader.Use();

            int projectionLoc = GL.GetUniformLocation(_shader.handle, "Projection");
            GL.UniformMatrix4f(projectionLoc, false, _projection);

            Console.WriteLine("Game.OnLoad() done");
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();

            int modelLoc = GL.GetUniformLocation(_shader.handle, "Model");

            List<Matrix4> matr = new List<Matrix4>();
            matr.Add(Matrix4.CreateTranslation(-150, 0, 0));
            matr.Add(Matrix4.CreateTranslation(150, 0, 0));

            foreach (var item in matr)
            {
                GL.UniformMatrix4f(modelLoc, false, item);
                GL.BindVertexArray(_vertexArrayObject);
                GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length);
            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            _shader.Dispose();
        }
    }
}