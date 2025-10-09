using System.Numerics;

namespace lr1
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetDistanceTo(Point other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public Point Translate(Vector vector)
        {
            return new Point(X + vector.Dx, Y + vector.Dy);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}