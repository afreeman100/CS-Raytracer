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

            scene.lights.Add(new AmbientLight(0.3));
            scene.lights.Add(new DirectionalLight(new Vector3(1, -0.5F, 0), 0.7));

            scene.objects.Add(new Sphere(new Vector3(-3, 1, 8), 1.5, Color.FromName("LightGray"), 0.6));
            scene.objects.Add(new Sphere(new Vector3(3, -1, 8), 1.5, Color.FromName("White"), 0.9));
            scene.objects.Add(new Sphere(new Vector3(0, 0, 20), 3, Color.FromName("Green"), 0));

            //Tri tri = new Tri(new Vector3(-3, 3, 8),
            //                  new Vector3(0, -3, 8),
            //                  new Vector3(3, 3, 8),
            //                  Color.FromName("LightGray"),
            //                  0.9);

            //scene.objects.Add(tri);

            //scene.objects.Add(new Sphere(new Vector3(0, 3, 10), 2, Color.FromName("SlateBlue")));
            //scene.objects.Add(new Sphere(new Vector3(1, 2, 8), 2, Color.FromName("Green")));
            //scene.objects.Add(new Sphere(new Vector3(2, 0, 8), 2, Color.FromName("DarkRed")));

            //scene.objects.Add(new Sphere(new Vector3(0, -10, 4), 6, Color.FromName("LightGray")));

            camera.Render(resolution, scene);
        }
    }
}
