namespace lr1
{
    public class Vector
    {
        public double Dx { get; private set; }
        public double Dy { get; private set; }

        public Vector(double dx, double dy)
        {
            Dx = dx;
            Dy = dy;
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(Dx * Dx + Dy * Dy);
        }

        public Vector Scale(double factor)
        {
            return new Vector(Dx * factor, Dy * factor);
        }
    }
}