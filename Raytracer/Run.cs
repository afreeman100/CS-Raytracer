using System;

namespace Raytracer
{
    public class Run
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Rendering...");

            Camera camera = new Camera("img");
            int resolution = 600;

            Scene scene = SceneBuilder.SceneThree();
            //Scene scene = SceneBuilder.SphereGenerator(50);
            //Scene scene = SceneBuilder.SceneTwo();

            camera.Render(resolution, scene);
        }
    }
}
