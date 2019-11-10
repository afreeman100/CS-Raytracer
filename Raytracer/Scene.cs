using System;
using System.Collections.Generic;
using System.Numerics;
using Raytracer.SceneObjects;

namespace Raytracer
{
    public class Scene
    {
        public List<SceneObject> objects;
        public List<ISceneLight> lights;

        public Scene()
        {
            objects = new List<SceneObject>();
            lights = new List<ISceneLight>();
        }

        /// <summary>
        /// Check if a given ray intersects any objects in the scene and if so, returns the nearest one.
        /// Returns: intersection occurred?,  distance,  normal,  scene object   
        /// </summary>
        public Intersection ClosestIntersection(Vector3 startPoint, Vector3 direction)
        {
            double tSmallest = double.MaxValue;
            Intersection intersection = new Intersection();

            foreach (SceneObject obj in objects)
            {
                Tuple<double, Vector3> i = obj.Intersect(startPoint, direction);
                if (i.Item1 > 0.0001 && i.Item1 < tSmallest)
                {
                    tSmallest = i.Item1;
                    intersection = new Intersection(true, i.Item1, i.Item2, obj);
                }
            }
            return intersection; //new Intersection(anyIntersections, intersection.Item1, intersection.Item2, intersection.Item3);
        }
    }
}
