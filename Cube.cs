using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;

namespace Graphics_Homework
{
    class Cube
    {
        private float cubesize;
        private Vector3 position = Vector3.Zero;

        private Vector3 rotation = Vector3.Zero;
        private float alpha;


        public Cube()
        {
            this.cubesize = 1.0f;
            this.alpha = 0.0f;
        }

        public void DrawCube()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Translate(position);
            GL.Rotate(alpha, rotation);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(-cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, -cubesize);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);
            GL.Vertex3(-cubesize, -cubesize, cubesize);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(-cubesize, -cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, -cubesize);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-cubesize, -cubesize, cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-cubesize, cubesize, -cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);

            GL.End();

            GL.PopMatrix();
        }
    }
}