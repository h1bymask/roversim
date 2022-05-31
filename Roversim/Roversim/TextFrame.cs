using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MoonSurface
{
    class TextFrame
    {
        private readonly float[] _frame;
        //private int _vertexBufferObject;
        private Shader _frameshader;
        private int _vaoframe;
        
        private int _frameBufferObject;

        private float Width = 1;
        private float Height = 2;

        public TextFrame()
        {
            //_vertexBufferObject = _vBO;
            _frame = new float[]
            {
                -Height/2,-Width/2,0.0f,
                Height/2,-Width/2,0.0f,
                -Height/2,Width/2,0.0f,
                Height/2,Width/2,0.0f,
                Height/2,-Width/2,0.0f,
                -Height/2,Width/2,0.0f,
            };
            //_vertexBufferObject = GL.GenBuffer();
            _frameshader =new Shader("./Shaders/shader2.vert", "./Shaders/shader2.frag");
        }

        public void load()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            _frameshader.Use();
            GL.GenVertexArrays(1,out _vaoframe);
            GL.BindVertexArray(_vaoframe);
            GL.GenBuffers(1, out _frameBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _frameBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _frame.Length * sizeof(float), _frame, BufferUsageHint.StaticDraw);

            //var vertexLocation = _frameshader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        public void render(FrameEventArgs e, Matrix4 model)
        {
            //_frameshader.SetMatrix4("model", model);

            _frameshader.Use();

            _frameshader.SetMatrix4("view", Program.camera.GetViewMatrix());
            _frameshader.SetMatrix4("projection", Program.camera.GetProjectionMatrix());
            _frameshader.SetVector3("objectColor", new Vector3(0.8f, 0.0f, 0.9f));//цвет окошка перед камерой
            //_shader.SetVector3("objectColor", new Vector3(0.83f, 0.06f, 0.06f));

            _frameshader.SetMatrix4("model", model);
            GL.BindVertexArray(_vaoframe);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _frame.Length);
        }

        public void destroy(EventArgs e)
        {
            GL.DeleteProgram(_frameshader.Handle);
            //GL.DeleteBuffer(_vertexBufferObject);
        }

    }
}
