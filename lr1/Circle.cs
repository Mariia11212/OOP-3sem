namespace lr1
{
    public class Circle : Shape
    {
        public Point Center { get; private set; }
        public double Radius { get; private set; }

        public Circle(Point center, double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be positive.");
            Center = center;
            Radius = radius;
        }

        public override double GetArea() => Math.PI * Radius * Radius;

        public override double GetPerimeter() => 2 * Math.PI * Radius;

        public override Point GetCentroid() => Center;

        public override void ApplyTransformation(ITransformation transformation)
        {
            Center = transformation.Transform(Center);
            if (transformation is Scaling scaling)
            {
                Radius *= scaling.Factor;
            }
        }

        public bool ContainsPoint(Point p)
        {
            return Center.GetDistanceTo(p) <= Radius;
        }

        public override string ToString()
        {
            return $"Circle with Center {Center} and Radius {Radius:F2}";
        }
    }
}