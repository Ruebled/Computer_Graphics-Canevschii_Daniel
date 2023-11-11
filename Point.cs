using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;

namespace Graphics_Homework
{
    class Point
    {
        private Vector3 position;
        private readonly Color color;
        private readonly float size;

        public Point(float x, float y, float z)
        {
            this.position = new Vector3(x, y, z);
            this.color = Color.Black;
            this.size = 10.0f;
        }

        public void DrawPoint()
        {
            GL.PointSize(this.size);

            GL.Begin(PrimitiveType.Points);
            GL.PointSize(1.0f);
            GL.Color3(this.color);
            GL.Vertex3(position);
            GL.End();
        }
    }
}