using System;
using System.Collections.Generic;
using System.Linq;

namespace lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- Геометрія на площині ---");

            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);
            Point p4 = new Point(0, 4);
            Point pCenter = new Point(1, 1);
            Console.WriteLine($"\nПочаткові точки: P1={p1}, P2={p2}, P3={p3}, P4={p4}, P_Center={pCenter}");

            Segment segment = new Segment(p1, p2);
            Circle circle = new Circle(pCenter, 2.5);
            Polygon triangle = new Polygon(new List<Point> { p1, p2, p3 });
            Rectangle rectangle = new Rectangle(p1, 3, 4);

            List<IShape> shapes = new List<IShape> { segment, circle, triangle, rectangle };

            Console.WriteLine("\n--- Інформація про початкові фігури ---");
            foreach (var shape in shapes)
            {
                shape.DisplayInfo();
            }

            Console.WriteLine("\n--- Застосування перетворень ---");

            ITransformation translation = new Translation(new Vector(1, 5));
            ITransformation rotation = new Rotation(90, pCenter);
            ITransformation scaling = new Scaling(2, pCenter);

            Console.WriteLine($"\nЗастосовуємо: {translation}");
            foreach (var shape in shapes)
            {
                shape.ApplyTransformation(translation);
                shape.DisplayInfo();
            }

            Console.WriteLine($"\nЗастосовуємо: {rotation}");
            foreach (var shape in shapes)
            {
                shape.ApplyTransformation(rotation);
                shape.DisplayInfo();
            }

            Console.WriteLine($"\nЗастосовуємо: {scaling}");
            foreach (var shape in shapes)
            {
                shape.ApplyTransformation(scaling);
                shape.DisplayInfo();
            }

            Console.WriteLine("\n--- Демонстрація статичного поліморфізму через TransformationManager ---");

            Point p5 = new Point(10, 10);
            Point p6 = new Point(15, 10);
            Point p7 = new Point(15, 15);
            Point p8 = new Point(10, 15);

            List<Rectangle> rectangles = new List<Rectangle>
            {
                new Rectangle(p5, 5, 5),
                new Rectangle(new Point(20, 20), 3, 2)
            };

            Console.WriteLine("\nПрямокутники до трансформації:");
            foreach (var rect in rectangles) rect.DisplayInfo();

            ITransformation complexTranslation = new Translation(new Vector(-5, -5));
            rectangles = TransformationManager.TransformAll(rectangles, complexTranslation);

            Console.WriteLine($"\nПрямокутники після: {complexTranslation}");
            foreach (var rect in rectangles) rect.DisplayInfo();

            List<Circle> circles = new List<Circle>
            {
                new Circle(new Point(0, 0), 1),
                new Circle(new Point(5, 5), 3)
            };

            Console.WriteLine("\nКола до трансформації:");
            foreach (var circ in circles) circ.DisplayInfo();

            ITransformation complexRotation = new Rotation(45, new Point(0, 0));
            circles = TransformationManager.TransformAll(circles, complexRotation);

            Console.WriteLine($"\nКола після: {complexRotation}");
            foreach (var circ in circles) circ.DisplayInfo();

            Console.WriteLine("\n--- Додаткові методи ---");
            Point testPoint = new Point(1, 1);
            Circle testCircle = new Circle(new Point(0, 0), 2);
            Console.WriteLine($"Точка {testPoint} всередині кола {testCircle}? {testCircle.ContainsPoint(testPoint)}");

            Segment testSegment = new Segment(new Point(0, 0), new Point(10, 0));
            Console.WriteLine($"Середина відрізка {testSegment}: {testSegment.GetMidpoint()}");
        }
    }
}