using System;
using System.Collections.Generic;
using System.Numerics;

namespace Raytracer

{
    public class Run
    {


        public static void Main(string[] args)
        {
            Tuple<int, int> resolution = new Tuple<int, int>(10, 10);
            Camera camera = new Camera();

            ISceneObject sphere = new Sphere(new Vector3(0, 0, 10), 2);
            //sphere.Intersect(new Vector3(0, 1, 0), new Vector3(3, 4, 5));

            List<ISceneObject> objects = new List<ISceneObject>();
            objects.Add(sphere);

            camera.Render(resolution, objects);



        }
    }



}