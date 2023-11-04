using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Drawing;

namespace Graphics_Homework
{
    class Cube
    {
        //Gravitation related properties
        private DateTime lastUpdateTime = DateTime.Now;
       
        private Vector3 force = Vector3.Zero;

        private Vector3 velocity = Vector3.Zero;

        public float forceX
        {
            get { return force.X;}
            set { this.force.X = value; }
        }
        public float forceY
        {
            get { return force.Y; }
            set { this.force.Y = value; }
        }
        public float forceZ
        {
            get { return force.Z; }
            set { this.force.Z = value; }
        }

        private float mass = 2.0f;
        private float dt = 0.0f;
        private Vector3 velocityVector = Vector3.Zero;
        

        public float cubesize { get; set; }

        private float cubePosAngle = 0;

        private Vector3 position = new Vector3(0, 50, 0);
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
        }

        public void UpdatePosition()
        {
            DateTime DateTimen = DateTime.Now;
            this.dt = (float)((DateTimen - lastUpdateTime).TotalMilliseconds/1000.0);
            
            this.velocity.Y += (force.Y / mass) * dt;
            this.position.Y += this.velocity.Y * dt;

            this.velocity.X += (force.X / mass) * dt;
            this.position.X += this.velocity.X * dt;

            this.velocity.Z += (force.Z / mass) * dt;
            this.position.Z = this.velocity.Z * dt;

            float forceSecondDecrease = 30.0f;

            if(this.force.X > 0)
            {
                this.force.X -= forceSecondDecrease * dt;
            }
            else
            {
                this.force.X += forceSecondDecrease * dt;
            }
            if (this.force.Y > 0)
            {
                this.force.Y -= forceSecondDecrease * dt;
            }
            else
            {
                this.force.Y += forceSecondDecrease * dt;
            }
            if (this.force.Z > 0)
            {
                this.force.Z -= forceSecondDecrease * dt;
            }
            else
            {
                this.force.Z += forceSecondDecrease * dt;
            }

            this.lastUpdateTime = DateTime.Now;
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