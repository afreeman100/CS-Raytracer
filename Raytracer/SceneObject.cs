using System;
using System.Numerics;

namespace Raytracer
{
    public interface ISceneObject
    {
        Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction);
    }
}
