using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace Raytracer
{
    public class Camera
    {
        // Camera properties
        private readonly Vector3 focalPoint;
        private readonly int focalLength;
        private readonly Tuple<int, int> canvasDimensions;

        // Direction vectors
        private readonly Vector3 right = new Vector3(1, 0, 0);
        private readonly Vector3 up = new Vector3(0, 1, 0);
        private readonly Vector3 forward = new Vector3(0, 0, 1);


        // Constructor with default camera parameters
        public Camera()
        {
            focalPoint = new Vector3(0, 0, 0);
            this.focalLength = 1;
            this.canvasDimensions = new Tuple<int, int>(1, 1);
        }


        // Constructor for user-specified paraeters
        public Camera(Vector3 focalPoint, int focalLength, Tuple<int, int> canvasDimensions)
        {
            this.focalPoint = focalPoint;
            this.focalLength = focalLength;
            this.canvasDimensions = canvasDimensions;
        }


        public void Render(Tuple<int, int> resolution, List<ISceneObject> objects)
        {
            Bitmap newImage = new Bitmap(resolution.Item2, resolution.Item1);

            // 'Shoot' ray through every pixel in canvas
            for (int row = 0; row < resolution.Item1; row++)
            {
                for (int col = 0; col < resolution.Item2; col++)
                {
                    newImage.SetPixel(col, row, Color.FromArgb(255, 0, 0, 0));

                    // Map from pixel coordinates to scene 
                    float r = (float)(this.canvasDimensions.Item1 * ((row + 0.5) / resolution.Item1 - 0.5));
                    float c = (float)(this.canvasDimensions.Item2 * ((col + 0.5) / resolution.Item2 - 0.5));
                    Vector3 worldCoord = this.focalPoint + this.focalLength * this.forward + c * this.right - r * this.up;
                    Vector3 direction = Vector3.Normalize(worldCoord - this.focalPoint);

                    // TODO STORE IN LIST AND ONLY CALCULATE AFTER ALL INTERSECTIONS HAVE BEEN FOUND
                    double tSmallest = 100000000;
                    foreach (ISceneObject obj in objects)
                    {
                        Tuple<double, Vector3> intersection = obj.Intersect(this.focalPoint, direction);

                        if (intersection.Item1 > 0 && intersection.Item1 < tSmallest)
                        {
                            tSmallest = intersection.Item1;
                            newImage.SetPixel(col, row, Color.FromArgb(255, 255, 100, 100));
                            // TODO calculate colour of pixel;
                        }
                    }
                }
            }
            newImage.Save("img.png", ImageFormat.Png);
            Console.WriteLine("Done!");
        }
    }
}
