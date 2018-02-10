/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */
using System;
using System.Drawing;

namespace System.Drawing {
    internal static class RectangleUtils {

        internal static Rectangle GetRectangleWithinRectangle(RectangleF rect, Size size, ContentAlignment alignment) {
            return GetRectangleWithinRectangle(rect, size.Width, size.Height, alignment);
        }

        internal static Rectangle GetRectangleWithinRectangle(RectangleF rect, int size, ContentAlignment alignment) {
            return GetRectangleWithinRectangle(rect, size, size, alignment);
        }

        internal static Rectangle GetRectangleWithinRectangle(RectangleF rect, int width, int height, ContentAlignment alignment) {
            RectangleF newRect = RectangleF.Empty;
            switch (alignment) {
                case ContentAlignment.BottomCenter:
                    newRect = new RectangleF(rect.X + GetOffset(rect.X, rect.Right, width), rect.Bottom - height, width, height);
                    break;
                case ContentAlignment.BottomLeft:
                    newRect = new RectangleF(rect.X , rect.Bottom - height, width, height);
                    break;
                case ContentAlignment.BottomRight:
                    newRect = new RectangleF(rect.Right-width, rect.Bottom - height, width, height);
                    break;
                case ContentAlignment.MiddleCenter:
                    newRect = new RectangleF(rect.X + GetOffset(rect.X, rect.Right, width), rect.Y + GetOffset(rect.Y, rect.Bottom, height), width, height);
                    break;
                case ContentAlignment.MiddleLeft:
                    newRect = new RectangleF(rect.X, rect.Y + GetOffset(rect.Y, rect.Bottom, height), width, height);
                    break;
                case ContentAlignment.MiddleRight:
                    newRect = new RectangleF(rect.Right - width, rect.Y + GetOffset(rect.Y, rect.Bottom, height), width, height);
                    break;
                case ContentAlignment.TopCenter:
                    newRect = new RectangleF(rect.X + GetOffset(rect.X, rect.Right, width), rect.Y, width, height);
                    break;
                case ContentAlignment.TopLeft:
                    newRect = new RectangleF(rect.X, rect.Y, width, height);
                    break;
                case ContentAlignment.TopRight:
                    newRect = new RectangleF(rect.Right - width, rect.Y, width, height);
                    break;
            }
            return Rectangle.Round(newRect);
        }

        private static float GetOffset(float start, float end, int length) {
            return (end - start - length) / 2f;
        }

    }
}
