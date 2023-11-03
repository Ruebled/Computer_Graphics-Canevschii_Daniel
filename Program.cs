using System;

namespace Canevschii
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
        }
    }
}
