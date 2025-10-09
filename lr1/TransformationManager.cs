using System.Collections.Generic;

namespace lr1
{
    public static class TransformationManager
    {
        public static List<T> TransformAll<T>(List<T> shapes, ITransformation transformation) where T : IShape
        {
            List<T> transformedShapes = new List<T>();
            foreach (var shape in shapes)
            {
                shape.ApplyTransformation(transformation);
                transformedShapes.Add(shape);
            }
            return transformedShapes;
        }

        public static Point ApplyMultipleTransformations(Point p, params ITransformation[] transformations)
        {
            Point currentPoint = p;
            foreach (var transform in transformations)
            {
                currentPoint = transform.Transform(currentPoint);
            }
            return currentPoint;
        }
    }
}