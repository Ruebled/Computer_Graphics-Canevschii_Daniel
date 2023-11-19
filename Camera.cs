using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_Homework
{
    class Camera
    {

        // Camera specific variables
        float cameraToOriginAngle = 45;
        float cameraToOriginRadius = 30;
        Vector3 cameraPosition;


        public Camera()
        {
            cameraPosition = new Vector3(30, 30, 30);
        }

        public Vector3 getPosition() { return cameraPosition; }

        public void update(MouseState thisMouse, MouseState previousMouse)
        {
            cameraToOriginAngle += ((thisMouse.X - previousMouse.X) / (float)100.0);
            cameraPosition.X = (float)(0 + Math.Cos(cameraToOriginAngle) * cameraToOriginRadius);
            cameraPosition.Z = (float)(0 + Math.Sin(cameraToOriginAngle) * cameraToOriginRadius);

            cameraToOriginRadius += (thisMouse.Y - previousMouse.Y) / (float)10;

            this.render();

            Logging.print(
                "Camera View Set To:"
                + " X:" + cameraPosition.X
                + " Y:" + cameraPosition.Y
                + " Z:" + cameraPosition.Z
                );
        }

        public void render()
        {

            Matrix4 lookat = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }
    }
}
