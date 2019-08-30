using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    /*
     * Interface for scene objects. These must be able to:
     *      - Calculate intersection with a ray
     *      - Calculate the color of a specific point, based on lighting and other scene objects
     */
    public interface ISceneObject
    {
        Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction);
        Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection);
    }


    /*
     * Must be able to determine if a light is shining on an object
     */
    public interface ISceneLight
    {
        double Intensity(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection);
    }


    /*
     * Small class to store the objects and lights in a scene
     */
    public class Scene
    {
        public List<ISceneObject> objects;
        public List<ISceneLight> lights;

        public Scene()
        {
            objects = new List<ISceneObject>();
            lights = new List<ISceneLight>();
        }
    }
}
