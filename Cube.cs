using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Drawing;

namespace Graphics_Homework
{
    class Cube
    {
        private float cubesize;
        private float cubePosAngle = 0;

        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;

        private Color[] FaceColors = new Color[]
        {
            Color.Silver,
            Color.Honeydew,
            Color.Moccasin,
            Color.IndianRed,
            Color.PaleVioletRed,
            Color.ForestGreen
        };

        public Cube()
        {
            this.cubesize = 1.0f;
            this.position.Y += this.cubesize + (float)0.1;
        }

        public void increaseSize(float size)
        {
            this.cubesize += size;
            this.position.Y = this.cubesize+(float)0.1;
        }
        public void decreaseSize(float size)
        {
            this.cubesize -= size;
            this.position.Y = this.cubesize+(float)0.1;
        }

        public float getSize()
        {
            return this.cubesize;
        }

        public void setPosition(Vector3 position)
        {
            this.position.X += position.X;
            this.position.Y += position.Y;
            this.position.Z += position.Z;
        }

        public void setRotationAngle(float PosAngle)
        {
            this.cubePosAngle += PosAngle;
        }

        public void setRotationAxis(Vector3 rotation)
        {
            this.rotation.X = rotation.X;
            this.rotation.Y = rotation.Y;  
            this.rotation.Z = rotation.Z;
        }

        public Vector3 getPosition()
        {
            return this.position;
        }

        public void changeFaceColors(Randomizer _r)
        {
            for(int i = 0;i<this.FaceColors.Length;i++)
            {
                this.FaceColors[i] = _r.GetRandomColor();
            }
        }

        public void DrawCube()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Translate(position);

            GL.Rotate(this.cubePosAngle, this.rotation);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(FaceColors[0]);
            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(-cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, -cubesize);

            GL.Color3(FaceColors[1]);
            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);
            GL.Vertex3(-cubesize, -cubesize, cubesize);

            GL.Color3(FaceColors[2]);

            GL.Vertex3(-cubesize, -cubesize, -cubesize);
            GL.Vertex3(-cubesize, -cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, -cubesize);

            GL.Color3(FaceColors[3]);
            GL.Vertex3(-cubesize, -cubesize, cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);

            GL.Color3(FaceColors[4]);
            GL.Vertex3(-cubesize, cubesize, -cubesize);
            GL.Vertex3(-cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);

            GL.Color3(FaceColors[5]);
            GL.Vertex3(cubesize, -cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, -cubesize);
            GL.Vertex3(cubesize, cubesize, cubesize);
            GL.Vertex3(cubesize, -cubesize, cubesize);

            GL.End();

            GL.PopMatrix();
        }
    }
}