﻿using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer.SceneObjects
{
    public class Sphere : SceneObject
    {
        private readonly Vector3 centre;
        private readonly double radius;


        public Sphere(Vector3 centre, double radius, Color color, double reflectivity)
            : base(color, reflectivity)
        {
            this.centre = centre;
            this.radius = radius;
        }

        /// <summary>
        /// Given an initial point and a normalized direction vector, see if line defined by these intersects the sphere.
        /// Returns where on the line the intersection occurs, in terms of the t parameter, and the normal to the point
        /// of intersection.
        /// </summary>
        public override Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction)
        {
            Vector3 l = position - centre;
            double b = 2 * Vector3.Dot(direction, l);
            double c = l.LengthSquared() - Math.Pow(radius, 2);

            double determinant = b * b - 4 * c;

            if (determinant >= 0)
            {
                // Possible intersection points
                double t1 = (-b + Math.Sqrt(determinant)) / 2;
                double t2 = (-b - Math.Sqrt(determinant)) / 2;

                // Find intersection point that is closes to the camera without being behind it
                double t = (t1 > 0 && t2 > 0) ? Math.Min(t1, t2) : Math.Max(t1, t2);

                // Calculate vector from centre to intersection point and normalize
                Vector3 normal = (position + (float)(t) * direction) - centre;
                normal = Vector3.Normalize(normal);

                return new Tuple<double, Vector3>(t, normal);
            }

            // No intersection found
            return new Tuple<double, Vector3>(-1, l);
        }
    }
}
