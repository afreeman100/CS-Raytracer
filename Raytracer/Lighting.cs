using System;
using System.Drawing;
using System.Numerics;

namespace Raytracer
{
    /*
     * Ambient lighting - always constant intensity regardless of position in scene
     */
    public class AmbientLight : ISceneLight
    {
        private readonly double intensity;

        public AmbientLight(double intensity) { this.intensity = intensity; }

        public double Diffuse(Scene s, Vector3 v1, Vector3 v2) { return intensity; }
        public double Specular(Scene s, Vector3 v1, Vector3 v2, Vector3 v3) { return 0; }
    }


    /*
     * Directional lighting is defined by a direction vector, then acts like an infinite plane projecting
     * light in that direction. This may be occuded by objects in the scene. Intensity is dependent on the
     * angle between light vector and object normal vector.
     */
    public class DirectionalLight : ISceneLight
    {
        public readonly Vector3 direction;
        public readonly double intensity;

        public DirectionalLight(Vector3 direction, double intensity)
        {
            // Flip direction so that direction vector points `towards` the light (more useful for calculations)
            this.direction = Vector3.Normalize(direction * -1);
            this.intensity = intensity;
        }

        // Calculate light intensity based on angle between object normal and light direction
        public double Diffuse(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal)
        {
            if (Occluded(scene, intersectionPoint, intersectionNormal)) { return 0; }
            return Vector3.Dot(intersectionNormal, this.direction);
        }


        // Calculate highlight intensity besed on angle between viewer and light reflection
        public double Specular(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection)
        {
            if (Occluded(scene, intersectionPoint, intersectionNormal)) { return 0; }

            // Reflect light across object normal then dot prodict with viewer
            Vector3 lightReflection = direction - 2 * intersectionNormal * Vector3.Dot(direction, intersectionNormal);
            lightReflection = Vector3.Normalize(lightReflection);
            double i = Vector3.Dot(lightReflection, rayDirection);

            // Cannot subtract light if reflection is pointing away from viewer!
            return Math.Max(0, i);
        }


        private bool Occluded(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal)
        {
            // Facing away from light
            if (Vector3.Dot(intersectionNormal, direction) <= 0) { return true; }

            // Shadows
            foreach (ISceneObject shadow_obj in scene.objects)
            {
                Tuple<double, Vector3> intersection = shadow_obj.Intersect(intersectionPoint, direction);
                if (intersection.Item1 > 0.0001) { return true; }
            }
            return false;
        }
    }
}