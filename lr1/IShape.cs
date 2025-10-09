namespace lr1
{
    public interface IShape
    {
        double GetArea();
        double GetPerimeter();
        Point GetCentroid();
        void ApplyTransformation(ITransformation transformation);
        void DisplayInfo(); 
    }
}