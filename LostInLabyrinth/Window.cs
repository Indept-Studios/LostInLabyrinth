using LostInLabyrinth;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

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

        //private readonly float[] _vertices =
        // {
        //   0.75f,1f,0f,
        //   1f,1f,0f,
        //   1f,0f,0f,
        //   0.75f,1f,0f,
        //   0.75f,0f,0f,
        //   1f,0f,0f
        //};
        float[] _vertices = {
                             0.5f,  0.5f, 0.0f,  // top right
                             0.5f, -0.5f, 0.0f,  // bottom right
                            -0.5f, -0.5f, 0.0f,  // bottom left
                            -0.5f,  0.5f, 0.0f   // top left
                            };
        uint[] _indices = {0,1,3,
                          1,2,3
                         };

        public Window(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            base.RenderFrequency = 60;
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
            GL.ClearColor(.2f, .3f, .3f, 1f);
            _timer = new Stopwatch();
            _timer.Start();

            //initialization code goes here
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTargetARB.ArrayBuffer, _vertices, BufferUsageARB.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, _indices, BufferUsageARB.StaticDraw);

            _shader = new("Shader/shader.vert", "Shader/shader.frag");
            //_shader.GetAttribLocation("aPos");
            _shader.Use();

            Console.WriteLine("Game.OnLoad() done");
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();

            // code goes here
            // solange nur ein shader, aufruf in OnLoad() besser da weniger performance nötig


            double timeValue = _timer.Elapsed.TotalSeconds;
            float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            int vertexColorLocation = GL.GetUniformLocation(_shader.handle, "vertexColor");
            GL.Uniform4f(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            
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