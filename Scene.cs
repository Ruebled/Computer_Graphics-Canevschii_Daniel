﻿using OpenTK;
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

        // Objects or array of objects to init for further rendering purpose
        readonly Cube cube = new Cube();
        readonly Plane plane = new Plane();
        readonly Camera camera = new Camera();

        static readonly string AssetsFolder = "..\\..\\assets";
        static readonly string[] Assets = new string[]
        {
            "bottle_cap_obj.obj",
            "lowpolytree.obj",
            "slime.obj",
            "soccer_ball.obj",
            "volleyball.obj",
            "Lowpoly_tree_sample.obj"
        };

        readonly ComplexObject complexObject = new ComplexObject(AssetsFolder + "\\" +  Assets[5]);

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

            camera.render();
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

            float ForceStep = 20f;

            // Movement of the cube on the plane and further...
            if (thisKeyboard[Key.Up])
            {
                cube.Force = new Vector3(cube.Force.X + ForceStep, cube.Force.Y, cube.Force.Z);
            }
            if (thisKeyboard[Key.Down])
            {
                cube.Force = new Vector3(cube.Force.X - ForceStep, cube.Force.Y, cube.Force.Z);
            }
            if (thisKeyboard[Key.Left])
            {
                cube.Force = new Vector3(cube.Force.X, cube.Force.Y, cube.Force.Z + ForceStep);
            }
            if (thisKeyboard[Key.Right])
            {
                cube.Force = new Vector3(cube.Force.X, cube.Force.Y, cube.Force.Z - ForceStep);
            }

            // Rotate the cube on anticlockwise on "X" Key Holding
            if (thisKeyboard[Key.X])
            {

            }
            // Rotate the cube on clockwise on "Z" Key Holding
            if (thisKeyboard[Key.Z])
            {
               
            }

            if (thisKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                Logging.SetMarker();
            }

            // Change cube's face colors to random ones on "C" key press
            if (thisKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cube.GenerateFaceColors();
            }

            if (thisKeyboard[Key.W] && !previousKeyboard[Key.W])
            {
                cube.CubePolygonModeWire ^= true;
                if (cube.CubePolygonModeWire)
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
                camera.update(thisMouse, previousMouse);
            }

            if (thisMouse.ScrollWheelValue > previousMouse.ScrollWheelValue && cube.ScaleFactor<plane.getSize())
            {
                cube.ScaleFactor += 1.0f;
                Logging.print("Scale Factor = " + cube.ScaleFactor);
            }

            if (thisMouse.ScrollWheelValue < previousMouse.ScrollWheelValue && cube.ScaleFactor>1)
            {
                cube.ScaleFactor -= 1.0f;
                Logging.print("Scale Factor = " + cube.ScaleFactor);
            }
            
            // Save Mouse and Keyboard state for next event compare
            previousMouse = thisMouse;
            previousKeyboard = thisKeyboard;

            // Update Cube Data
            cube.UpdatePosition();

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
           
            // Render Code

            plane.DrawPlane();
            cube.DrawCube();
            complexObject.Draw();

            // End Render Code

            SwapBuffers();
        }
    }
}
