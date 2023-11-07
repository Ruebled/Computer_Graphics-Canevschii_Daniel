using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Graphics_Homework
{
    class Cube
    {
        // Cube body parameters
        private const float cubeSize = 1.0f;
        public Vector3 Position { get; set; }
        public float RotationAngle { get; set; }
        public Vector3 RotationAxis { get; set; }
        public float ScaleFactor { get; set; }

        // Physics related parameters
        public Vector3 Force {  get; set; }
        public Vector3 Acceleration {  get; set; }
        public Vector3 Speed { get; set; }
        private float Mass => cubeSize * ScaleFactor;

        // Cube's face colors
        private readonly Color[] FaceColors = new Color[8];
        
        // Polygon Mode cube display (wireframe/fill)
        public bool CubePolygonModeWire {  get; set; }

        // Saved info
        private DateTime lastDateTime = DateTime.Now;
        private DateTime DateTimen;

        public Cube()
        {
            // Initialize cube's default parameters
            this.Position = new Vector3(0.0f, 0.0f, 0.0f);
            this.RotationAngle = 0.0f;
            this.RotationAxis = new Vector3(0.0f, 0.0f, 0.0f);
            this.ScaleFactor = 10.0f;

            // Set cube rendering Polygon Mode
            this.CubePolygonModeWire = false;

            // Set up Physics variables
            this.Force = Vector3.Zero;
            this.Acceleration = Vector3.Zero;
            this.Speed = Vector3.Zero;

            // Print log information
            Logging.print("Created a cube:\n" +
                " - Size: " + cubeSize + "\n" +
                " - Position: " + this.Position + "\n" +
                " - Rotation (Angle, X, Y, Z): " + this.RotationAngle +
                " " + this.RotationAxis + "\n" +
                " - Scale Factor: " + this.ScaleFactor + "\n" +
                " - Polygon Mode Render Fill");

            // Create cube's vertex colors
            this.GenerateFaceColors();
        }

        public void GenerateFaceColors()
        { 
            Randomizer _r = new Randomizer();
            Logging.print("Generated cube's vertex colors:");
            for (int i = 0; i < this.FaceColors.Length; i++)
            {
                this.FaceColors[i] = _r.GetRandomColor();
                Logging.print("\tVertex[" + i + "] = " + this.FaceColors[i].ToString());
            }
        }
        
        public void UpdatePosition()
        {
            this.DateTimen = DateTime.Now;
            float dt = (float)1/(float)30;

            // F = m*a
            this.Acceleration = new Vector3
            {
                X = this.Force.X / this.Mass,
                Y = this.Force.Y / this.Mass,
                Z = this.Force.Z / this.Mass
            };

            this.Speed = new Vector3()
            {
                X = this.Acceleration.X * dt,
                Y = this.Acceleration.Y * dt,
                Z = this.Acceleration.Z * dt
            };

            Vector3 ChangeInPosition = new Vector3()
            {
                X = this.Speed.X * dt,
                Y = this.Speed.Y * dt,
                Z = this.Speed.Z * dt
            };

            this.Position += ChangeInPosition;

            if(ChangeInPosition != Vector3.Zero)
            {
                Logging.print("X: " + this.Position.X.ToString() + " " +
                "Y: " + this.Position.Y.ToString() + " " +
                "Z: " + this.Position.Z.ToString());
            }

            this.lastDateTime = DateTimen;

            DecreaseForce();
        }

        private void DecreaseForce()
        {

        }
        public void DrawCube()
        {
            if (this.CubePolygonModeWire)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            } 

            GL.PushMatrix();

            // Applying transformation on the next to be drawn shape
            GL.Translate(this.Position);
            GL.Rotate(this.RotationAngle, this.RotationAxis);
            GL.Scale(this.ScaleFactor, this.ScaleFactor, this.ScaleFactor);

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