using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Drawing;

namespace Graphics_Homework
{
    class Cube
    {
        // Cube body parameters
        private const float cubeSize = 1.0f;
        public Vector3 cubePosition { get; set; }
        public float rotationAngle { get; set; }
        public Vector3 rotationAxis { get; set; }
        public float scaleFactor { get; set; }

        // Cube's face colors
        private Color[] FaceColors = new Color[8];
        
        // Polygon Mode cube display (wireframe/fill)
        public bool cubePolygonModeWire {  get; set; }

        public Cube()
        {
            // Initialize cube's default parameters
            this.cubePosition = new Vector3(0.0f, 0.0f, 0.0f);
            this.rotationAngle = 0.0f;
            this.rotationAxis = new Vector3(0.0f, 0.0f, 0.0f);
            this.scaleFactor = 10.0f;

            // Set cube rendering Polygon Mode
            this.cubePolygonModeWire = false;

            // Print log information
            Logging.print("Created a cube:\n" +
                " - Size: " + cubeSize + "\n" +
                " - Position: " + this.cubePosition + "\n" +
                " - Rotation (Angle, X, Y, Z): " + this.rotationAngle +
                " " + this.rotationAxis + "\n" +
                " - Scale Factor: " + this.scaleFactor + "\n");

            // Create cube's vertex colors
            this.generateFaceColors();
        }

        public void generateFaceColors()
        { 
            Randomizer _r = new Randomizer();
            Logging.print("Generated cube's vertex colors:");
            for (int i = 0; i < this.FaceColors.Length; i++)
            {
                this.FaceColors[i] = _r.GetRandomColor();
                Logging.print("\tVertex[" + i + "] = " + this.FaceColors[i].ToString());
            }
        }


        /*//Gravitation related properties
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
        private float Size;
        public float cubeSize {
            get
            {
                return this.Size;
            }
            set
            {
                this.Size = value;
                this.position.Y = value + 0.1f;
            }
        }

        private float mass {
            get
            {
                return 2.0f * this.cubeSize;
            }
        }
        private float dt = 0.0f;

        private float cubePosAngle = 0;

        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;*/





        public void UpdatePosition()
        {/*
            // Using semi-implicit euler motion equation to
            // move the object based on force applied
            DateTime DateTimen = DateTime.Now;
            this.dt = (float)((DateTimen - lastUpdateTime).TotalMilliseconds/1000.0);
            
// Modify this horifying piece of text
            this.velocity.Y = (float)Math.Round((Decimal)this.velocity.Y, 0) + (float)Math.Round((force.Y / mass) * dt, 0);
            this.position.Y = (float)Math.Round((Decimal)this.position.Y, 0) + (float)Math.Round(this.velocity.Y * dt, 0);

            this.velocity.X = (float)Math.Round((Decimal)this.velocity.X, 0) + (float)Math.Round((force.X / mass) * dt, 0);
            this.position.X = (float)Math.Round((Decimal)this.position.X, 0) + (float)Math.Round(this.velocity.X * dt, 0);

            this.velocity.Z = (float)Math.Round((Decimal)this.velocity.Z, 0) + (float)Math.Round((force.Z / mass) * dt, 0);
            this.position.Z = (float)Math.Round((Decimal)this.position.Z, 0) + (float)Math.Round(this.velocity.Z * dt, 0);

            // Slowly decrease the force object's moving with
            float forceSecondDecrease = 30.0f;
            if ((force.X != 0.0f || force.Y != 0.0f || force.Z != 0.0f) == true)
            {
                Logging.print("Applied force on:");
            }

            float temp_force = (float)Math.Round((Decimal)(forceSecondDecrease * dt), 1);
            if (this.force.X > 0)
            {
                this.force.X -= temp_force;
                Logging.print("\tX : " + this.force.X.ToString());
            }
            else if (this.force.X < 0)
            {
                this.force.X += temp_force;
                Logging.print("\tX : " + this.force.X.ToString());
            }
            if (this.force.Y > 0)
            {
                this.force.Y -= temp_force;
                Logging.print("\tY : " + this.force.X.ToString());
            }
            else if(this.force.Y < 0)
            {
                this.force.Y += temp_force;
                Logging.print("\tY : " + this.force.X.ToString());
            }
            if (this.force.Z > 0)
            {
                this.force.Z -= temp_force;
                Logging.print("\tZ : " + this.force.X.ToString());
            }
            else if (this.force.Z < 0)
            {
                this.force.Z += temp_force;
                Logging.print("\tZ : " + this.force.X.ToString());
            }

            this.lastUpdateTime = DateTime.Now;*/
        }

