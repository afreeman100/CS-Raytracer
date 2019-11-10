using System;
using System.Drawing;
using System.Numerics;


namespace Raytracer.SceneObjects
{
    public abstract class SceneObject
    {
        private readonly Color color;
        private readonly double reflectivity;
        private readonly double specularReflectivity;
        private readonly double specularFalloff;


        protected SceneObject(Color color, double reflectivity)
        {
            this.color = color;
            this.reflectivity = reflectivity;
            this.specularReflectivity = 0.3;
            this.specularFalloff = 10;
        }


        /// <summary>
        /// Each object will have its own formula for determining collisions according to its particular defining properties
        /// </summary>
        public abstract Tuple<double, Vector3> Intersect(Vector3 position, Vector3 direction);


        /*
         * Most scene objects us the same procedure for calculating color at a
         * particular point, however sometimes this must be overridden to first
         * adjust the object normal
         */
        public virtual Color PointColor(Scene scene, Vector3 intersectionPoint, Vector3 intersectionNormal, Vector3 rayDirection, int reflections)
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
                //Tuple<bool, double, Vector3, SceneObject> intersection = scene.ClosestIntersection(intersectionPoint, viewReflection);
                Intersection intersection = scene.ClosestIntersection(intersectionPoint, viewReflection);


                // No reflection
                if (!intersection.DidIntersect) { return pointColor; }

                // Blend object color with reflected color according to object reflectivity
                Vector3 intersectionPointNew = intersectionPoint + (float)(intersection.Position) * viewReflection;
                Color objColor = intersection.IntersectedObject.PointColor(scene, intersectionPointNew, intersection.Normal, viewReflection, reflections - 1);
                pointColor = ColorManipulator.Add(ColorManipulator.Multiply(objColor, reflectivity), ColorManipulator.Multiply(pointColor, (1 - reflectivity)));
            }

            return pointColor;
        }
    }
}
