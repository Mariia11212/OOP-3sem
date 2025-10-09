using System.Collections.Generic;
using lr1.DataStructures;

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

        public static IDataList<T> TransformAll<T>(IDataList<T> shapes, ITransformation transformation) where T : IShape
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes.GetAt(i).ApplyTransformation(transformation);
            }
            return shapes;
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