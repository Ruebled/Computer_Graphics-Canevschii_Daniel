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
        Cube cube = new Cube();

        public Scene(string windowTitle) : base(800, 600, new GraphicsMode(32, 24, 0, 8), windowTitle)
        {
            VSync = VSyncMode.On;

            ConsolePrintHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(DEFAULT_BG_COLOR);

            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
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
            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0 );
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
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // Render Code
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
