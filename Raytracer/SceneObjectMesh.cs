using System;
using System.Numerics;

namespace Raytracer
{
    public class SceneObjectMesh : ISceneObject
    {
        public SceneObjectMesh(String fileName)
        {

            Console.WriteLine("yey);
        }

        public Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }
}
