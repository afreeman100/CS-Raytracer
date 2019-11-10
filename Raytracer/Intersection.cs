using System.Numerics;


namespace Raytracer
{

    /// <summary>
    /// <para>DidIntersect: whether intersection occurred</para>
    /// <para>Position: if so, where along ray the object was hit</para>
    /// <para>Normal: normal of intersection point</para>
    /// <para>IntersectedObject: scene object that the ray hit</para>
    /// </summary>
    public class Intersection
    {
        public bool DidIntersect { get; }
        public double Position { get; }
        public Vector3 Normal { get; }
        public SceneObjects.SceneObject IntersectedObject { get; }


        public Intersection(bool didIntersect, double position, Vector3 normal, SceneObjects.SceneObject intersectedObject)
        {
            DidIntersect = didIntersect;
            Position = position;
            Normal = normal;
            IntersectedObject = intersectedObject;
        }

        public Intersection()
        {
            DidIntersect = false;
            Position = 0;
            Normal = new Vector3();
            IntersectedObject = null;
        }
    }
}
