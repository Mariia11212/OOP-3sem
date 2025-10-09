namespace lr1
{
    public class Scaling : ITransformation
    {
        public double Factor { get; private set; }
        private Point _center;

        public Scaling(double factor, Point center)
        {
            if (factor <= 0)
                throw new ArgumentException("Scaling factor must be positive.");
            Factor = factor;
            _center = center;
        }

        public Point Transform(Point p)
        {
            double translatedX = p.X - _center.X;
            double translatedY = p.Y - _center.Y;

            double scaledX = translatedX * Factor;
            double scaledY = translatedY * Factor;

            return new Point(scaledX + _center.X, scaledY + _center.Y);
        }

        public override string ToString()
        {
            return $"Scaling by Factor {Factor:F2} around {_center}";
        }
    }
}