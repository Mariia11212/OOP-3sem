using lr1;
using lr1.DataStructures;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace lr1.Tests
{
    [TestFixture]
    public class MyDynamicArrayBasedListTests
    {
        private MyDynamicArrayBasedList<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new MyDynamicArrayBasedList<int>();
        }

        [Test]
        public void NewList_IsEmptyAndHasZeroCount()
        {
            Assert.That(_list.Count, Is.EqualTo(0));
        }

        [Test]
        public void Add_IncrementsCount()
        {
            var list = new MyDynamicArrayBasedList<string>();
            list.Add("hello");
            list.Add("world");
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void Add_BeyondInitialCapacity_ResizesSuccessfully()
        {
            for (int i = 1; i <= 5; i++)
            {
                _list.Add(i * 10);
            }
            Assert.That(_list.Count, Is.EqualTo(5));
            Assert.That(_list.GetAt(4), Is.EqualTo(50));
        }

        [Test]
        public void GetAt_ValidIndex_ReturnsCorrectElement()
        {
            _list.Add(10);
            _list.Add(20);
            Assert.That(_list.GetAt(1), Is.EqualTo(20));
        }

        [TestCase(-1)]
        [TestCase(1)]
        public void GetAt_InvalidIndex_ThrowsArgumentOutOfRangeException(int index)
        {
            _list.Add(10);
            Assert.Throws<ArgumentOutOfRangeException>(() => _list.GetAt(index));
        }

        [Test]
        public void InsertAt_Middle_ShiftsElementsAndIncrementsCount()
        {
            _list.Add(10);
            _list.Add(30);
            _list.InsertAt(20, 1);
            Assert.That(_list.Count, Is.EqualTo(3));
            Assert.That(_list.GetAt(0), Is.EqualTo(10));
            Assert.That(_list.GetAt(1), Is.EqualTo(20));
            Assert.That(_list.GetAt(2), Is.EqualTo(30));
        }

        [Test]
        public void InsertAt_Start_ShiftsElements()
        {
            _list.Add(10);
            _list.Add(20);
            _list.InsertAt(5, 0);
            Assert.That(_list.Count, Is.EqualTo(3));
            Assert.That(_list.GetAt(0), Is.EqualTo(5));
            Assert.That(_list.GetAt(1), Is.EqualTo(10));
        }

        [Test]
        public void RemoveAt_Middle_RemovesElementAndDecrementsCount()
        {
            var list = new MyDynamicArrayBasedList<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.RemoveAt(1);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.GetAt(1), Is.EqualTo("C"));
        }

        [Test]
        public void RemoveAt_End_Succeeds()
        {
            _list.Add(10);
            _list.Add(20);
            _list.RemoveAt(1);
            Assert.That(_list.Count, Is.EqualTo(1));
            Assert.That(_list.GetAt(0), Is.EqualTo(10));
        }

        [Test]
        public void IndexOf_ExistingItem_ReturnsCorrectIndex()
        {
            _list.Add(10);
            _list.Add(20);
            _list.Add(10);
            Assert.That(_list.IndexOf(10), Is.EqualTo(0));
        }

        [Test]
        public void IndexOf_NonExistingItem_ReturnsMinusOne()
        {
            _list.Add(10);
            Assert.That(_list.IndexOf(99), Is.EqualTo(-1));
        }

        [Test]
        public void FindFirst_WithMultipleMatches_ReturnsFirstMatch()
        {
            _list.Add(5);
            _list.Add(15);
            _list.Add(25);
            var result = _list.FindFirst(x => x > 10);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void FindFirst_NoMatch_ReturnsDefault()
        {
            _list.Add(5);
            var result = _list.FindFirst(x => x > 100);
            Assert.That(result, Is.EqualTo(default(int)));
        }

        [Test]
        public void Clear_ResetsCountToZero()
        {
            _list.Add(1);
            _list.Add(2);
            _list.Clear();
            Assert.That(_list.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetEnumerator_AfterClear_WorksOnEmptyList()
        {
            _list.Add(1);
            _list.Clear();
            Assert.DoesNotThrow(() =>
            {
                foreach (var item in _list) { }
            });
        }

        [Test]
        public void GetEnumerator_AllowsIterationThroughAllItems()
        {
            _list.Add(10);
            _list.Add(20);
            _list.Add(30);
            var iteratedItems = new List<int>();
            foreach (var item in _list)
            {
                iteratedItems.Add(item);
            }
            CollectionAssert.AreEqual(new[] { 10, 20, 30 }, iteratedItems);
        }
    }

    [TestFixture]
    public class VectorTests
    {
        private const double Eps = 1e-3;

        [Test]
        public void Constructor_SetsProperties()
        {
            var v = new Vector(1.5, -2.5);
            Assert.That(v.Dx, Is.EqualTo(1.5));
            Assert.That(v.Dy, Is.EqualTo(-2.5));
        }

        [Test]
        public void GetMagnitude_CalculatesCorrectLength()
        {
            var v = new Vector(3, 4);
            Assert.That(v.GetMagnitude(), Is.EqualTo(5).Within(Eps));
        }

        [Test]
        public void GetMagnitude_ForZeroVector_IsZero()
        {
            var v = new Vector(0, 0);
            Assert.That(v.GetMagnitude(), Is.EqualTo(0).Within(Eps));
        }

        [Test]
        public void Scale_ReturnsNewScaledVector()
        {
            var v = new Vector(2, 3);
            var scaledVector = v.Scale(3);
            Assert.That(scaledVector.Dx, Is.EqualTo(6));
            Assert.That(scaledVector.Dy, Is.EqualTo(9));
            Assert.That(scaledVector, Is.Not.SameAs(v));
        }

        [Test]
        public void Scale_DoesNotModifyOriginalVector()
        {
            var originalVector = new Vector(5, 10);
            originalVector.Scale(2);
            Assert.That(originalVector.Dx, Is.EqualTo(5));
            Assert.That(originalVector.Dy, Is.EqualTo(10));
        }
    }

    [TestFixture]
    public class PointTests
    {
        private const double Eps = 1e-3;
        private Point _point;

        [SetUp]
        public void SetUp()
        {
            _point = new Point(10, 20);
        }

        [Test]
        public void Constructor_SetsProperties()
        {
            Assert.That(_point.X, Is.EqualTo(10));
            Assert.That(_point.Y, Is.EqualTo(20));
        }

        [Test]
        public void GetDistanceTo_Self_ReturnsZero()
        {
            Assert.That(_point.GetDistanceTo(_point), Is.EqualTo(0).Within(Eps));
        }

        [Test]
        public void GetDistanceTo_AnotherPoint_CalculatesCorrectDistance()
        {
            var p2 = new Point(13, 24);
            Assert.That(_point.GetDistanceTo(p2), Is.EqualTo(5).Within(Eps));
        }

        [Test]
        public void Translate_ReturnsNewMovedPoint()
        {
            var v = new Vector(3, -5);
            var translatedPoint = _point.Translate(v);
            Assert.That(translatedPoint.X, Is.EqualTo(13));
            Assert.That(translatedPoint.Y, Is.EqualTo(15));
            Assert.That(translatedPoint, Is.Not.SameAs(_point));
        }

        [Test]
        public void Translate_DoesNotModifyOriginalPoint()
        {
            var v = new Vector(1, 1);
            _point.Translate(v);
            Assert.That(_point.X, Is.EqualTo(10));
            Assert.That(_point.Y, Is.EqualTo(20));
        }
    }

    [TestFixture]
    public class RectangleTests
    {
        private Rectangle _rect;
        private Point _topLeft;

        [SetUp]
        public void SetUp()
        {
            _topLeft = new Point(10, 20);
            _rect = new Rectangle(_topLeft, 10, 5);
        }

        [Test]
        public void Constructor_ValidArgs_InitializesProperties()
        {
            Assert.That(_rect.TopLeft, Is.SameAs(_topLeft));
            Assert.That(_rect.Width, Is.EqualTo(10));
            Assert.That(_rect.Height, Is.EqualTo(5));
            Assert.That(_rect.Vertices.Count, Is.EqualTo(4));
        }

        [TestCase(0, 50)]
        [TestCase(10, 0)]
        [TestCase(-10, 50)]
        [TestCase(10, -50)]
        public void Constructor_InvalidDimensions_ThrowsArgumentException(double width, double height)
        {
            Assert.Throws<ArgumentException>(() => new Rectangle(new Point(0, 0), width, height));
        }

        [Test]
        public void GetArea_ReturnsCorrectArea()
        {
            Assert.That(_rect.GetArea(), Is.EqualTo(50));
        }

        [Test]
        public void GetPerimeter_ReturnsCorrectPerimeter()
        {
            Assert.That(_rect.GetPerimeter(), Is.EqualTo(30));
        }

        [Test]
        public void ApplyTransformation_WithTranslation_MovesAllVertices()
        {
            var translation = new Translation(new Vector(5, -10));
            _rect.ApplyTransformation(translation);
            Assert.That(_rect.TopLeft.X, Is.EqualTo(15));
            Assert.That(_rect.TopLeft.Y, Is.EqualTo(10));
        }

        [Test]
        public void ApplyTransformation_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _rect.ApplyTransformation(null));
        }

        [TestCase(double.NaN, 5)] //Додано перевірку обробки невалідних числових значень(NaN, Infinity) 
        [TestCase(5, double.NaN)]
        [TestCase(double.PositiveInfinity, 5)]
        [TestCase(5, double.NegativeInfinity)]
        public void ApplyTransformation_WithInvalidVector_HandlesInvalidValues(double dx, double dy)
        {
            var translation = new Translation(new Vector(dx, dy));
            _rect.ApplyTransformation(translation);
            Assert.That(double.IsNaN(_rect.TopLeft.X) || double.IsInfinity(_rect.TopLeft.X), Is.True);
            Assert.That(double.IsNaN(_rect.TopLeft.Y) || double.IsInfinity(_rect.TopLeft.Y), Is.True);
        }
    }

    [TestFixture]
    public class CircleTests
    {
        private const double Eps = 1e-3;
        private Circle _circle;
        private Point _center;

        [SetUp]
        public void SetUp()
        {
            _center = new Point(10, 10);
            _circle = new Circle(_center, 5);
        }

        [Test]
        public void Constructor_ValidArgs_InitializesProperties()
        {
            Assert.That(_circle.Center, Is.SameAs(_center));
            Assert.That(_circle.Radius, Is.EqualTo(5));
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void Constructor_InvalidRadius_ThrowsArgumentException(double radius)
        {
            Assert.Throws<ArgumentException>(() => new Circle(new Point(0, 0), radius));
        }

        [Test]
        public void GetArea_ReturnsCorrectArea()
        {
            double expectedArea = Math.PI * 25;
            Assert.That(_circle.GetArea(), Is.EqualTo(expectedArea).Within(Eps));
        }

        [Test]
        public void GetPerimeter_ReturnsCorrectPerimeter()
        {
            double expectedPerimeter = 2 * Math.PI * 5;
            Assert.That(_circle.GetPerimeter(), Is.EqualTo(expectedPerimeter).Within(Eps));
        }

        [Test]
        public void ApplyTransformation_WithTranslation_MovesCenterButNotRadius()
        {
            var translation = new Translation(new Vector(5, -5));
            _circle.ApplyTransformation(translation);
            Assert.That(_circle.Center.X, Is.EqualTo(15));
            Assert.That(_circle.Center.Y, Is.EqualTo(5));
            Assert.That(_circle.Radius, Is.EqualTo(5));
        }

        [Test]
        public void ApplyTransformation_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _circle.ApplyTransformation(null));
        }

        [TestCase(double.NaN, 5)] //Додано перевірку обробки невалідних числових значень(NaN, Infinity) 
        [TestCase(5, double.NaN)]
        [TestCase(double.PositiveInfinity, 5)]
        [TestCase(5, double.NegativeInfinity)]
        public void ApplyTransformation_WithInvalidVector_HandlesInvalidValues(double dx, double dy)
        {
            var translation = new Translation(new Vector(dx, dy));
            _circle.ApplyTransformation(translation);
            Assert.That(double.IsNaN(_circle.Center.X) || double.IsInfinity(_circle.Center.X), Is.True);
            Assert.That(double.IsNaN(_circle.Center.Y) || double.IsInfinity(_circle.Center.Y), Is.True);
        }
    }
}