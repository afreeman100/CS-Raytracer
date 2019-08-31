using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Tri : SceneObject
    {
        private readonly Vector3 v0;
        private readonly Vector3 v1;
        private readonly Vector3 v2;
        private readonly Vector3 normal;


        public Tri(Vector3 v0, Vector3 v1, Vector3 v2, Color color, double reflectivity)
            : base(color, reflectivity)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.normal = Vector3.Normalize(Vector3.Cross(v2 - v0, v0 - v1));
        }


        public override Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection, int reflections)
        {
            // Flipping the intersection normal makes shadows work properly
            return base.PointColor(scene, intersectionPoint, intersectionNormal * -1, rayDirection, reflections);
        }

        /*
         * https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
         */
        public override Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction)
        {
            if (Vector3.Dot(normal, direction) <= 0)
            {
                return new Tuple<double, Vector3>(-1, normal);
            }

            const double EPSILON = 0.000001;

            Vector3 edge1, edge2, h, s, q;
            float a, f, u, v;
            edge1 = v1 - v0;
            edge2 = v2 - v0;
            h = Vector3.Cross(direction, edge2);
            a = Vector3.Dot(edge1, h);

            if (a > -EPSILON && a < EPSILON)
            {
                return new Tuple<double, Vector3>(-1, normal);
            }

            f = (float)(1.0 / a);
            s = position - v0;
            u = f * Vector3.Dot(s, h);

            if (u < 0.0 || u > 1.0)
            {
                return new Tuple<double, Vector3>(-1, normal);
            }

            q = Vector3.Cross(s, edge1);
            v = f * Vector3.Dot(direction, q);
            if (v < 0.0 || u + v > 1.0)
            {
                return new Tuple<double, Vector3>(-1, normal);
            }

            // At this stage we can compute t to find out where the intersection point is on the line.
            float t = f * Vector3.Dot(edge2, q);
            if (t > EPSILON) // ray intersection
            {
                return new Tuple<double, Vector3>(t, normal);
            }
            else // This means that there is a line intersection but not a ray intersection.
            {
                return new Tuple<double, Vector3>(-1, normal);
            }
        }
    }
}
