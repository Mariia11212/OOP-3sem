using System;
using System.Collections.Generic;
using System.Linq;
using lr1.DataStructures;

namespace lr1
{
    public class Polygon : Shape
    {
        private MyDynamicArrayBasedList<Point> _vertices;
        public IDataList<Point> Vertices => _vertices;

        public Polygon(IEnumerable<Point> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException(nameof(vertices));
            if (vertices.Count() < 3)
                throw new ArgumentException("Polygon must have at least 3 vertices.");

            _vertices = new MyDynamicArrayBasedList<Point>();
            foreach (var p in vertices)
            {
                _vertices.Add(p);
            }
        }

        public Polygon(MyDynamicArrayBasedList<Point> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException(nameof(vertices));
            if (vertices.Count < 3)
                throw new ArgumentException("Polygon must have at least 3 vertices.");
            _vertices = vertices;
        }

        public override double GetArea()
        {
            double area = 0;
            for (int i = 0; i < _vertices.Count; i++)
            {
                Point p1 = _vertices.GetAt(i);
                Point p2 = _vertices.GetAt((i + 1) % _vertices.Count);
                area += (p1.X * p2.Y) - (p1.Y * p2.X);
            }
            return Math.Abs(area / 2.0);
        }

        public override double GetPerimeter()
        {
            double perimeter = 0;
            for (int i = 0; i < _vertices.Count; i++)
            {
                perimeter += _vertices.GetAt(i).GetDistanceTo(_vertices.GetAt((i + 1) % _vertices.Count));
            }
            return perimeter;
        }

        public override Point GetCentroid()
        {
            double centerX = 0;
            double centerY = 0;
            double signedArea = 0;

            for (int i = 0; i < _vertices.Count; i++)
            {
                Point p1 = _vertices.GetAt(i);
                Point p2 = _vertices.GetAt((i + 1) % _vertices.Count);
                double crossProduct = (p1.X * p2.Y) - (p2.X * p1.Y);
                signedArea += crossProduct;
                centerX += (p1.X + p2.X) * crossProduct;
                centerY += (p1.Y + p2.Y) * crossProduct;
            }

            signedArea /= 2.0;
            if (signedArea == 0) return _vertices.GetAt(0);

            return new Point(centerX / (6 * signedArea), centerY / (6 * signedArea));
        }

        public override void ApplyTransformation(ITransformation transformation)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                Point transformedPoint = transformation.Transform(_vertices.GetAt(i));
                _vertices.RemoveAt(i);
                _vertices.InsertAt(transformedPoint, i);
            }
        }

        public void AddVertex(Point p)
        {
            _vertices.Add(p);
        }

        public override string ToString()
        {
            return $"Polygon with {_vertices.Count} vertices: {_vertices.ToString()}";
        }
    }
}