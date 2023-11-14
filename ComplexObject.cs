using OpenTK;
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;
using System.IO;

namespace Graphics_Homework
{
    class ComplexObject
    {

        List<Vector3> Vertices = new List<Vector3>();
        List<Vector3> Normals = new List<Vector3>();
        List<Vector2> Texture = new List<Vector2>();
        List<int[]> Elements = new List<int[]>();

        public ComplexObject() { }
        public ComplexObject(ComplexObject obj) { }

        public ComplexObject(String FileName)
        {
            string[] lines = File.ReadAllLines(FileName);

            if (lines == null) { return; }

            foreach (string line in lines)
            {
                if(line.Trim() == String.Empty || line.Length<=2)
                {
                    continue;
                }
                if (line.Substring(0, 2) == "v ")
                {
                    String[] vec = (line.Substring(2, line.Length-2)).Trim().Split(' ');
                    Vertices.Add(new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2])));
                }
                if (line.Substring(0, 3) == "vt ")
                {
                    String[] vec = (line.Substring(3, line.Length-3)).Trim().Split(' ');
                    Texture.Add(new Vector2(float.Parse(vec[0]), float.Parse(vec[1])));

                }
                if (line.Substring(0, 3) == "vn ")
                {
                    String[] vec = (line.Substring(3, line.Length-3)).Trim().Split(' ');
                    Normals.Add(new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2])));

                }
                if (line.Substring(0, 2) == "f ")
                {
                    String[] face = line.Trim().Split(' ');

                    int[] faceT = new int[face.Length-1];
                    for(int i = 1; i<face.Length; i++)
                    { 
                        int vert = Int32.Parse(face[i].Trim().Split('/')[0]);
                        faceT[i-1] = vert;    
                    }

                    Elements.Add(faceT);
                }
                
            }
        }

        public void Draw()
        {
            GL.PushMatrix();

            // Start drawing the vertex as Quads
            //GL.Scale(4.0, 4.0, 4.0);

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

            for(int i = 0; i< Elements.Count; i++)
            {
                for(int j = 0; j < Elements[i].Length; j++)
                {
                    GL.Vertex3(Vertices[Elements[i][j]-1]);
                }
            }
            GL.End();
            GL.PopMatrix();
        }
    }
}
