using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Raytracer.SceneObjects
{
    /// <summary>
    /// Returns a list of Tri objects that make up more complex objects like squares and cubes
    /// </summary>
    public static class Composites
    {
        /// <summary>
        /// Returns 2 identical tri objects, but with opposite normals.
        /// This makes the shape opaque from both sides.
        /// </summary>
        public static IEnumerable<SceneObject> DoubleSidedTri(Vector3 v0, Vector3 v1, Vector3 v2, Color color, double reflectivity)
        {
            return new List<SceneObject>
            {
                new Tri(v0, v1, v2, color, reflectivity),
                new Tri(v0, v2, v1, color, reflectivity)
            };
        }

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
            return new List<SceneObject>
            {
                new Tri(v0, v1, v2, color, reflectivity),
                new Tri(v3, v2, v1, color, reflectivity)
            };
        }

        /// <summary>
        /// A quad with normals on both sides of the face
        /// </summary>
        public static IEnumerable<SceneObject> DoubleSidedQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Color color, double reflectivity)
        {
            var triList = new List<SceneObject>();
            triList.AddRange(DoubleSidedTri(v0, v1, v2, color, reflectivity));
            triList.AddRange(DoubleSidedTri(v3, v2, v1, color, reflectivity));
            return triList;
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
        public static IEnumerable<SceneObject> Cube(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7, Color color, double reflectivity)
        {
            var triList = new List<SceneObject>();
            triList.AddRange(Quad(v0, v1, v2, v3, color, reflectivity));
            triList.AddRange(Quad(v2, v3, v6, v7, color, reflectivity));
            triList.AddRange(Quad(v4, v5, v0, v1, color, reflectivity));
            triList.AddRange(Quad(v6, v7, v4, v5, color, reflectivity));
            triList.AddRange(Quad(v1, v5, v3, v7, color, reflectivity));
            triList.AddRange(Quad(v4, v0, v6, v2, color, reflectivity));
            return triList;
        }

        /// <summary>
        /// A cube with normals on both sides of faces
        /// </summary>
        public static IEnumerable<SceneObject> DoubleSidedCube(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7, Color color, double reflectivity)
        {
            var tris = new List<SceneObject>();
            tris.AddRange(DoubleSidedQuad(v0, v1, v2, v3, color, reflectivity));
            tris.AddRange(DoubleSidedQuad(v2, v3, v6, v7, color, reflectivity));
            tris.AddRange(DoubleSidedQuad(v4, v5, v0, v1, color, reflectivity));
            tris.AddRange(DoubleSidedQuad(v6, v7, v4, v5, color, reflectivity));
            tris.AddRange(DoubleSidedQuad(v1, v5, v3, v7, color, reflectivity));
            tris.AddRange(DoubleSidedQuad(v4, v0, v6, v2, color, reflectivity));
            return tris;
        }
    }
}
