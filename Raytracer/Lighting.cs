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
        private double intensity;

        public AmbientLight(double intensity) { this.intensity = intensity; }

        public double Intensity(Scene s, Vector3 v1, Vector3 v2, Vector3 v3) { return intensity; }
    }


    /*
     * Directional lighting is defined by a direction vector, then acts like an infinite plane projecting
     * light in that direction. This may be occuded by objects in the scene. Intensity is dependent on the
     * angle between light vector and object normal vector.
     */ 
    public class DirectionalLight : ISceneLight
    {
        public Vector3 direction;
        public double intensity;

        public DirectionalLight(Vector3 direction, double intensity)
        {
            // Flip direction so that direction vector points `towards` the light (more useful for calculations)
            this.direction = Vector3.Normalize(direction * -1);
            this.intensity = intensity;
        }


        public double Intensity(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection)
        {
            // Not facing light
            if (Vector3.Dot(intersectionNormal, direction) <= 0) { return 0; }

            // Check all objects in the scene to see if they cast shadow - small offset to prevent self-occlusion
            foreach (ISceneObject shadow_obj in scene.objects)
            {
                Tuple<double, Vector3> intersection = shadow_obj.Intersect(intersectionPoint, direction);
                if (intersection.Item1 > 0.0001) { return 0; }
            }

            // Calculate light intensity based on angle between object normal and light direction
            return Vector3.Dot(intersectionNormal, direction);     
        }
    }
}
