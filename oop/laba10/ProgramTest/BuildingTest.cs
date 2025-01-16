using Microsoft.VisualStudio.TestTools.UnitTesting;
using laba10;
using System;
using System.IO;

namespace BuildingTests
{
    [TestClass]
    public class BuildingUnitTests
    {
        [TestMethod]
        public void Building_DefaultConstructor_ShouldInitializeDefaults()
        {
            // Arrange & Act
            Building building = new Building();

            // Assert
            Assert.AreEqual("Не указано", building.Feature[0], "Feature должен быть инициализирован значением 'Не указано'");
        }

        [TestMethod]
        public void Building_Init_ShouldSetValidValues()
        {
            // Arrange
            Building building = new Building();
            var input = new StringReader("Здание 1\n10\nподвал,лифт\n");
            Console.SetIn(input);

            // Act
            building.Init();

            // Assert
            Assert.AreEqual("Здание 1", building.Address, "Address должен быть установлен через Init");
            Assert.AreEqual(10, building.Floors, "Floors должен быть установлен через Init");
            CollectionAssert.AreEqual(new string[] { "подвал", "лифт" }, building.Feature, "Feature должен содержать введенные значения");
        }

        [TestMethod]
        public void Building_RandomInit_ShouldGenerateValidData()
        {
            // Arrange
            Building building = new Building();

            // Act
            building.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(building.Address), "RandomInit должен генерировать непустой Address");
            Assert.IsTrue(building.Floors > 0, "RandomInit должен генерировать положительное количество этажей");
            Assert.IsTrue(building.Feature.Length > 0, "RandomInit должен генерировать массив Feature с элементами");
        }

        [TestMethod]
        public void Building_Show_ShouldDisplayCorrectData()
        {
            // Arrange
            Building building = new Building
            {
                Address = "Улица Ленина",
                Floors = 5,
                Feature = new string[] { "подвал", "лифт" }
            };
            string expectedOutput = "Адрес здания: Улица Ленина, Количество этажей: 5, Особенности: подвал, лифт";

            // Act & Assert
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                building.Show();
                Assert.AreEqual(expectedOutput.Trim(), sw.ToString().Trim(), "Show должен выводить корректные данные");
            }
        }

        [TestMethod]
        public void Building_ShallowCopy_ShouldCreateShallowCopy()
        {
            // Arrange
            Building original = new Building
            {
                Address = "Улица Гагарина",
                Floors = 3,
                Feature = new string[] { "парковка", "сауна" }
            };

            // Act
            Building shallowCopy = original.ShallowCopy();

            // Assert
            Assert.AreEqual(original.Address, shallowCopy.Address, "ShallowCopy должен копировать Address");
            Assert.AreEqual(original.Floors, shallowCopy.Floors, "ShallowCopy должен копировать Floors");
            Assert.AreSame(original.Feature, shallowCopy.Feature, "ShallowCopy должен копировать ссылку на Feature");
        }

        [TestMethod]
        public void Building_Clone_ShouldCreateDeepCopy()
        {
            // Arrange
            Building original = new Building
            {
                Address = "Улица Гагарина",
                Floors = 3,
                Feature = new string[] { "парковка", "сауна" }
            };

            // Act
            Building deepCopy = (Building)original.Clone();
            original.Feature[0] = "конференц-зал";

            // Assert
            Assert.AreEqual(original.Address, deepCopy.Address, "Clone должен копировать Address");
            Assert.AreEqual(original.Floors, deepCopy.Floors, "Clone должен копировать Floors");
            Assert.AreNotSame(original.Feature, deepCopy.Feature, "Clone должен создавать независимый массив Feature");
            Assert.AreEqual("парковка", deepCopy.Feature[0], "Feature в Clone не должен изменяться при изменении оригинала");
        }
    }
}
