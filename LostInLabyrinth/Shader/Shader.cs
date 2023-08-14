using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Reflection.Metadata;

namespace LostInLabyrinth
{
    internal class Shader : IDisposable
    {
        //public
        public ProgramHandle handle { get; }

        //private
        private ShaderHandle FragmentShader;
        private ShaderHandle VertexShader;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource = File.ReadAllText(vertexPath);

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);
            int success = 0;
            GL.GetShaderi(VertexShader, ShaderParameterName.CompileStatus, ref success);
            if (success == 0)
            {
                GL.GetShaderInfoLog(VertexShader, out var infoLog);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShaderi(FragmentShader, ShaderParameterName.CompileStatus, ref success);
            if (success == 0)
            {
                GL.GetShaderInfoLog(FragmentShader, out var infoLog);
                Console.WriteLine(infoLog);
            }

            handle = GL.CreateProgram();

            GL.AttachShader(handle, VertexShader);
            GL.AttachShader(handle, FragmentShader);

            GL.LinkProgram(handle);

            GL.GetProgrami(handle, ProgramPropertyARB.LinkStatus, ref success);
            if (success == 0)
            {
                GL.GetProgramInfoLog(handle, out var infoLog);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(handle, VertexShader);
            GL.DetachShader(handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(handle, attribName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(handle);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(handle);
        }
    }
}
