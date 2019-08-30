using System;
using System.Drawing;

namespace Raytracer
{
    // Provide some simple methods for manipulating colors
    public static class ColorManipulator
    {
        // Multiple each channel of color by d
        public static Color Multiply(Color color, double d)
        {
            int r = (int)(color.R * d);
            int g = (int)(color.G * d);
            int b = (int)(color.B * d);
            return Clip(r, g, b);
        }


        // Sum two colors
        public static Color Add(Color c1, Color c2)
        {
            int r = c1.R + c2.R;
            int g = c1.G + c2.G;
            int b = c1.B + c2.B;
            return Clip(r, g, b);
        }


        // Clip to ensure values are no more than 255
        public static Color Clip(int r, int g, int b)
        {
            r = Math.Min(255, r);
            g = Math.Min(255, g);
            b = Math.Min(255, b);

            return Color.FromArgb(r, g, b);
        }

    }
}
