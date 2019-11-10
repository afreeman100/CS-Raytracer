using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Raytracer.SceneObjects
{
    /// <summary>
    /// Returns a list of Tri objects that make up more complex objects like squares and cubes
    /// </summary>
    public class Composites
    {

        /// <summary>
        /// Returns 2 tris that define a square
        /// 
        ///   v3.....v1
        ///   .       .
        ///   .       .
        ///   .       .
        ///   v2......v0
        /// </summary>
        public static IEnumerable<SceneObject> Quad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Color color, double reflectivity)
        {
            var tris = new List<SceneObject>();
            tris.Add(new Tri(v0, v1, v2, color, reflectivity));
            tris.Add(new Tri(v3, v2, v1, color, reflectivity));
            return tris;
        }

        /// <summary>
        /// Returns 12 tris that define a cube
        /// 
        ///   v7.....v5     
        ///   ..       .
        ///   .   .       .
        ///   .     .       .
        ///   v6      v3.....v1
        ///     .     .       .
        ///       .   .       .
        ///         . .       .
        ///           v2......v0
        /// </summary>
        public static IEnumerable<SceneObject> Cube(
            Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3,
            Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7,
            Color color, double reflectivity)
        {
            var tris = new List<SceneObject>();
            tris.AddRange(Quad(v0, v1, v2, v3, color, reflectivity));
            tris.AddRange(Quad(v2, v3, v6, v7, color, reflectivity));
            tris.AddRange(Quad(v4, v5, v0, v1, color, reflectivity));
            tris.AddRange(Quad(v6, v7, v4, v5, color, reflectivity));
            tris.AddRange(Quad(v1, v5, v3, v7, color, reflectivity));
            tris.AddRange(Quad(v4, v0, v6, v2, color, reflectivity));
            return tris;
        }
    }
}
