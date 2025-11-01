using lr1;
using lr1.DataStructures;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lr1.Tests
{
    [TestFixture]
    public class MyDataTests
    {
        // МОДУЛЬ: MyDynamicArrayBasedList<T>
        [TestFixture]
        public class MyDynamicArrayBasedListTests
        {
            // Тест #1: Перевіряємо, що щойно створений список є порожнім і має Count = 0.
            [Test]
            public void NewList_ShouldBeEmptyAndHaveZeroCount()
            {
                var list = new MyDynamicArrayBasedList<int>();
                Assert.That(list.Count, Is.EqualTo(0));
            }

            // Тест #2: Перевіряємо, що метод Add коректно збільшує лічильник.
            [Test]
            public void Add_ShouldIncrementCount()
            {
                var list = new MyDynamicArrayBasedList<string>();
                list.Add("hello");
                list.Add("world");
                Assert.That(list.Count, Is.EqualTo(2));
            }

            // Тест #3: Перевіряємо, що список автоматично розширюється, коли кількість елементів перевищує початкову ємність (4).
            [Test]
            public void Add_BeyondInitialCapacity_ShouldResizeAndAddSuccessfully()
            {
                var list = new MyDynamicArrayBasedList<int>();
                // Додаємо 5 елементів, щоб перевищити початкову ємність 4
                for (int i = 1; i <= 5; i++)
                {
                    list.Add(i * 10);
                }
                Assert.That(list.Count, Is.EqualTo(5));
                Assert.That(list.GetAt(4), Is.EqualTo(50)); // Перевіряємо останній доданий елемент
            }

            // Тест #4: Перевіряємо, що GetAt повертає правильний елемент за валідним індексом.
            [Test]
            public void GetAt_ValidIndex_ShouldReturnCorrectElement()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                list.Add(20);
                Assert.That(list.GetAt(1), Is.EqualTo(20));
            }

            // Тест #5: Перевіряємо, що GetAt кидає виняток для невалідного (від'ємного) індексу.
            [Test]
            public void GetAt_NegativeIndex_ShouldThrowArgumentOutOfRangeException()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                Assert.Throws<ArgumentOutOfRangeException>(() => list.GetAt(-1));
            }

            // Тест #6: Перевіряємо, що GetAt кидає виняток для індексу, що дорівнює або перевищує Count.
            [Test]
            public void GetAt_IndexEqualToCount_ShouldThrowArgumentOutOfRangeException()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                Assert.Throws<ArgumentOutOfRangeException>(() => list.GetAt(1));
            }

            // Тест #7: Перевіряємо, що InsertAt вставляє елемент у середину списку, зміщуючи інші елементи.
            [Test]
            public void InsertAt_MiddleOfList_ShouldShiftElementsAndIncrementCount()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                list.Add(30);
                list.InsertAt(20, 1); // Вставляємо 20 в індекс 1
                Assert.That(list.Count, Is.EqualTo(3));
                Assert.That(list.GetAt(0), Is.EqualTo(10));
                Assert.That(list.GetAt(1), Is.EqualTo(20));
                Assert.That(list.GetAt(2), Is.EqualTo(30));
            }

            // Тест #8: Перевіряємо, що RemoveAt видаляє елемент і коректно зміщує решту елементів.
            [Test]
            public void RemoveAt_ShouldRemoveElementAndDecrementCount()
            {
                var list = new MyDynamicArrayBasedList<string>();
                list.Add("A");
                list.Add("B");
                list.Add("C");
                list.RemoveAt(1); // Видаляємо "B"
                Assert.That(list.Count, Is.EqualTo(2));
                Assert.That(list.GetAt(1), Is.EqualTo("C"));
            }

            // Тест #9: Перевіряємо, що IndexOf знаходить індекс першого входження елемента.
            [Test]
            public void IndexOf_ExistingItem_ShouldReturnCorrectIndex()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                list.Add(20);
                list.Add(10);
                Assert.That(list.IndexOf(10), Is.EqualTo(0));
            }

            // Тест #10: Перевіряємо, що IndexOf повертає -1, якщо елемента немає у списку.
            [Test]
            public void IndexOf_NonExistingItem_ShouldReturnMinusOne()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                Assert.That(list.IndexOf(99), Is.EqualTo(-1));
            }


            // Тест #11: Перевіряємо, що FindFirst знаходить правильний елемент за предикатом.
            [Test]
            public void FindFirst_WithMatchingPredicate_ShouldReturnItem()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(5);
                list.Add(15);
                list.Add(25);
                var result = list.FindFirst(x => x > 10);
                Assert.That(result, Is.EqualTo(15));
            }

            // Тест #12: Перевіряємо, що FindFirst повертає default, якщо жоден елемент не відповідає предикату.
            [Test]
            public void FindFirst_WithNoMatchingPredicate_ShouldReturnDefault()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(5);
                var result = list.FindFirst(x => x > 100);
                Assert.That(result, Is.EqualTo(default(int))); // default(int) is 0
            }

            // Тест #13: Перевіряємо, що метод Clear робить список порожнім (Count = 0).
            [Test]
            public void Clear_ShouldResetCountToZero()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(1);
                list.Add(2);
                list.Clear();
                Assert.That(list.Count, Is.EqualTo(0));
            }

            // Тест #14: Перевіряємо, що енумератор дозволяє коректно ітерувати по списку за допомогою foreach.
            [Test]
            public void GetEnumerator_ShouldAllowIterationThroughAllItems()
            {
                var list = new MyDynamicArrayBasedList<int>();
                list.Add(10);
                list.Add(20);
                list.Add(30);

                var iteratedItems = new List<int>();
                foreach (var item in list)
                {
                    iteratedItems.Add(item);
                }

                CollectionAssert.AreEqual(new[] { 10, 20, 30 }, iteratedItems);
            }
        }
    }
    // МОДУЛЬ: Vector

    [TestFixture]
    public class VectorTests
    {
        // Тест #1: Перевіряємо, що конструктор правильно ініціалізує властивості Dx та Dy.
        [Test]
        public void Constructor_ShouldSetDxAndDyProperties()
        {
            var v = new Vector(1.5, -2.5);
            Assert.That(v.Dx, Is.EqualTo(1.5));
            Assert.That(v.Dy, Is.EqualTo(-2.5));
        }

        // Тест #2: Перевіряємо правильність обчислення довжини (магнитуди) вектора.
        [Test]
        public void GetMagnitude_ForPythagoreanTriple_ShouldCalculateCorrectLength()
        {
            var v = new Vector(3, 4);
            // Викликаємо метод GetMagnitude()
            double magnitude = v.GetMagnitude();
            // Використовуємо Within для порівняння чисел з плаваючою комою
            Assert.That(magnitude, Is.EqualTo(5).Within(0.001));
        }

        // Тест #3: Перевіряємо, що GetMagnitude для нульового вектора повертає 0.
        [Test]
        public void GetMagnitude_ForZeroVector_ShouldBeZero()
        {
            var v = new Vector(0, 0);
            Assert.That(v.GetMagnitude(), Is.EqualTo(0));
        }

        // Тест #4: Перевіряємо, що метод Scale повертає новий вектор з правильними, масштабованими значеннями.
        [Test]
        public void Scale_ShouldReturnNewScaledVector()
        {
            var v = new Vector(2, 3);
            // Викликаємо метод Scale()
            var scaledVector = v.Scale(3);
            Assert.That(scaledVector.Dx, Is.EqualTo(6));
            Assert.That(scaledVector.Dy, Is.EqualTo(9));
        }

        // Тест #5: Перевіряємо, що метод Scale не змінює оригінальний вектор (перевірка на іммутабельність).
        [Test]
        public void Scale_ShouldNotModifyOriginalVector()
        {
            var originalVector = new Vector(5, 10);
            originalVector.Scale(2); // Викликаємо метод, але не зберігаємо результат

            // Переконуємось, що оригінальний вектор залишився незмінним
            Assert.That(originalVector.Dx, Is.EqualTo(5));
            Assert.That(originalVector.Dy, Is.EqualTo(10));
        }
        // МОДУЛЬ: Point
        [TestFixture]
        public class PointTests
        {
            // Тест #1: Перевіряємо, що конструктор Point правильно ініціалізує властивості X та Y.
            [Test]
            public void Constructor_ShouldSetXAndYProperties()
            {
                var p = new Point(10.5, -20.5);
                Assert.That(p.X, Is.EqualTo(10.5));
                Assert.That(p.Y, Is.EqualTo(-20.5));
            }

            // Тест #2: Перевіряємо, що відстань від точки до самої себе дорівнює 0.
            [Test]
            public void GetDistanceTo_Self_ShouldReturnZero()
            {
                var p = new Point(12, 34);
                Assert.That(p.GetDistanceTo(p), Is.EqualTo(0));
            }

            // Тест #3: Перевіряємо коректність обчислення відстані до іншої точки.
            [Test]
            public void GetDistanceTo_AnotherPoint_ShouldCalculateCorrectDistance()
            {
                var p1 = new Point(1, 2);
                var p2 = new Point(4, 6); // Відстань = sqrt((4-1)^2 + (6-2)^2) = sqrt(3^2 + 4^2) = sqrt(9+16) = sqrt(25) = 5
                Assert.That(p1.GetDistanceTo(p2), Is.EqualTo(5).Within(0.001));
            }

            // Тест #4: Перевіряємо, що метод Translate повертає нову точку з правильними координатами.
            [Test]
            public void Translate_ShouldReturnNewMovedPoint()
            {
                var p = new Point(10, 20);
                var v = new Vector(3, -5);
                var translatedPoint = p.Translate(v);

                Assert.That(translatedPoint.X, Is.EqualTo(13));
                Assert.That(translatedPoint.Y, Is.EqualTo(15));
            }

            // Тест #5: Перевіряємо, що метод Translate не змінює оригінальну точку.
            [Test]
            public void Translate_ShouldNotModifyOriginalPoint()
            {
                var originalPoint = new Point(10, 20);
                var v = new Vector(1, 1);
                originalPoint.Translate(v); // Викликаємо метод, але не зберігаємо результат

                // Переконуємось, що оригінальна точка залишилася незмінною
                Assert.That(originalPoint.X, Is.EqualTo(10));
                Assert.That(originalPoint.Y, Is.EqualTo(20));
            }
        }
        // МОДУЛЬ: Rectangle
        [TestFixture]
        public class RectangleTests
        {
            // Тест #1: Перевіряємо, що конструктор з валідними даними правильно ініціалізує властивості.
            [Test]
            public void Constructor_WithValidArguments_ShouldInitializePropertiesCorrectly()
            {
                var topLeft = new Point(10, 20);
                var rect = new Rectangle(topLeft, 100, 50);

                Assert.That(rect.TopLeft, Is.EqualTo(topLeft));
                Assert.That(rect.Width, Is.EqualTo(100));
                Assert.That(rect.Height, Is.EqualTo(50));
                // Перевіряємо, що базовий клас Polygon отримав 4 вершини
                Assert.That(rect.Vertices.Count, Is.EqualTo(4));
            }

            // Тест #2: Перевіряємо, що конструктор кидає виняток при нульовій ширині.
            [Test]
            public void Constructor_WithZeroWidth_ShouldThrowArgumentException()
            {
                Assert.Throws<ArgumentException>(() => new Rectangle(new Point(0, 0), 0, 50));
            }

            // Тест #3: Перевіряємо, що конструктор кидає виняток при від'ємній висоті.
            [Test]
            public void Constructor_WithNegativeHeight_ShouldThrowArgumentException()
            {
                Assert.Throws<ArgumentException>(() => new Rectangle(new Point(0, 0), 100, -50));
            }

            // Тест #4: Перевіряємо правильність обчислення площі.
            [Test]
            public void GetArea_ShouldReturnCorrectArea()
            {
                var rect = new Rectangle(new Point(0, 0), 10, 5);
                Assert.That(rect.GetArea(), Is.EqualTo(50));
            }

            // Тест #5: Перевіряємо правильність обчислення периметра.
            [Test]
            public void GetPerimeter_ShouldReturnCorrectPerimeter()
            {
                var rect = new Rectangle(new Point(0, 0), 10, 5);
                Assert.That(rect.GetPerimeter(), Is.EqualTo(30));
            }

            // Тест #6: Перевіряємо, що трансформація переміщення (Translation) коректно зміщує всі вершини.
            [Test]
            public void ApplyTransformation_WithTranslation_ShouldMoveAllVertices()
            {
                var rect = new Rectangle(new Point(10, 20), 4, 2);
                var translation = new Translation(new Vector(5, -10));

                rect.ApplyTransformation(translation);

                // Перевіряємо нову позицію опорної точки
                Assert.That(rect.TopLeft.X, Is.EqualTo(15));
                Assert.That(rect.TopLeft.Y, Is.EqualTo(10));

                // Перевіряємо, що інші вершини також змістились (наприклад, права нижня)
                var bottomRightVertex = rect.Vertices.GetAt(2);
                Assert.That(bottomRightVertex.X, Is.EqualTo(15 + 4)); // 10 + 4 + 5
                Assert.That(bottomRightVertex.Y, Is.EqualTo(10 + 2)); // 20 + 2 - 10
            }
        }
        // МОДУЛЬ: Circle
        [TestFixture]
        public class CircleTests
        {
            // Тест #1: Перевіряємо, що конструктор з валідними даними правильно ініціалізує властивості.
            [Test]
            public void Constructor_WithValidArguments_ShouldInitializeProperties()
            {
                var center = new Point(5, 5);
                var circle = new Circle(center, 10);

                Assert.That(circle.Center, Is.EqualTo(center));
                Assert.That(circle.Radius, Is.EqualTo(10));
            }

            // Тест #2: Перевіряємо, що конструктор кидає виняток при нульовому радіусі.
            [Test]
            public void Constructor_WithZeroRadius_ShouldThrowArgumentException()
            {
                Assert.Throws<ArgumentException>(() => new Circle(new Point(0, 0), 0));
            }

            // Тест #3: Перевіряємо коректність обчислення площі.
            [Test]
            public void GetArea_ShouldReturnCorrectArea()
            {
                var circle = new Circle(new Point(0, 0), 10);
                double expectedArea = Math.PI * 100;
                Assert.That(circle.GetArea(), Is.EqualTo(expectedArea).Within(0.001));
            }

            // Тест #4: Перевіряємо коректність обчислення довжини кола (периметра).
            [Test]
            public void GetPerimeter_ShouldReturnCorrectPerimeter()
            {
                var circle = new Circle(new Point(0, 0), 10);
                double expectedPerimeter = 2 * Math.PI * 10;
                Assert.That(circle.GetPerimeter(), Is.EqualTo(expectedPerimeter).Within(0.001));
            }
            // Тест #5: Перевіряємо, що Translation змінює центр, але не радіус.
            [Test]
            public void ApplyTransformation_WithTranslation_ShouldMoveCenterButNotChangeRadius()
            {
                var circle = new Circle(new Point(10, 10), 5);
                var translation = new Translation(new Vector(5, -5));

                circle.ApplyTransformation(translation);

                Assert.That(circle.Center.X, Is.EqualTo(15));
                Assert.That(circle.Center.Y, Is.EqualTo(5));
                Assert.That(circle.Radius, Is.EqualTo(5)); // Радіус не змінився
            }
        }
    }
}