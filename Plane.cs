using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using System.Drawing;

namespace Graphics_Homework
{
    class Plane
    {
        private float planeSize;
        private Vector3 planePosition;

        public Plane()
        {
            this.planeSize = 18.0f;
            this.planePosition = Vector3.Zero;
        }

        public float getSize()
        {
            return this.planeSize;
        }
        public void DrawPlane()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.FromArgb(100, 34, 89, 123));
            GL.Vertex3(-this.planeSize, 0, -this.planeSize);
            GL.Vertex3(-this.planeSize, 0, this.planeSize);
            GL.Vertex3(this.planeSize, 0, this.planeSize);
            GL.Vertex3(this.planeSize, 0, -this.planeSize);

            GL.End();

            GL.PopMatrix();
        }
    }
}