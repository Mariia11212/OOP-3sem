# Лабораторна робота №1: Об'єктно-орієнтоване програмування в C#

**Тема:** Геометрія на площині та її перетворення  

Ця робота демонструє застосування принципів ООП для реалізації системи геометричних об'єктів та їхніх перетворень на площині.

---

## Зміст проекту

Проект `lr1` містить наступні файли (усі використовують `namespace lr1`):

### Базові об'єкти
- `Point.cs`
- `Vector.cs`

### Інтерфейси
- `IShape.cs`
- `ITransformation.cs`

### Абстрактна фігура
- `Shape.cs`

### Конкретні фігури
- `Segment.cs`
- `Circle.cs`
- `Polygon.cs`
- `Rectangle.cs`

### Перетворення
- `Translation.cs`
- `Rotation.cs`
- `Scaling.cs`

### Управління
- `TransformationManager.cs` (для статичного поліморфізму)

### Демонстрація
- `Program.cs`

---

## Виконання вимог ООП

| Вимога | Статус | Деталі |
|--------|--------|--------|
| 1. Не менше 9 класів/типів | ✅ ВИКОНАНО | Реалізовано 14 класів/інтерфейсів (`Point`, `Vector`, `IShape`, `Shape`, `Segment`, `Circle`, `Polygon`, `Rectangle`, `ITransformation`, `Translation`, `Rotation`, `Scaling`, `TransformationManager`, `Program`) |
| 2. Не менше 15 полів | ✅ ВИКОНАНО | Сумарно 17 полів (наприклад, `Point.X/Y`, `Vector.Dx/Dy`, `Circle.Center/Radius`, `Polygon._vertices`, `Rectangle.TopLeft/Width/Height`, `Translation._translationVector`, `Rotation._angleInRadians/_pivot`, `Scaling.Factor/_center`, `Segment.StartPoint/EndPoint`) |
| 3. Не менше 25 нетривіальних методів | ✅ ВИКОНАНО | Реалізовано 41 метод (наприклад, `Point.GetDistanceTo()`, `Polygon.GetArea()`, `Rotation.Transform()`, `TransformationManager.TransformAll()`, `Shape.DisplayInfo()`) |
| 4. Не менше 2 ієрархій успадкування (хоча б одна ≥ 3 класів) | ✅ ВИКОНАНО | 1. Фігури: `IShape -> Shape -> Segment, Circle, Polygon` (4 класи). `Rectangle` успадковує `Polygon`.<br>2. Перетворення: `ITransformation -> Translation, Rotation, Scaling` (3 класи) |
| 5. Не менше 3 незалежних випадків поліморфізму (статичний та динамічний) | ✅ ВИКОНАНО | 1. Динамічний (фігури): `List<IShape>` у `Program.cs`, виклики `shape.DisplayInfo()`, `shape.ApplyTransformation()`<br>2. Динамічний (перетворення): `Shape.ApplyTransformation(ITransformation)` та `transformation.Transform()`<br>3. Статичний (Generics): `TransformationManager.TransformAll<T>(List<T>, ITransformation)` |
| 6. Правильна інкапсуляція | ✅ ВИКОНАНО | Усі поля мають `private` або `private set`. Методи мають доцільні модифікатори доступу (`public`, `private`, `protected`) |
| 7. Використання абстрактних базових класів та інтерфейсів | ✅ ВИКОНАНО | Використано інтерфейси (`IShape`, `ITransformation`) та абстрактний клас (`Shape`) для спільної функціональності |

---

## Реалізовані геометричні можливості

- **Базові об'єкти:** точки, вектори, відрізки, кола, багатокутники, прямокутники.  
- **Властивості:** обчислення площі, периметра, центроїда для всіх фігур.  
- **Перетворення:** переміщення, обертання, масштабування (можуть застосовуватись до будь-якої фігури).  
- **Додатково:** обчислення відстані між точками, перевірка входження точки в коло, знаходження середини відрізка.

---

## Демонстрація

Файл `Program.cs` містить приклади:

1. Створення та виведення початкових властивостей різних фігур.  
2. Застосування перетворень до фігур та відображення змінених властивостей.  
3. Приклади **статичного поліморфізму** через `TransformationManager`.  
4. Використання додаткових функцій (наприклад, обчислення відстані, перевірка точки в колі).

---
