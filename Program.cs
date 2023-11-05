using System;
using System.Threading;

namespace Graphics_Homework
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Scene scene = new Scene("CubeRenderer"))
            {
                scene.Run(30.0, 0.0);
            }

            Thread.Sleep(1000);

            Environment.Exit(0);
           
            return;
        }
    }
}
