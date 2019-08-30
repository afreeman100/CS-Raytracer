using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    public class Sphere : ISceneObject
    {
        private Vector3 centre;
        private double radius;
        public Color color;


        // The most basic sphere within the scene is defined by its centre and radius
        public Sphere(Vector3 centre, double radius, Color color)
        {
            this.centre = centre;
            this.radius = radius;
            this.color = color;
        }



        /*
        * Given an initial point and a normalized direction vector, see if line defined by these intersects the sphere.
        * Returns where on the line the intersection occurs, in terms of the t parameter, and the normal to the point
        * of intersection.
        */
        public Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction)
        {
            Vector3 l = position - this.centre;
            double b = 2 * Vector3.Dot(direction, l);
            double c = l.LengthSquared() - Math.Pow(this.radius, 2);

            double determinant = b * b - 4 * c;

            if (determinant >= 0)
            {
                // Possible intersection points
                double t1 = (-b + Math.Sqrt(determinant)) / 2;
                double t2 = (-b - Math.Sqrt(determinant)) / 2;

                // Find intersection point that is closes to the camera without being behind it
                double t = (t1 > 0 && t2 > 0) ? Math.Min(t1, t2) : Math.Max(t1, t2);

                // Calculate vector from centre to intersection point and normalize
                Vector3 normal = (position + (float)(t) * direction) - this.centre;
                normal = Vector3.Normalize(normal);

                return new Tuple<double, Vector3>(t, normal);
            }

            // No intersection found
            return new Tuple<double, Vector3>(-1, l);
        }


        /*
         * Determine the intensity of a specific point on the object, based on the
         * lighting and other objects in the scene.
         */
        public Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection)
        {
            Color pointColor = Color.FromArgb(0, 0, 0);

            // Determine intensity contribution from each light source
            foreach (ISceneLight light in scene.lights)
            {
                double lightIntensity = light.Intensity(scene, intersectionPoint, intersectionNormal, rayDirection);

                Color temp = ColorManipulator.Multiply(this.color, lightIntensity);
                pointColor = ColorManipulator.Add(pointColor, temp);

            }
            return pointColor;
        }
    }
}
