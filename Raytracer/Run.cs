using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Run
    {
        public static void Main(string[] args)
        {
            Camera camera = new Camera("img");
            int resolution = 2000;

            //Scene scene = BuildSceneOne();
            Scene scene = BuildSceneTwo(50);

            camera.Render(resolution, scene);
        }


        public static Scene BuildSceneOne()
        {
            Scene scene = new Scene();

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

            return scene;
        }


        public static Scene BuildSceneTwo(int numSpheres)
        {
            Scene scene = new Scene();
            Random rnd = new Random();

            scene.lights.Add(new AmbientLight(0.3));
            scene.lights.Add(new DirectionalLight(new Vector3(0.5F, -1, 0.5F), 0.7));

            // Randomly generate spheres within given boundries
            for (int i = 0; i < numSpheres; i++)
            {
                double p1 = DoubleInRange(8, -8);
                double p2 = DoubleInRange(8, -5);
                double p3 = DoubleInRange(16, 12);
                double rad = DoubleInRange(2, 0.5);
                double reflect = DoubleInRange(0.4, 0);
                Color c = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                scene.objects.Add(new Sphere(new Vector3((float)p1, (float)p2, (float)p3), rad, c, reflect));
            }

            double DoubleInRange(double dMax, double dMin) { return rnd.NextDouble() * (dMax - dMin) + dMin; }

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


            return scene;
        }
    }
}
