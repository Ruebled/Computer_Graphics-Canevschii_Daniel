using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;

namespace Graphics_Homework
{
    class Scene : GameWindow
    {
        // BG colour used as scene color
        Color4 DEFAULT_BG_COLOR = Color4.LightBlue;

        // Define Mouse&Keyboard states for key press/movement events
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;

        // Camera specific variables
        float cameraToOriginAngle = 45;
        float cameraToOriginRadius = 30;
        Vector3 cameraPosition = new Vector3(30, 30, 30);

        // Objects or array of objects to init for further rendering purpose
        Cube cube = new Cube();
        Plane plane = new Plane();

        public Scene(string windowTitle) : base(800, 600, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 24, 0, 10), windowTitle)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set BG_Color of the window
            GL.ClearColor(DEFAULT_BG_COLOR);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            // Enable anti-aliasing
            GL.Enable(EnableCap.Multisample);

            // Properties for 3D visual enhancing
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // Properties for gradiend enabling
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // Save initial mouse&keyboard states
            previousMouse = new MouseState();
            previousKeyboard = new KeyboardState();
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

            // Exit application on ESC key
            if (thisKeyboard[Key.Escape])
            {
                Logging.print("Exit Application\n");
                Exit();
            }

            // Movement of the cube on the plane and further...
            if (thisKeyboard[Key.Up])
            {
                
            }
            if (thisKeyboard[Key.Down])
            {
                
            }
            if (thisKeyboard[Key.Left])
            {
                
            }
            if (thisKeyboard[Key.Right])
            {
                
            }

            // Rotate the cube on anticlockwise on "X" Key Holding
            if (thisKeyboard[Key.X])
            {

            }
            // Rotate the cube on clockwise on "Z" Key Holding
            if (thisKeyboard[Key.Z])
            {
               
            }

            // Change cube's face colors to random ones on "C" key press
            if (thisKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cube.generateFaceColors();
            }

            if (thisKeyboard[Key.W] && !previousKeyboard[Key.W])
            {
                cube.cubePolygonModeWire ^= true;
                if (cube.cubePolygonModeWire)
                {
                    Logging.print("Set cube PolygonMode to Wire");
                }
                else
                {
                    Logging.print("Set cube PolygonMode to Fill");
                }
            }

            // Camera movement around the center using the middle button and mouse move
            if (thisMouse[MouseButton.Middle])
            {
                cameraToOriginAngle = (cameraToOriginAngle + ((thisMouse.X - previousMouse.X)/(float)100.0));
                cameraPosition.X = (float)(0 + Math.Cos(cameraToOriginAngle) * cameraToOriginRadius);
                cameraPosition.Z = (float)(0 + Math.Sin(cameraToOriginAngle) * cameraToOriginRadius);

                cameraToOriginRadius += (thisMouse.Y - previousMouse.Y) / (float)10;

                // Set Camera View
                Matrix4 lookat = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 0, 0, 0, 0, 1, 0);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref lookat);

                Logging.print("Camera View Set To:" 
                    + " X:" + cameraPosition.X
                    + " Y:" + cameraPosition.Y
                    + " Z:" + cameraPosition.Z);
            }

            if (thisMouse.ScrollWheelValue > previousMouse.ScrollWheelValue && cube.scaleFactor<plane.getSize())
            {
                cube.scaleFactor += 1.0f;
                Logging.print("Scale Factor = " + cube.scaleFactor);
            }

            if (thisMouse.ScrollWheelValue < previousMouse.ScrollWheelValue && cube.scaleFactor>1)
            {
                cube.scaleFactor -= 1.0f;
                Logging.print("Scale Factor = " + cube.scaleFactor);
            }
            
            // Save Mouse and Keyboard state for next event compare
            previousMouse = thisMouse;
            previousKeyboard = thisKeyboard;

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
    }
}
