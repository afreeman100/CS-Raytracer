using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Scene
    {
        public List<ISceneObject> objects;
        public List<ISceneLight> lights;

        public Scene()
        {
            objects = new List<ISceneObject>();
            lights = new List<ISceneLight>();
        }


        /*
         * Check if a given ray intersects any objects in the scene and if so, returns the nearest one.
         * Returns: intersection occurred?,  distance,  normal,  scene object
         *      1: whether intersection occurred
         *      2: if so, where along ray the object was hit
         *      3: normal of intersection point
         *      4: scene object that the ray hit     
         */
        public Tuple<bool, double, Vector3, ISceneObject> ClosestIntersection(Vector3 startPoint, Vector3 direction)
        {
            // Record closest intersection, if any occurs
            bool anyIntersections = false;
            double tSmallest = double.MaxValue;
            Tuple<double, Vector3, ISceneObject> i = new Tuple<double, Vector3, ISceneObject>(0, Vector3.UnitX, objects[0]);

            foreach (ISceneObject obj in objects)
            {
                Tuple<double, Vector3> intersection = obj.Intersect(startPoint, direction);
                if (intersection.Item1 > 0.0001 && intersection.Item1 < tSmallest)
                {
                    tSmallest = intersection.Item1;
                    i = new Tuple<double, Vector3, ISceneObject>(intersection.Item1, intersection.Item2, obj);
                    anyIntersections = true;
                }
            }
            return new Tuple<bool, double, Vector3, ISceneObject>(anyIntersections, i.Item1, i.Item2, i.Item3);
        }
    }
}
