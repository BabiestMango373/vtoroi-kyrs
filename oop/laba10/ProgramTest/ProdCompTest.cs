using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary10;
using System.Collections.Generic;

namespace ProductionComparerTests
{
    [TestClass]
    public class ProductionComparerUnitTests
    {
        [TestMethod]
        public void ProductionComparer_Compare_ShouldReturnNegativeForSmallerEmployees()
        {
            // Arrange
            Production p1 = new Production("Завод А", 50);
            Production p2 = new Production("Завод Б", 100);
            ProductionComparer comparer = new ProductionComparer();

            // Act
            int result = comparer.Compare(p1, p2);

            // Assert
            Assert.IsTrue(result < 0, "Compare должен возвращать отрицательное значение, если первый объект меньше второго по количеству работников");
        }

        [TestMethod]
        public void ProductionComparer_Compare_ShouldReturnZeroForEqualEmployees()
        {
            // Arrange
            Production p1 = new Production("Завод А", 100);
            Production p2 = new Production("Завод Б", 100);
            ProductionComparer comparer = new ProductionComparer();

            // Act
            int result = comparer.Compare(p1, p2);

            // Assert
            Assert.AreEqual(0, result, "Compare должен возвращать 0, если объекты равны по количеству работников");
        }

        [TestMethod]
        public void ProductionComparer_Compare_ShouldReturnPositiveForGreaterEmployees()
        {
            // Arrange
            Production p1 = new Production("Завод А", 200);
            Production p2 = new Production("Завод Б", 100);
            ProductionComparer comparer = new ProductionComparer();

            // Act
            int result = comparer.Compare(p1, p2);

            // Assert
            Assert.IsTrue(result > 0, "Compare должен возвращать положительное значение, если первый объект больше второго по количеству работников");
        }

        [TestMethod]
        public void ProductionComparer_Compare_ShouldReturnZeroForNonProductionObjects()
        {
            // Arrange
            object obj1 = new object();
            object obj2 = new object();
            ProductionComparer comparer = new ProductionComparer();

            // Act
            int result = comparer.Compare(obj1, obj2);

            // Assert
            Assert.AreEqual(0, result, "Compare должен возвращать 0, если объекты не являются экземплярами Production");
        }
    }
}