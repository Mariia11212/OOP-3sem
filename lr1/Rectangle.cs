using System.Collections.Generic;

namespace lr1
{
    public class Rectangle : Polygon
    {
        public Point TopLeft { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        public Rectangle(Point topLeft, double width, double height)
            : base(GetRectangleVertices(topLeft, width, height))
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Width and height must be positive.");
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        private static IEnumerable<Point> GetRectangleVertices(Point topLeft, double width, double height)
        {
            return new List<Point>
            {
                topLeft,
                new Point(topLeft.X + width, topLeft.Y),
                new Point(topLeft.X + width, topLeft.Y + height),
                new Point(topLeft.X, topLeft.Y + height)
            };
        }

        public override double GetArea() => Width * Height;
        public override double GetPerimeter() => 2 * (Width + Height);

        public override void ApplyTransformation(ITransformation transformation)
        {
            base.ApplyTransformation(transformation);
            TopLeft = transformation.Transform(TopLeft);
        }

        public override string ToString()
        {
            return $"Rectangle with TopLeft {TopLeft}, Width {Width:F2}, Height {Height:F2}";
        }
    }
}