        public void DrawCube()
        {
            if (this.cubePolygonModeWire)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            } 

            GL.PushMatrix();

            // Applying transformation on the next to be drawn shape
            GL.Translate(this.cubePosition);
            GL.Rotate(this.rotationAngle, this.rotationAxis);
            GL.Scale(this.scaleFactor, this.scaleFactor, this.scaleFactor);

            // Start drawing the vertex as Quads
            GL.Begin(PrimitiveType.Quads);

            GL.Color4(FaceColors[0]);
            GL.Vertex3(-cubeSize, -cubeSize, -cubeSize);
            GL.Color4(FaceColors[1]);
            GL.Vertex3(-cubeSize, cubeSize, -cubeSize);
            GL.Color4(FaceColors[2]);
            GL.Vertex3(cubeSize, cubeSize, -cubeSize);
            GL.Color4(FaceColors[3]);
            GL.Vertex3(cubeSize, -cubeSize, -cubeSize);

            GL.Color4(FaceColors[0]);
            GL.Vertex3(-cubeSize, -cubeSize, -cubeSize);
            GL.Color4(FaceColors[3]);
            GL.Vertex3(cubeSize, -cubeSize, -cubeSize);
            GL.Color4(FaceColors[6]);
            GL.Vertex3(cubeSize, -cubeSize, cubeSize);
            GL.Color4(FaceColors[7]);
            GL.Vertex3(-cubeSize, -cubeSize, cubeSize);

            GL.Color4(FaceColors[0]);
            GL.Vertex3(-cubeSize, -cubeSize, -cubeSize);
            GL.Color4(FaceColors[7]);
            GL.Vertex3(-cubeSize, -cubeSize, cubeSize);
            GL.Color4(FaceColors[5]);
            GL.Vertex3(-cubeSize, cubeSize, cubeSize);
            GL.Color4(FaceColors[1]);
            GL.Vertex3(-cubeSize, cubeSize, -cubeSize);

            GL.Color4(FaceColors[7]);
            GL.Vertex3(-cubeSize, -cubeSize, cubeSize);
            GL.Color4(FaceColors[6]);
            GL.Vertex3(cubeSize, -cubeSize, cubeSize);
            GL.Color4(FaceColors[4]);
            GL.Vertex3(cubeSize, cubeSize, cubeSize);
            GL.Color4(FaceColors[5]);
            GL.Vertex3(-cubeSize, cubeSize, cubeSize);

            GL.Color4(FaceColors[1]);
            GL.Vertex3(-cubeSize, cubeSize, -cubeSize);
            GL.Color4(FaceColors[5]);
            GL.Vertex3(-cubeSize, cubeSize, cubeSize);
            GL.Color4(FaceColors[4]);
            GL.Vertex3(cubeSize, cubeSize, cubeSize);
            GL.Color4(FaceColors[2]);
            GL.Vertex3(cubeSize, cubeSize, -cubeSize);

            GL.Color4(FaceColors[3]);
            GL.Vertex3(cubeSize, -cubeSize, -cubeSize);
            GL.Color4(FaceColors[2]);
            GL.Vertex3(cubeSize, cubeSize, -cubeSize);
            GL.Color4(FaceColors[4]);
            GL.Vertex3(cubeSize, cubeSize, cubeSize);
            GL.Color4(FaceColors[6]);
            GL.Vertex3(cubeSize, -cubeSize, cubeSize);

            GL.End();

            GL.PopMatrix();

            // Reset rendering polygon mode to fill
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
    }
}