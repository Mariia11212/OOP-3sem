namespace lr1
{
    public class Rotation : ITransformation
    {
        private double _angleInRadians;
        private Point _pivot;

        public Rotation(double angleInDegrees, Point pivot)
        {
            _angleInRadians = angleInDegrees * Math.PI / 180.0;
            _pivot = pivot;
        }

        public Point Transform(Point p)
        {
            double translatedX = p.X - _pivot.X;
            double translatedY = p.Y - _pivot.Y;

            double rotatedX = translatedX * Math.Cos(_angleInRadians) - translatedY * Math.Sin(_angleInRadians);
            double rotatedY = translatedX * Math.Sin(_angleInRadians) + translatedY * Math.Cos(_angleInRadians);

            return new Point(rotatedX + _pivot.X, rotatedY + _pivot.Y);
        }

        public override string ToString()
        {
            return $"Rotation by {_angleInRadians * 180 / Math.PI:F2} degrees around {_pivot}";
        }
    }
}