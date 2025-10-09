namespace lr1
{
    public class Segment : Shape
    {
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public Segment(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override double GetArea() => 0; // Відрізок не має площі

        public override double GetPerimeter() => StartPoint.GetDistanceTo(EndPoint);

        public override Point GetCentroid()
        {
            return new Point(
                (StartPoint.X + EndPoint.X) / 2,
                (StartPoint.Y + EndPoint.Y) / 2
            );
        }

        public override void ApplyTransformation(ITransformation transformation)
        {
            StartPoint = transformation.Transform(StartPoint);
            EndPoint = transformation.Transform(EndPoint);
        }

        public Point GetMidpoint() => GetCentroid();

        public override string ToString()
        {
            return $"Segment from {StartPoint} to {EndPoint}";
        }
    }
}