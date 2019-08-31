using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Tri : ISceneObject
    {
        private readonly Vector3 v0;
        private readonly Vector3 v1;
        private readonly Vector3 v2;
        private readonly Vector3 normal;
        private readonly Color color;
        private readonly double reflectivity;
        private readonly double specularReflectivity = 0.3;
        private readonly double specularFalloff = 10;

        private readonly bool doubleSided = true;

        public Tri(Vector3 v0, Vector3 v1, Vector3 v2, Color color, double reflectivity)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.normal = Vector3.Normalize(Vector3.Cross(v2 - v0, v0 - v1));

            this.color = color;
            this.reflectivity = reflectivity;
        }


        /*
         * https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
         */
        public Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction)
        {
            if (!doubleSided && Vector3.Dot(normal, direction) <= 0)
            {
                return new Tuple<double, Vector3>(-1, normal);
            }

            const double EPSILON = 0.0000001;

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


        public Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection, int reflections)
        {
            Color pointColor = Color.FromArgb(0, 0, 0);

            // Determine intensity contribution from each light source
            foreach (ISceneLight light in scene.lights)
            {
                double diffuse = light.Diffuse(scene, intersectionPoint, intersectionNormal);
                double specular = light.Specular(scene, intersectionPoint, intersectionNormal, rayDirection);
                specular = specularReflectivity * Math.Pow(specular, specularFalloff);

                // Diffuse lighting takes the color of the object
                Color temp = ColorManipulator.Multiply(this.color, diffuse);
                pointColor = ColorManipulator.Add(pointColor, temp);

                // Specular highlights take the color of the light (currently fixed to white)
                temp = ColorManipulator.Multiply(Color.White, specular);
                pointColor = ColorManipulator.Add(pointColor, temp);
            }


            // Color contribution from reflections
            if (reflections > 0)
            {
                // Reflect view ray across intersection normal and fire new ray in this direction
                Vector3 viewReflection = rayDirection - 2 * intersectionNormal * Vector3.Dot(rayDirection, intersectionNormal);
                Tuple<bool, double, Vector3, ISceneObject> intersection = scene.ClosestIntersection(intersectionPoint, viewReflection);

                // No reflection
                if (!intersection.Item1) { return pointColor; }

                // Blend object color with reflected color according to object reflectivity
                Vector3 intersectionPointNew = intersectionPoint + (float)(intersection.Item2) * viewReflection;
                Color objColor = intersection.Item4.PointColor(scene, intersectionPointNew, intersection.Item3, viewReflection, reflections - 1);
                pointColor = ColorManipulator.Add(ColorManipulator.Multiply(objColor, reflectivity), ColorManipulator.Multiply(pointColor, (1 - reflectivity)));
            }

            return pointColor;
        }
    }
}
