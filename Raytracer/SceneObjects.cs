using System;
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
        Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection, int reflections);
    }
}
