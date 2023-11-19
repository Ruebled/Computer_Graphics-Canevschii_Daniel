using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;
using System.Runtime.InteropServices;

namespace Graphics_Homework
{
    class Grid
    {
        private readonly Color GRIDCOLOR = Color.WhiteSmoke;
        private const int GRIDSTEP = 10;
        private const int UNITS = 20;
        private const int POINT_OFFSET = GRIDSTEP * UNITS;
        private const int MICRO_OFFSET = 1; // useful because otherwise the axes will be "drown" in overlapping grid lines...

        private int CameraOffsetX;
        private int CameraOffsetY;


        public Grid()
        {
            CameraOffsetX = 0;
            CameraOffsetY = 0;
        }

        public void UpdateGrid(Vector3 cameraPosition)
        {
            CameraOffsetX = (int)(cameraPosition.X/GRIDSTEP) * GRIDSTEP;
            CameraOffsetY = (int)(cameraPosition.Z/GRIDSTEP) * GRIDSTEP;
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(GRIDCOLOR);

            for (int i = -POINT_OFFSET+(CameraOffsetX); i <= POINT_OFFSET+CameraOffsetX; i += GRIDSTEP)
            {
                // XZ plan drawing: parallel with Oz
                GL.Vertex3(i + MICRO_OFFSET, 0, POINT_OFFSET + CameraOffsetY);
                GL.Vertex3(i + MICRO_OFFSET, 0, (-1 * POINT_OFFSET) + CameraOffsetY);
            }
            
            for (int j = -POINT_OFFSET + (CameraOffsetY); j <= POINT_OFFSET + CameraOffsetY; j += GRIDSTEP)
            {
                // XZ plan drawing: parallel with Ox
                GL.Vertex3( POINT_OFFSET +CameraOffsetX, 0, j + MICRO_OFFSET);
                GL.Vertex3((-1 * POINT_OFFSET) + CameraOffsetX, 0, j + MICRO_OFFSET);
            }
            

            GL.End();
        }
    }
}
