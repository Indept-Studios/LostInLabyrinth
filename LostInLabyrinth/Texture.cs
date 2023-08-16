using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace Hommage
{
    public class Texture
    {
        private Configuration _configuration;
        private TextureHandle _handle;

        private static Texture _whitePixel;
        public static Texture WhitePixel
        {
            get
            {
                if (_whitePixel == null)
                {
                    _whitePixel = new Texture(1, 1);
                    ReadOnlySpan<Rgba32> data = stackalloc Rgba32[] { new Rgba32(255, 255, 255, 255) };
                    _whitePixel.UploadTexture(data);
                }
                return _whitePixel;
            }
        }

        public int Height { get; }
        public int Width { get; }

        public string Filename { get; }

        public Texture(int width, int height)
        {
            Filename = "[UNKNOWN]";
            Width = width;
            Height = height;

            CreateTexture();

        }

        public Texture(string fileName)
        {
            _configuration = Configuration.Default.Clone();
            _configuration.PreferContiguousImageBuffers = true;
            var decoderOptions = new DecoderOptions() { Configuration = _configuration };
            using var img = Image.Load<Rgba32>(decoderOptions, fileName);
            if (!img.DangerousTryGetSinglePixelMemory(out var mem))
                throw new NotSupportedException();


            Filename = Path.GetFileNameWithoutExtension(fileName);
            Width = img.Width;
            Height = img.Height;

            CreateTexture();
            UploadTexture<Rgba32>(mem.Span);
        }

        private void UploadTexture<T>(ReadOnlySpan<T> data) where T : unmanaged
        {
            GL.TexSubImage2D<T>(TextureTarget.Texture2d, 0, 0, 0, Width, Height, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        }

        private void CreateTexture()
        {

            _handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2d, _handle);
            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba8, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, (nint)0);

            // Standart Parameter die gesetzt werden müssen
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2d, _handle);
        }

        /// <summary>
        /// Change the given integers to an float RGBA Value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns>RGBA as float</returns>
        public static Color4<Rgba> CreateColor(int r, int g, int b, int a = 255)
        {
            float fr = r / 255f;
            float fg = g / 255f;
            float fb = b / 255f;
            float fa = a / 255f;
            return new Color4<Rgba>(fr, fg, fb,fa);
        }

    }
}
