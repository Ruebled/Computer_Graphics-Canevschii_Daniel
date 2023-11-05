﻿using OpenTK;
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

        private bool viewCubeWireframe = false;

        Cube cube = new Cube();
        Plane plane = new Plane();
        Point point = new Point( 10 , 10, -10);

        Vector3 cameraPosition = new Vector3(30, 30, 30);

        float cameraToOriginAngle = 45;
        float cameraToOriginRadius = 30;

        public Scene(string windowTitle) : base(800, 600, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 24, 0, 10), windowTitle)
        {
            VSync = VSyncMode.On;

            ConsolePrintHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(DEFAULT_BG_COLOR);

            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

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

            // Exit application on ESC key
            if (thisKeyboard[Key.Escape])
            {
                Exit();
            }

            // Movement of the cube on the plane and further...
            if (thisKeyboard[Key.Up])
            {
                cube.forceZ += 2f;
            }
            if (thisKeyboard[Key.Down])
            {
                cube.forceZ += -2f;
            }
            if (thisKeyboard[Key.Left])
            {
                cube.forceX += 2f;
            }
            if (thisKeyboard[Key.Right])
            {
                cube.forceX += -2f;
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

            if (thisKeyboard[Key.W] && !previousKeyboard[Key.W])
            {
                this.viewCubeWireframe ^= true;
            }

            // Camera movement around the center using the middle button and mouse move
            if (thisMouse[MouseButton.Middle])
            {
                cameraToOriginAngle = (cameraToOriginAngle + ((thisMouse.X - previousMouse.X)/(float)100.0));
                cameraPosition.X = (float)(0 + Math.Cos(cameraToOriginAngle) * cameraToOriginRadius);
                cameraPosition.Z = (float)(0 + Math.Sin(cameraToOriginAngle) * cameraToOriginRadius);

                cameraToOriginRadius += (thisMouse.Y - previousMouse.Y) / (float)10;
            }

            if (thisMouse.ScrollWheelValue > previousMouse.ScrollWheelValue && cube.cubesize<plane.getSize())
            {
                cube.cubesize += 1;
            }

            if (thisMouse.ScrollWheelValue < previousMouse.ScrollWheelValue && cube.cubesize>1)
            {
                cube.cubesize += -1;
            }
            
            // Save Mouse and Keyboard state for next event compare
            previousMouse = thisMouse;
            previousKeyboard = thisKeyboard;

            // Set Camera View
            Matrix4 lookat = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            // Check for bottom collision then change displacement to -y
            bool isnotColided = false;
            if (isnotColided)
            {
                cube.forceY = -9.8f;
            }
            else
            {
                cube.forceY = 0.0f;
            }
            
            cube.UpdatePosition();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
           
            // Render Code
            plane.DrawPlane();

            if (viewCubeWireframe)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            point.DrawPoint();
            cube.DrawCube();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            
            // End Render Code


            SwapBuffers();
        }

        private void ConsolePrintHelp()
        {
            Console.WriteLine("\n\tMENIU");
            Console.WriteLine(" ESC - parasire program");
        }
    }
}
