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
            int resolution = 4000;

            scene.lights.Add(new AmbientLight(0.3));
            scene.lights.Add(new DirectionalLight(new Vector3(1, -0.5F, 0), 0.7));

            scene.objects.Add(new Sphere(new Vector3(3, 1, 18), 1, Color.FromName("Blue"), 0));
            scene.objects.Add(new Sphere(new Vector3(-3, -1, 15), 1, Color.FromName("Green"), 0.05));
            scene.objects.Add(new Sphere(new Vector3(0, 0, 12), 2, Color.FromName("LightGray"), 0.7));

            // Triangles to act as rectangular floor
            Tri f1 = new Tri(new Vector3(100, -3, 100),                            
                              new Vector3(100, -3, -100),
                              new Vector3(-100, -3, 100),
                              Color.FromName("White"),
                              0.8);

            Tri f2 = new Tri(new Vector3(-100, -3, -100),
                              new Vector3(-100, -3, 100),
                              new Vector3(100, -3, -100),
                              Color.FromName("White"),
                              0.8);

            scene.objects.Add(f1);
            scene.objects.Add(f2);

            camera.Render(resolution, scene);
        }
    }
}
