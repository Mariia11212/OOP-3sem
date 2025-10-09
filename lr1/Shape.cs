namespace lr1
{
    public abstract class Shape : IShape
    {
        public abstract double GetArea();
        public abstract double GetPerimeter();
        public abstract Point GetCentroid();
        public abstract void ApplyTransformation(ITransformation transformation);

        public void DisplayInfo()
        {
            Console.WriteLine($"Shape Type: {GetType().Name}");
            Console.WriteLine($"  Centroid: {GetCentroid()}");
            Console.WriteLine($"  Area: {GetArea():F2}");
            Console.WriteLine($"  Perimeter: {GetPerimeter():F2}");
        }
    }
}