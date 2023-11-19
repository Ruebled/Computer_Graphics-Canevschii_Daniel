using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Graphics_Homework
{
    class Object
    {
        // Cube body parameters
        private const float cubeSize = 1.0f;
        public Vector3 Position { get; set; }
        public float RotationAngle { get; set; }
        public Vector3 RotationAxis { get; set; }
        public float ScaleFactor { get; set; }

        // Physics related parameters
        public Vector3 Force { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Speed { get; set; }
        private float Mass => cubeSize * ScaleFactor;

        // Cube's face colors
        private readonly Color[] FaceColors;

        // Polygon Mode cube display (wireframe/fill)
        public bool CubePolygonModeWire { get; set; }

        // Saved info
        private DateTime lastDateTime = DateTime.Now;
        private DateTime DateTimen;

        List<Vector3> Vertices = new List<Vector3>();
        List<Vector3> Normals = new List<Vector3>();
        List<Vector2> Texture = new List<Vector2>();
        List<int[]> Elements = new List<int[]>();

        public Object() 
        {
           
        }

        public Object(String FileName)
        {
            string[] lines = File.ReadAllLines(FileName);

            if (lines == null) { return; }

            foreach (string line in lines)
            {
                if (line.Trim() == String.Empty || line.Length <= 2)
                {
                    continue;
                }
                if (line.Substring(0, 2) == "v ")
                {
                    String[] vec = (line.Substring(2, line.Length - 2)).Trim().Split(' ');
                    Vertices.Add(new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2])));
                }
                if (line.Substring(0, 3) == "vt ")
                {
                    String[] vec = (line.Substring(3, line.Length - 3)).Trim().Split(' ');
                    Texture.Add(new Vector2(float.Parse(vec[0]), float.Parse(vec[1])));

                }
                if (line.Substring(0, 3) == "vn ")
                {
                    String[] vec = (line.Substring(3, line.Length - 3)).Trim().Split(' ');
                    Normals.Add(new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2])));

                }
                if (line.Substring(0, 2) == "f ")
                {
                    String[] face = line.Trim().Split(' ');

                    int[] faceT = new int[face.Length - 1];
                    for (int i = 1; i < face.Length; i++)
                    {
                        int vert = Int32.Parse(face[i].Trim().Split('/')[0]);
                        faceT[i - 1] = vert;
                    }

                    Elements.Add(faceT);
                }

            }

            // Initialize cube's default parameters
            this.Position = new Vector3(0.0f, 10.0f, 0.0f);
            this.RotationAngle = 0.0f;
            this.RotationAxis = new Vector3(0.0f, 0.0f, 0.0f);
            this.ScaleFactor = 1.0f;

            // Set cube rendering Polygon Mode
            this.CubePolygonModeWire = false;

            // Set up Physics variables
            this.Force = new Vector3(0, 0, 0);
            this.Acceleration = Vector3.Zero;
            this.Speed = Vector3.Zero;

            // Create face's color vector
            this.FaceColors = new Color[Elements.Count];
            // Create cube's vertex colors
            this.GenerateFaceColors();

            // To do: Edit this Logging message with more info
            Logging.print("3D object added");
        }

        public void Draw()
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

            // Selecting drawing mode by face vertex numbers
            switch (Elements[0].Length)
            {
                case 2:
                    GL.Begin(PrimitiveType.Lines);
                    break;
                case 3:
                    GL.Begin(PrimitiveType.Triangles);
                    break;
                case 4:
                    GL.Begin(PrimitiveType.Quads);
                    break;
            }

            for (int i = 0; i < Elements.Count; i++)
            {
                GL.Color4(this.FaceColors[i]);
                for (int j = 0; j < Elements[i].Length; j++)
                { 
                    GL.Vertex3(Vertices[Elements[i][j] - 1]);
                }
            }
            GL.End();
            GL.PopMatrix();

            // Reset rendering polygon mode to fill
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }

        public void GenerateFaceColors()
        {
            Randomizer _r = new Randomizer();
            //Logging.print("Generated cube's vertex colors:");
            for (int i = 0; i < this.FaceColors.Length; i++)
            {
                this.FaceColors[i] = _r.GetRandomColor();
                //Logging.print("\tVertex[" + i + "] = " + this.FaceColors[i].ToString());
            }
        }

        public void UpdatePosition()
        {
            this.DateTimen = DateTime.Now;
            float dt = (float)1 / (float)30;

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

            if (ChangeInPosition != Vector3.Zero)
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
        
    }
}
