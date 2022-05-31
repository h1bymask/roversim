using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MoonSurface
{
    class Car
    {
        private readonly float[] _car;
        //private int _vertexBufferObject;
        private Shader _carshader;
        private int _vaocar;
        //private int _vertexBufferObject;
        private int _carBufferObject;

        private float carLenght = 10;
        private float carWidth = 10;
        private float carHeight = 10;

        public Car()
        {
            //_vertexBufferObject = _vBO;
            _car = new float[]
            {   
                //-carWidth/2 -> 0, carWidth/2 -> carWidth чтобы центр машинки был на плоскости дна
                carLenght/2,0,-carHeight/2, carLenght/2,carWidth,-carHeight/2, carLenght/2,0,carHeight/2, carLenght/2,carWidth,carHeight/2, carLenght/2,carWidth,-carHeight/2, carLenght/2,0,carHeight/2,//front
                -carLenght/2,0,-carHeight/2, -carLenght/2,carWidth,-carHeight/2, -carLenght/2,0,carHeight/2, -carLenght/2,carWidth,carHeight/2, -carLenght/2,carWidth,-carHeight/2, -carLenght/2,0,carHeight/2,//back
                carLenght/2,0,carHeight/2, carLenght/2,carWidth,carHeight/2, -carLenght/2,0,carHeight/2, -carLenght/2,carWidth,carHeight/2, carLenght/2,carWidth,carHeight/2, -carLenght/2,0,carHeight/2,//up
                carLenght/2,0,-carHeight/2, carLenght/2,carWidth,-carHeight/2, -carLenght/2,0,-carHeight/2, -carLenght/2,carWidth,-carHeight/2, carLenght/2,carWidth,-carHeight/2, -carLenght/2,0,-carHeight/2,//down
                carLenght/2,0,-carHeight/2, -carLenght/2,0,-carHeight/2, carLenght/2,0,carHeight/2, -carLenght/2,0,carHeight/2, -carLenght/2,0,-carHeight/2, carLenght/2,0,carHeight/2,//left
                carLenght/2,carWidth,-carHeight/2, -carLenght/2,carWidth,-carHeight/2, carLenght/2,carWidth,carHeight/2, -carLenght/2,carWidth,carHeight/2, -carLenght/2,carWidth,-carHeight/2, carLenght/2,carWidth,carHeight/2//right
            };
            //_vertexBufferObject = GL.GenBuffer();
            _carshader = new Shader("./Shaders/shader2.vert", "./Shaders/shader2.frag");
        }

        public void load()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            _carshader.Use();
            GL.GenVertexArrays(1, out _vaocar);
            GL.BindVertexArray(_vaocar);
            GL.GenBuffers(1, out _carBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _carBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _car.Length * sizeof(float), _car, BufferUsageHint.StaticDraw);

            //var vertexLocation = _frameshader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        public void render(FrameEventArgs e, Matrix4 model)
        {
            //_frameshader.SetMatrix4("model", model);

            _carshader.Use();

            _carshader.SetMatrix4("view", Program.camera.GetViewMatrix());
            _carshader.SetMatrix4("projection", Program.camera.GetProjectionMatrix());
            _carshader.SetVector3("objectColor", new Vector3(1.0f, 0.0f, 0.0f));//цвет окошка перед камерой
            //_shader.SetVector3("objectColor", new Vector3(0.83f, 0.06f, 0.06f));//мой код

            _carshader.SetMatrix4("model", model);
            GL.BindVertexArray(_vaocar);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _car.Length);
        }

        public void destroy(EventArgs e)
        {
            GL.DeleteProgram(_carshader.Handle);
            //GL.DeleteBuffer(_vertexBufferObject);
        }

    }
}
