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
            scene.lights.Add(new DirectionalLight(new Vector3(0.5F, -1, 0.5F), 0.7));

            scene.objects.Add(new Sphere(new Vector3(3, 1, 18), 1, Color.FromName("Blue"), 0.05));
            scene.objects.Add(new Sphere(new Vector3(-3, -1, 15), 1, Color.FromName("Green"), 0.1));
            scene.objects.Add(new Sphere(new Vector3(0, 0, 12), 2, Color.FromName("LightGray"), 0.7));

            // Floor
            scene.objects.Add(new Tri(new Vector3(100, -3, 100),
                              new Vector3(-100, -3, 100),
                              new Vector3(100, -3, -100),
                              Color.FromArgb(230, 230, 250),
                              0.25));
            scene.objects.Add(new Tri(new Vector3(-100, -3, -100),
                              new Vector3(100, -3, -100),
                              new Vector3(-100, -3, 100),
                              Color.FromArgb(230, 230, 250),
                              0.25));

            // Pink wall
            scene.objects.Add(new Tri(
                              new Vector3(0, -3, 25),
                              new Vector3(10, -3, 14),
                              new Vector3(0, 4, 25),
                              Color.FromArgb(150, 0, 150),
                              0));
            scene.objects.Add(new Tri(
                              new Vector3(10, 4, 14),
                              new Vector3(0, 4, 25),
                              new Vector3(10, -3, 14),
                              Color.FromArgb(150, 0, 150),
                              0));

            // Blue wall
            scene.objects.Add(new Tri(
                              new Vector3(0, -3, 25),
                              new Vector3(0, 4, 25),
                              new Vector3(-10, -3, 14),                              
                              Color.FromArgb(0, 0, 150),
                              0));
            scene.objects.Add(new Tri(
                              new Vector3(-10, 4, 14),
                              new Vector3(-10, -3, 14),
                              new Vector3(0, 4, 25),                              
                              Color.FromArgb(0, 0, 150),
                              0));



            camera.Render(resolution, scene);
        }
    }
}
