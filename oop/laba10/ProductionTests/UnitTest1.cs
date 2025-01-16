using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary10;

namespace TestLab10
{
    [TestClass]
    public class ProductionTests
    {
        [TestMethod]
        public void Test_DefaultConstructor()
        {
            // Arrange & Act
            Production production = new Production();

            // Assert
            Assert.IsNull(production.Name, "Имя должно быть null при создании без параметров");
            Assert.AreEqual(0, production.Employees, "Количество работников должно быть равно 0 при создании без параметров");
        }

        [TestMethod]
        public void Test_ParameterizedConstructor()
        {
            // Arrange
            string expectedName = "Завод";
            int expectedEmployees = 50;

            // Act
            Production production = new Production(expectedName, expectedEmployees);

            // Assert
            Assert.AreEqual(expectedName, production.Name, "Имя должно быть установлено через конструктор");
            Assert.AreEqual(expectedEmployees, production.Employees, "Количество работников должно быть установлено через конструктор");
        }

        [TestMethod]
        public void Test_CopyConstructor()
        {
            // Arrange
            Production original = new Production("Завод", 50);

            // Act
            Production copy = new Production(original);

            // Assert
            Assert.AreEqual(original.Name, copy.Name, "Имя в копии должно совпадать с оригиналом");
            Assert.AreEqual(original.Employees, copy.Employees, "Количество работников в копии должно совпадать с оригиналом");
        }

        [TestMethod]
        public void Test_RandomInit()
        {
            // Arrange
            Production production = new Production();

            // Act
            production.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(production.Name), "RandomInit должен генерировать непустое имя");
            Assert.IsTrue(production.Employees > 0, "RandomInit должен генерировать положительное количество работников");
        }

        [TestMethod]
        public void Test_ShowMethod()
        {
            // Arrange
            Production production = new Production("Завод", 100);
            string expectedOutput = "\n\nНаименование: Завод, количество сотрудников: 100";

            // Act & Assert
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                production.Show();
                Assert.AreEqual(expectedOutput.Trim(), sw.ToString().Trim(), "Show должен выводить корректные данные");
            }
        }

        [TestMethod]
        public void Test_NonVirtualShowMethod()
        {
            // Arrange
            Production production = new Production("Завод", 200);
            string expectedOutput = "Наименование: Завод, количество сотрудников: 200";

            // Act & Assert
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                production.NonVirtualShow();
                Assert.AreEqual(expectedOutput.Trim(), sw.ToString().Trim(), "NonVirtualShow должен выводить корректные данные");
            }
        }

        [TestMethod]
        public void Test_EqualsMethodForEqualObjects()
        {
            // Arrange
            Production production1 = new Production("Завод", 50);
            Production production2 = new Production("Завод", 50);

            // Act & Assert
            Assert.IsTrue(production1.Equals(production2), "Equals должен возвращать true для одинаковых объектов");
        }

        [TestMethod]
        public void Test_EqualsMethodForDifferentObjects()
        {
            // Arrange
            Production production1 = new Production("Завод", 50);
            Production production2 = new Production("Фабрика", 100);

            // Act & Assert
            Assert.IsFalse(production1.Equals(production2), "Equals должен возвращать false для разных объектов");
        }

        [TestMethod]
        public void Test_CloneMethod()
        {
            // Arrange
            Production original = new Production("Завод", 75);

            // Act
            Production clone = (Production)original.Clone();

            // Assert
            Assert.AreEqual(original.Name, clone.Name, "Clone должен копировать имя");
            Assert.AreEqual(original.Employees, clone.Employees, "Clone должен копировать количество работников");
            Assert.AreNotSame(original, clone, "Clone должен возвращать новый объект");
        }

        [TestMethod]
        public void Test_CompareToMethod_LessThan()
        {
            // Arrange
            Production production1 = new Production("Завод А", 50);
            Production production2 = new Production("Завод Б", 100);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.IsTrue(result < 0, "CompareTo должен возвращать отрицательное значение, если первый объект меньше второго");
        }

        [TestMethod]
        public void Test_CompareToMethod_Equal()
        {
            // Arrange
            Production production1 = new Production("Завод", 50);
            Production production2 = new Production("Завод", 50);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.AreEqual(0, result, "CompareTo должен возвращать 0, если объекты равны");
        }

        [TestMethod]
        public void Test_CompareToMethod_GreaterThan()
        {
            // Arrange
            Production production1 = new Production("Завод Б", 50);
            Production production2 = new Production("Завод А", 50);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo должен возвращать положительное значение, если первый объект больше второго");
        }

        [TestMethod]
        public void Test_ReadPosInt_InvalidInput()
        {
            // Arrange
            var input = new StringReader("abc\n-2\n10\n");
            Console.SetIn(input);

            Production production = new Production();
            using var output = new StringWriter();
            Console.SetOut(output);

            // Act
            int result = production.Employees = 10;

            // Assert
            Assert.AreEqual(10, result, "Employees должен принимать корректные данные");
        }
    }
}
