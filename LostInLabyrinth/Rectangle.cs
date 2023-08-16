using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Hommage
{
    public class Rectangle
    {
        public float Width;
        public float Height;

        public Vector3 Position;
        public Color4<Rgba> Color;

        private BufferHandle _vertexBufferObject;
        private VertexArrayHandle _vertexArrayObject;

        private Texture _texture;

        private readonly float[] _vertices =
        {
          // positions      // texture
          1f,  1f, 0.0f,    1.0f, 1.0f,   // bottom right
          0f,  1f, 0.0f,    0.0f, 1.0f,   // bottom left
          1f,  0f, 0.0f,    1.0f, 0.0f,    // top right
          0f,  0f, 0.0f,    0.0f, 0.0f,    // top left
        };

        public Rectangle(Texture texture, float height = 0, float width = 0)
        {
            _texture = texture;
            Height = height == 0 ? texture.Height : height;
            Width = width == 0 ? texture.Width : width; 
            Position = new Vector3(100, 100, 0f);
            Color = new Color4<Rgba>(1, 1, 1, 1);

            if (_vertexBufferObject.Handle == 0)
            {
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTargetARB.ArrayBuffer, _vertices, BufferUsageARB.StaticDraw);

                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
            }
        }

        public void Draw(Shader shader)
        {
            int modelLocation = GL.GetUniformLocation(shader.handle, "Model");
            GL.UniformMatrix4f(modelLocation, false, Matrix4.CreateScale(Width, Height, 1) * Matrix4.CreateTranslation(Position));

            GL.ActiveTexture(TextureUnit.Texture0);
            _texture.Bind();

            int textureLoc = GL.GetUniformLocation(shader.handle, "Texture");
            GL.Uniform1i(textureLoc, 0);

            int colorLoc = GL.GetUniformLocation(shader.handle, "Colorize");
            GL.Uniform4f(colorLoc, Color.X, Color.Y, Color.Z, Color.W);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, _vertices.Length);
        }
    }
}
