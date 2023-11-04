using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;

namespace Graphics_Homework
{
    class Scene : GameWindow
    {
        Color4 DEFAULT_BG_COLOR = Color4.LightBlue;

        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
       
        Cube cube = new Cube();
        Plane plane = new Plane();
        Vector3 cameraPosition = new Vector3(30, 30, 30);

        float cameraToOriginAngle = 45;
        float cameraToOriginRadius = 30;

        public Scene(string windowTitle) : base(800, 600, new GraphicsMode(32, 24, 0, 8), windowTitle)
        {
            VSync = VSyncMode.On;

            ConsolePrintHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(DEFAULT_BG_COLOR);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            previousMouse = new MouseState();
            previousKeyboard = new KeyboardState();

            cube.setRotationAxis(new Vector3(0, 1, 0));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // Set Perspective
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            
            // Set Camera View
            Matrix4 lookat = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState thisKeyboard = Keyboard.GetState();
            MouseState thisMouse = Mouse.GetState();

            if (thisKeyboard[Key.Escape])
            {
                Exit();
            }

            // Movement of the cube on the plane and further...
            if (thisKeyboard[Key.Up])
            {
                cube.setPosition(new Vector3(0, 0, 1));
            }
            if (thisKeyboard[Key.Down])
            {
                cube.setPosition(new Vector3(0, 0, -1));
            }
            if (thisKeyboard[Key.Left])
            {
                cube.setPosition(new Vector3(1, 0, 0));
            }
            if (thisKeyboard[Key.Right])
            {
                cube.setPosition(new Vector3(-1, 0, 0));
            }

            // Rotate the cube on anticlockwise on "X" Key Holding
            if (thisKeyboard[Key.X])
            {

                cube.setRotationAngle(5);
            }
            // Rotate the cube on clockwise on "Z" Key Holding
            if (thisKeyboard[Key.Z])
            {
                cube.setRotationAngle(-5);
            }

            // Change cube's face colors to random ones on "C" key press
            if (thisKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                Randomizer rando = new Randomizer();
                cube.changeFaceColors(rando);
            }

            // Camera movement around the center using the middle button and mouse move
            if (thisMouse[MouseButton.Middle])
            {
                cameraToOriginAngle = (cameraToOriginAngle + ((thisMouse.X - previousMouse.X)/(float)100.0));
                cameraPosition.X = (float)(0 + Math.Cos(cameraToOriginAngle) * cameraToOriginRadius);
                cameraPosition.Z = (float)(0 + Math.Sin(cameraToOriginAngle) * cameraToOriginRadius);

                cameraToOriginRadius += (thisMouse.Y - previousMouse.Y) / (float)10;
            }

            if (thisMouse.ScrollWheelValue > previousMouse.ScrollWheelValue && cube.getSize()<plane.getSize())
            {
                cube.increaseSize(1);
            }
            if (thisMouse.ScrollWheelValue < previousMouse.ScrollWheelValue && cube.getSize()>1)
            {
                cube.decreaseSize(1);
            }
            // Save Mouse and Keyboard state for next event compare
            previousMouse = thisMouse;
            previousKeyboard = thisKeyboard;

            // Set Camera View
            Matrix4 lookat = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // Render Code
            plane.DrawPlane();
            cube.DrawCube();
            // End Render Code


            SwapBuffers();
        }

        private void ConsolePrintHelp()
        {
            Console.WriteLine("\n\tMENIU");
            Console.WriteLine(" ESC - parasire program");
            Console.WriteLine(" H - afisare meniu (help)");
            Console.WriteLine(" R - resetare scena la valori implicite");
            Console.WriteLine(" B - schimbare culoare de fundal (randomizat)");
        }
    }
}
