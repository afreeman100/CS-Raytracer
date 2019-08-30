using System;
using System.Collections.Generic;
using System.Numerics;

namespace Raytracer

{
    public class Run
    {
        public static void Main(string[] args)
        {
            Camera camera = new Camera();
            List<ISceneObject> objects = new List<ISceneObject>();
            Tuple<int, int> resolution = new Tuple<int, int>(100, 100);

            objects.Add(new Sphere(new Vector3(0, 3, 10), 2));
            objects.Add(new Sphere(new Vector3(2, 0, 8), 2));


            camera.Render(resolution, objects);

        }
    }



}