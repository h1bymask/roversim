using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Terrain
    {
        private readonly float[] _vertices;

        private Shader _shader;

        //private int _vertexBufferObject;
        private int _vaoterrain;
        private Bitmap heightmap;
        private float scale;

        private int _terrainBufferObject;

        private float limitAngle = 45;

        public Terrain(FileInfo heightmapFile)
        {
            //_vertexBufferObject = _vBO;
            heightmap = new Bitmap(heightmapFile.FullName);
            _vertices = new float[heightmap.Width * heightmap.Height * 3 * 3 * 2];
            scale = 7;
            int a = 0;
            for (int i = 0; i < heightmap.Width - 1; i++)
            {
                for (int j = 0; j < heightmap.Height - 1; j++)
                {
                    Color c = heightmap.GetPixel(i, j);
                    //Console.WriteLine(c);
                    _vertices[a++] = i * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = j * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = j * scale;

                    c = heightmap.GetPixel(i + 1, j);
                    _vertices[a++] = (i + 1) * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = j * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = j * scale;

                    c = heightmap.GetPixel(i, j + 1);
                    _vertices[a++] = i * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = (j + 1) * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = (j + 1) * scale;

                    c = heightmap.GetPixel(i + 1, j + 1);
                    _vertices[a++] = (i + 1) * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = (j + 1) * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = (j + 1) * scale;

                    c = heightmap.GetPixel(i + 1, j);
                    _vertices[a++] = (i + 1) * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = j * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = j * scale;
                    
                    c = heightmap.GetPixel(i, j + 1);
                    _vertices[a++] = i * scale;
                    _vertices[a++] = (c.R + c.G + c.B) / 9;
                    _vertices[a++] = (j + 1) * scale;
                    //_vertices[a++] = c.R / 4;
                    //_vertices[a++] = (c.R + c.G + c.B) / 12;
                    //_vertices[a++] = (j + 1) * scale;
                }
            }
            //Console.WriteLine(_vertices.Length);
            //Console.WriteLine(heightmap.Width);
            //Console.WriteLine(heightmap.Height);
            /*foreach (var item in _vertices)
            {
                Console.WriteLine(item);
            }*/
           // _vertexBufferObject = GL.GenBuffer();

            _shader = new Shader("./Shaders/Shader.vert", "./Shaders/Shader.frag");
        }

        //public float getHeightAtPosition(int x, int y)
        //{
        //    return heightmap.GetPixel(x, y).R / 4;
        //}

        public Vector2 returnWidthHeight()
        {
            //приходится возвращать с e окрестностью, потому что на расстоянии ближе 0.01 от края машина округляет координату до края
            return new Vector2((heightmap.Width - 2) * scale - 1, (heightmap.Height - 2) * scale - 1);
        }

        public Vector2 returnInitialCoord()
        {
            return new Vector2(2 * scale + 1, 2 * scale + 1);
        }

        public float getHeightAtPosition(float i,float j)
        {
            Color c = heightmap.GetPixel((int)i, (int)j);
            return (c.R + c.G + c.B) / 9;
        }

        public void load()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //GL.Enable(EnableCap.DepthTest);
            //GL.DepthFunc(DepthFunction.Less);
            _shader.Use();

            //_vaoterrain = GL.GenVertexArray();
            GL.GenVertexArrays(1, out _vaoterrain);
            GL.BindVertexArray(_vaoterrain);
            GL.GenBuffers(1, out _terrainBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _terrainBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            //var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        public void render(FrameEventArgs e, Matrix4 model)
        {

            //_shader.SetVector3("objectColor", new Vector3(0.925f, 0.054f, 0.035f));
            _shader.Use();
            _shader.SetMatrix4("view", Program.camera.GetViewMatrix());
            _shader.SetMatrix4("projection", Program.camera.GetProjectionMatrix());

            //_shader.SetVector3("objColor", new Vector3(0.83f, 0.06f, 0.06f));//мой код

            _shader.SetMatrix4("model", model);
            GL.BindVertexArray(_vaoterrain);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length);
        }

        public void destroy(EventArgs e)
        {
            GL.DeleteProgram(_shader.Handle);
            //GL.DeleteBuffer(_vertexBufferObject);
        }

        public float[][] returnIJ(float i, float j)
        {
            float a = ((int)i / (int)scale) * scale;
            float b = ((int)j / (int)scale) * scale;
            float[][] IJ = new float[3][];
            if (i + j <= a + b + scale) IJ[0] = new float[] { a, b };
            else IJ[0] = new float[] { a + scale, b + scale };
            IJ[1] = new float[] { a + scale, b };
            IJ[2] = new float[] { a, b + scale };
            return IJ;
        }

        public float returnHeightOnTriangle(Vector2 D)
        {
            //if (D.X % scale == 0 && D.Y % scale == 0) return getHeightAtPosition(D.X / scale, D.Y / scale);
            //else if (D.X % scale == 0) return ((getHeightAtPosition(D.X / scale, (int)D.Y / (int)scale + 1) - getHeightAtPosition(D.X / scale, (int)D.Y / (int)scale)) * D.Y - getHeightAtPosition(D.X / scale, (int)D.Y / (int)scale + 1) * (((int)D.Y / (int)scale) * scale) + getHeightAtPosition(D.X / scale, (int)D.Y / (int)scale) * (((int)D.Y / (int)scale) * scale + scale)) / scale;
            //else if (D.Y % scale == 0) return ((getHeightAtPosition((int)D.X / (int)scale + 1, D.Y / scale) - getHeightAtPosition((int)D.X / (int)scale, D.Y / scale)) * D.X - getHeightAtPosition((int)D.X / (int)scale + 1, D.Y / scale) * (((int)D.X / (int)scale) * scale) + getHeightAtPosition((int)D.X / (int)scale, D.Y / scale) * (((int)D.X / (int)scale) * scale + scale)) / scale;
            //есть треугольник, заданы 3 его вершины, и есть две координаты i,j точки внутри треугольника, нужно вернуть третью координату - высоту
            
            float[][] IJ = returnIJ(D.X, D.Y);
            Vector3 A = new Vector3(IJ[0][0], IJ[0][1], getHeightAtPosition(IJ[0][0] / scale, IJ[0][1] / scale));
            Vector3 B = new Vector3(IJ[1][0], IJ[1][1], getHeightAtPosition(IJ[1][0] / scale, IJ[1][1] / scale));
            Vector3 C = new Vector3(IJ[2][0], IJ[2][1], getHeightAtPosition(IJ[2][0] / scale, IJ[2][1] / scale));
            return A.Z + ((A.X - D.X) * ((B.Y - A.Y) * (C.Z - A.Z) - (B.Z - A.Z) * (C.Y - A.Y)) + (A.Y - D.Y) * ((B.Z - A.Z) * (C.X - A.X) - (B.X - A.X) * (C.Z - A.Z))) / ((float)((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)));
        }

        public float returnScale()
        {
            return scale;
        }

        public float returnY_XX1Y1X2Y2(Vector2 XY1,Vector2 XY2,float x)
        {
            return (XY1.Y * x - x * XY2.Y + XY2.Y * XY1.X - XY1.Y * XY2.X) / (XY1.X - XY2.X);
        }

        public float angleBTVectors(Vector3 a, Vector3 b)
        {
            return (float)MathHelper.RadiansToDegrees(Math.Acos(Vector3.Dot(a, b) / (a.Length * b.Length)));
        }

        public bool isObstacleForwardX(Vector3 C)
        {
            Vector3 V = new Vector3();
            Vector3 W = new Vector3();
            float a = ((int)C.X / (int)scale) * scale;
            float b = ((int)C.Z / (int)scale) * scale;
            if (C.X + C.Z < a + b + scale)
            {
                V.Z = C.Z;
                V.X = a + b + scale - V.Z;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), V.Z);   //returnHeightOnTriangle(new Vector2(V.X, V.Z));
                W.Z = C.Z;
                W.X = a + scale;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), W.Z);
            }
            else
            {
                V.Z = C.Z;
                V.X = a + scale;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), V.Z);
                W.Z = C.Z;
                W.X = a + b + 2 * scale - W.Z;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 2, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), W.Z);  //returnHeightOnTriangle(new Vector2(W.X, W.Z));
            }

            if (W.Y <= V.Y) return false;

            Vector3 M = V - C;
            Vector3 N = W - V;
            M.Y = 0;

            return angleBTVectors(M, N) > limitAngle;
        }

        public bool isObstacleForwardY(Vector3 C)
        {
            Vector3 V = new Vector3();
            Vector3 W = new Vector3();
            float a = ((int)C.X / (int)scale) * scale;
            float b = ((int)C.Z / (int)scale) * scale;
            if (C.X + C.Z < a + b + scale)
            {
                V.X = C.X;
                V.Z = a + b + scale - V.X;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), V.Z);   //returnHeightOnTriangle(new Vector2(V.X, V.Z));
                W.X = C.X;
                W.Z = b + scale;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(a + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), new Vector2(a, getHeightAtPosition(a / scale, b / scale + 1)), W.X);
            }
            else
            {
                V.X = C.X;
                V.Z = b + scale;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(a + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), new Vector2(a, getHeightAtPosition(a / scale, b / scale + 1)), V.X);
                W.X = C.X;
                W.Z = a + b + 2 * scale - W.X;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b + scale, getHeightAtPosition(a / scale + 1, b / scale + 1)), new Vector2(b + 2 * scale, getHeightAtPosition(a / scale, b / scale + 2)), W.Z);  //returnHeightOnTriangle(new Vector2(W.X, W.Z));
            }

            if (W.Y <= V.Y) return false;

            Vector3 M = V - C;
            Vector3 N = W - V;
            M.Y = 0;

            return angleBTVectors(M, N) > limitAngle;
        }

        public bool isObstacleBackX(Vector3 C)
        {
            Vector3 V = new Vector3();
            Vector3 W = new Vector3();
            float a = ((int)C.X / (int)scale) * scale;
            float b = ((int)C.Z / (int)scale) * scale;
            if (C.X + C.Z < a + b + scale)
            {
                V.Z = C.Z;
                V.X = a;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), V.Z);
                W.Z = C.Z;
                W.X = a + b - W.Z;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale - 1, b / scale + 1)), W.Z);  //returnHeightOnTriangle(new Vector2(W.X, W.Z));
            }
            else
            {
                V.Z = C.Z;
                V.X = a + b + scale - V.Z;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), V.Z);   //returnHeightOnTriangle(new Vector2(V.X, V.Z));
                W.Z = C.Z;
                W.X = a;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), W.Z);
            }

            if (W.Y <= V.Y) return false;

            Vector3 M = V - C;
            Vector3 N = W - V;
            M.Y = 0;

            return angleBTVectors(M, N) > limitAngle;
        }

        public bool isObstacleBackY(Vector3 C)
        {
            Vector3 V = new Vector3();
            Vector3 W = new Vector3();
            float a = ((int)C.X / (int)scale) * scale;
            float b = ((int)C.Z / (int)scale) * scale;
            if (C.X + C.Z < a + b + scale)
            {
                V.X = C.X;
                V.Z = b;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(a + scale, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(a, getHeightAtPosition(a / scale, b / scale)), V.X);   //returnHeightOnTriangle(new Vector2(V.X, V.Z));
                W.X = C.X;
                W.Z = a + b - W.X;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(b - scale, getHeightAtPosition(a / scale + 1, b / scale - 1)), new Vector2(b, getHeightAtPosition(a / scale, b / scale)), W.Z);
            }
            else
            {
                V.X = C.X;
                V.Z = a + b + scale - V.X;
                V.Y = returnY_XX1Y1X2Y2(new Vector2(b, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(b + scale, getHeightAtPosition(a / scale, b / scale + 1)), V.Z);
                W.X = C.X;
                W.Z = b;
                W.Y = returnY_XX1Y1X2Y2(new Vector2(a + scale, getHeightAtPosition(a / scale + 1, b / scale)), new Vector2(a, getHeightAtPosition(a / scale, b / scale)), W.X);  //returnHeightOnTriangle(new Vector2(W.X, W.Z));
            }

            if (W.Y <= V.Y) return false;

            Vector3 M = V - C;
            Vector3 N = W - V;
            M.Y = 0;

            return angleBTVectors(M, N) > limitAngle;
        }

    }
}
