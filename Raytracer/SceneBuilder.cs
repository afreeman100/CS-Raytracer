using System;
using System.Drawing;
using System.Numerics;


namespace Raytracer
{
    public static class SceneBuilder
    {
        public static Scene SphereGenerator(int numSpheres)
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
                scene.AddObject(new SceneObjects.Sphere(new Vector3((float)p1, (float)p2, (float)p3), rad, c, reflect));
            }

            double DoubleInRange(double dMax, double dMin) { return rnd.NextDouble() * (dMax - dMin) + dMin; }

            // Floor
            scene.AddObject(new SceneObjects.Tri(new Vector3(100, -3, 100),
                              new Vector3(-100, -3, 100),
                              new Vector3(100, -3, -100),
                              Color.FromArgb(230, 230, 250),
                              0.25));
            scene.AddObject(new SceneObjects.Tri(new Vector3(-100, -3, -100),
                              new Vector3(100, -3, -100),
                              new Vector3(-100, -3, 100),
                              Color.FromArgb(230, 230, 250),
                              0.25));


            return scene;
        }

        public static Scene SceneThree()
        {
            Scene scene = new Scene();

            scene.lights.Add(new AmbientLight(0.3));
            scene.lights.Add(new DirectionalLight(new Vector3(0.5F, -1, 0.5F), 0.7));

            // Pink wall
            scene.AddObject(SceneObjects.Composites.Quad(
                            new Vector3(0, -30, 41),
                            new Vector3(10, -30, 14),
                            new Vector3(0, 4, 41),
                            new Vector3(10, 4, 14),
                            Color.FromArgb(150, 0, 150),
                            0));

            // Blue cube
            scene.AddObject(SceneObjects.Composites.Cube(
                                new Vector3(4, -1, 24),
                                new Vector3(4, 5, 24),
                                new Vector3(2, -3, 18),
                                new Vector3(2, 3, 18),

                                new Vector3(-2, -1, 24),
                                new Vector3(-2, 5, 24),
                                new Vector3(-4, -3, 18),
                                new Vector3(-4, 3, 18),
                                Color.FromArgb(0, 0, 150),
                                0));

            // Floor
            scene.AddObject(SceneObjects.Composites.Quad(
                                new Vector3(100, -4, 100),
                                new Vector3(-100, -4, 100),
                                new Vector3(100, -4, -100),
                                new Vector3(-100, -4, -100),
                                Color.FromArgb(230, 230, 250),
                                0.2));

            return scene;
        }

        public static Scene SceneTwo()
        {
            Scene scene = new Scene();

            scene.lights.Add(new AmbientLight(0.3));
            scene.lights.Add(new DirectionalLight(new Vector3(0.5F, -1, 0.5F), 0.7));

            // Pink wall
            scene.AddObject(SceneObjects.Composites.Quad(
                            new Vector3(0, -30, 41),
                            new Vector3(10, -30, 14),
                            new Vector3(0, 4, 41),
                            new Vector3(10, 4, 14),
                            Color.FromArgb(150, 0, 150),
                            0));

            // Floor
            scene.AddObject(SceneObjects.Composites.Quad(
                                new Vector3(100, -4, 100),
                                new Vector3(-100, -4, 100),
                                new Vector3(100, -4, -100),
                                new Vector3(-100, -4, -100),
                                Color.FromArgb(230, 230, 250),
                                0));

            // Blue sphere
            scene.AddObject(new SceneObjects.Sphere(
                                new Vector3(1, 3, 15),
                                4.0,
                                Color.FromArgb(0, 0, 150),
                                0));

            return scene;
        }
    }
}
