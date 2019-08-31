using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Run
    {
        public static void Main(string[] args)
        {
            Camera camera = new Camera();
            Scene scene = new Scene();
            int resolution = 1000;

            scene.lights.Add(new AmbientLight(0.2));
            scene.lights.Add(new DirectionalLight(new Vector3(1, -0.5F, 0), 0.5));
            scene.lights.Add(new DirectionalLight(new Vector3(-0.2F, 1, 0.4F), 0.1));

            scene.objects.Add(new Sphere(new Vector3(0, 3, 10), 2, Color.FromName("SlateBlue")));
            scene.objects.Add(new Sphere(new Vector3(1, 2, 8), 2, Color.FromName("Green")));
            scene.objects.Add(new Sphere(new Vector3(2, 0, 8), 2, Color.FromName("DarkRed")));

            camera.Render(resolution, scene);
        }
    }
}
