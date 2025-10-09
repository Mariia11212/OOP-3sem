namespace lr1
{
    public class Translation : ITransformation
    {
        private Vector _translationVector;

        public Translation(Vector vector)
        {
            _translationVector = vector;
        }

        public Point Transform(Point p)
        {
            return new Point(p.X + _translationVector.Dx, p.Y + _translationVector.Dy);
        }

        public override string ToString()
        {
            return $"Translation by Vector ({_translationVector.Dx}, {_translationVector.Dy})";
        }
    }
}