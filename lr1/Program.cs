using System;
using System.Collections.Generic;
using System.Linq;
using lr1.DataStructures;

namespace lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- Лабораторна робота №1 ---");

            Console.WriteLine("\n=== Розділ 1: Геометрія на площині ===");

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

            IDataList<IShape> shapes = new MyDynamicArrayBasedList<IShape>
            {
                segment,
                circle,
                triangle,
                rectangle
            };

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
            TransformationManager.TransformAll(shapes, translation);
            foreach (var shape in shapes)
            {
                shape.DisplayInfo();
            }

            Console.WriteLine($"\nЗастосовуємо: {rotation}");
            TransformationManager.TransformAll(shapes, rotation);
            foreach (var shape in shapes)
            {
                shape.DisplayInfo();
            }

            Console.WriteLine($"\nЗастосовуємо: {scaling}");
            TransformationManager.TransformAll(shapes, scaling);
            foreach (var shape in shapes)
            {
                shape.DisplayInfo();
            }

            Console.WriteLine("\n--- Демонстрація статичного поліморфізму через TransformationManager ---");

            Point p5 = new Point(10, 10);

            IDataList<Rectangle> rectangles = new MyDynamicArrayBasedList<Rectangle>
            {
                new Rectangle(p5, 5, 5),
                new Rectangle(new Point(20, 20), 3, 2)
            };

            Console.WriteLine("\nПрямокутники до трансформації:");
            foreach (var rect in rectangles) rect.DisplayInfo();

            ITransformation complexTranslation = new Translation(new Vector(-5, -5));
            TransformationManager.TransformAll(rectangles, complexTranslation);

            Console.WriteLine($"\nПрямокутники після: {complexTranslation}");
            foreach (var rect in rectangles) rect.DisplayInfo();

            IDataList<Circle> circles = new MyDynamicArrayBasedList<Circle>
            {
                new Circle(new Point(0, 0), 1),
                new Circle(new Point(5, 5), 3)
            };

            Console.WriteLine("\nКола до трансформації:");
            foreach (var circ in circles) circ.DisplayInfo();

            ITransformation complexRotation = new Rotation(45, new Point(0, 0));
            TransformationManager.TransformAll(circles, complexRotation);

            Console.WriteLine($"\nКола після: {complexRotation}");
            foreach (var circ in circles) circ.DisplayInfo();

            Console.WriteLine("\n--- Додаткові методи (Геометрія) ---");
            Point testPoint = new Point(1, 1);
            Circle testCircle = new Circle(new Point(0, 0), 2);
            Console.WriteLine($"Точка {testPoint} всередині кола {testCircle}? {testCircle.ContainsPoint(testPoint)}");

            Segment testSegment = new Segment(new Point(0, 0), new Point(10, 0));
            Console.WriteLine($"Середина відрізка {testSegment}: {testSegment.GetMidpoint()}");


            Console.WriteLine("\n\n=== Розділ 2: Структури Даних ===");

            Console.WriteLine("\n--- MyLinkedList<int> ---");
            IDataList<int> linkedList = new MyLinkedList<int>();
            linkedList.Add(10);
            linkedList.Add(20);
            linkedList.InsertAt(15, 1);
            linkedList.Add(30);
            Console.WriteLine($"Список: {linkedList} (Count: {linkedList.Count})");

            Console.WriteLine($"Елемент за індексом 2: {linkedList.GetAt(2)}");
            Console.WriteLine($"Індекс елемента 20: {linkedList.IndexOf(20)}");
            Console.WriteLine($"Чи містить 10? {linkedList.Contains(10)}");
            Console.WriteLine($"Чи містить 100? {linkedList.Contains(100)}");

            // Виправлення для int? FindFirst
            int? firstEvenNullable = linkedList.FindFirst(x => x % 2 == 0);
            int firstEven = firstEvenNullable ?? 0; // Якщо не знайдено, припускаємо 0 (або інше default значення)
            Console.WriteLine($"Перше парне число: {(firstEvenNullable.HasValue ? firstEven.ToString() : "Не знайдено")}");

            linkedList.RemoveAt(0);
            Console.WriteLine($"Список після видалення за індексом 0: {linkedList} (Count: {linkedList.Count})");

            linkedList.Clear();
            Console.WriteLine($"Список після очищення: {linkedList} (Count: {linkedList.Count})");


            Console.WriteLine("\n--- MyDynamicArrayBasedList<string> ---");
            IDataList<string> arrayList = new MyDynamicArrayBasedList<string>();
            arrayList.Add("Apple");
            arrayList.Add("Banana");
            arrayList.InsertAt("Cherry", 1);
            arrayList.Add("Date");
            Console.WriteLine($"Список: {arrayList} (Count: {arrayList.Count})");

            Console.WriteLine($"Елемент за індексом 0: {arrayList.GetAt(0)}");
            Console.WriteLine($"Індекс елемента 'Banana': {arrayList.IndexOf("Banana")}");
            Console.WriteLine($"Чи містить 'Cherry'? {arrayList.Contains("Cherry")}");

            string? startsWithB = arrayList.FindFirst(s => s.StartsWith("B"));
            Console.WriteLine($"Перший елемент, що починається на 'B': {(startsWithB != null ? startsWithB : "Не знайдено")}");

            arrayList.RemoveAt(1);
            Console.WriteLine($"Список після видалення за індексом 1: {arrayList} (Count: {arrayList.Count})");


            Console.WriteLine("\n--- MyCircularList<char> ---");
            IDataList<char> circularList = new MyCircularList<char>();
            circularList.Add('A');
            circularList.Add('B');
            circularList.InsertAt('C', 1);
            circularList.Add('D');
            Console.WriteLine($"Список: {circularList} (Count: {circularList.Count})");

            Console.WriteLine($"Елемент за індексом 3: {circularList.GetAt(3)}");
            Console.WriteLine($"Індекс елемента 'C': {circularList.IndexOf('C')}");
            circularList.RemoveAt(0);
            Console.WriteLine($"Список після видалення за індексом 0: {circularList} (Count: {circularList.Count})");
        }
    }
}