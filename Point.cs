using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;

namespace Graphics_Homework
{
    class Point
    {
        private Vector3 position;
        private Color color;
        private float size;

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
            GL.Color3(this.color);
            GL.Vertex3(position);
            GL.End();
        }
    }
}