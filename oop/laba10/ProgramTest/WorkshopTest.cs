using ClassLibrary10;

namespace WorkshopTests
{
    [TestClass]
    public class WorkshopUnitTests
    {
        [TestMethod]
        public void Workshop_DefaultConstructor_ShouldInitializeDefaults()
        {
            // Arrange & Act
            Workshop workshop = new Workshop();

            // Assert
            Assert.AreEqual(10, workshop.Area, "Площадь должна быть 10 по умолчанию");
            Assert.IsTrue(string.IsNullOrEmpty(workshop.WorkshopName), "WorkshopName должен быть пустым по умолчанию");
        }

        [TestMethod]
        public void Workshop_ParameterizedConstructor_ShouldSetValues()
        {
            // Arrange
            string expectedName = "Мастерская 1";
            int expectedEmployees = 100;
            string expectedWorkshopName = "Основная мастерская";
            int expectedArea = 500;

            // Act
            Workshop workshop = new Workshop(expectedName, expectedEmployees, "Фабрика 1", 200.5, "Цех 1", "основной", expectedWorkshopName, expectedArea);

            // Assert
            Assert.AreEqual(expectedName, workshop.Name, "Name должен быть установлен через конструктор");
            Assert.AreEqual(expectedEmployees, workshop.Employees, "Employees должен быть установлен через конструктор");
            Assert.AreEqual(expectedWorkshopName, workshop.WorkshopName, "WorkshopName должен быть установлен через конструктор");
            Assert.AreEqual(expectedArea, workshop.Area, "Area должен быть установлен через конструктор");
        }

        [TestMethod]
        public void Workshop_Init_ShouldSetValidValues()
        {
            // Arrange
            Workshop workshop = new Workshop();
            var input = new StringReader("Мастерская 2\n15\nНазвание мастерской\n50\n");
            Console.SetIn(input);

            // Act
            workshop.Init();

            // Assert
            Assert.AreEqual("Мастерская 2", workshop.Name, "Name должен быть установлен через Init");
            Assert.AreEqual(15, workshop.Employees, "Employees должен быть установлен через Init");
            Assert.AreEqual("Название мастерской", workshop.WorkshopName, "WorkshopName должен быть установлен через Init");
            Assert.AreEqual(50, workshop.Area, "Area должен быть установлен через Init");
        }

        [TestMethod]
        public void Workshop_RandomInit_ShouldGenerateValidData()
        {
            // Arrange
            Workshop workshop = new Workshop();

            // Act
            workshop.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(workshop.WorkshopName), "RandomInit должен генерировать непустое WorkshopName");
            Assert.IsTrue(workshop.Area >= 10 && workshop.Area <= 200, "RandomInit должен генерировать площадь в пределах 10-200");
        }

        [TestMethod]
        public void Workshop_Equals_ShouldReturnTrueForEqualObjects()
        {
            // Arrange
            Workshop workshop1 = new Workshop("Мастерская", 100, "Фабрика", 300.5, "Цех", "основной", "Основная мастерская", 500);
            Workshop workshop2 = new Workshop("Мастерская", 100, "Фабрика", 300.5, "Цех", "основной", "Основная мастерская", 500);

            // Act & Assert
            Assert.IsTrue(workshop1.Equals(workshop2), "Equals должен возвращать true для одинаковых объектов");
        }

        [TestMethod]
        public void Workshop_Equals_ShouldReturnFalseForDifferentObjects()
        {
            // Arrange
            Workshop workshop1 = new Workshop("Мастерская", 100, "Фабрика", 300.5, "Цех", "основной", "Основная мастерская", 500);
            Workshop workshop2 = new Workshop("Мастерская 2", 150, "Фабрика 2", 400.5, "Цех 2", "вспомогательный", "Вторая мастерская", 600);

            // Act & Assert
            Assert.IsFalse(workshop1.Equals(workshop2), "Equals должен возвращать false для разных объектов");
        }


        

        [TestMethod]
        public void Workshop_Equals_ShouldReturnFalseForDifferentTypes()
        {
            // Arrange
            Workshop workshop = new Workshop("Мастерская", 100, "Фабрика", 300.5, "Цех", "основной", "Основная мастерская", 500);
            Factory factory = new Factory("Завод", 50, "Фабрика 1", 200.5);

            // Act & Assert
            Assert.IsFalse(workshop.Equals(factory), "Equals должен возвращать false для объектов разных типов");
        }

        [TestMethod]
        public void Workshop_WorkshopName_ShouldNotBeEmpty()
        {
            // Arrange
            Workshop workshop = new Workshop();
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            workshop.WorkshopName = "";

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(workshop.WorkshopName), "WorkshopName должен остаться пустым.");
            StringAssert.Contains(output.ToString(), "Ошибка: Название мастерской не может быть пустым", "Сообщение об ошибке должно быть выведено.");
        }

        [TestMethod]
        public void Workshop_CopyConstructor_ShouldCopyValuesCorrectly()
        {
            // Arrange
            Workshop original = new Workshop("Мастерская 1", 200, "Фабрика 2", 300.5, "Цех 1", "основной", "Основная мастерская", 100);

            // Act
            Workshop copy = new Workshop(original);

            // Assert
            Assert.AreEqual(original.Name, copy.Name, "Name должен быть скопирован.");
            Assert.AreEqual(original.WorkshopName, copy.WorkshopName, "WorkshopName должен быть скопирован.");
            Assert.AreEqual(original.Area, copy.Area, "Area должен быть скопирован.");
        }


        [TestMethod]
        public void Workshop_Show_ShouldDisplayCorrectData()
        {
            // Arrange
            Workshop workshop = new Workshop("Мастерская", 100, "Фабрика", 300.5, "Цех", "основной", "Основная мастерская", 500);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            workshop.Show();

            // Assert
            string expectedOutput = "Наименование: Мастерская, количество сотрудников: 100\nНазвание мастерской: Основная мастерская\nПлощадь мастерскуой: 500";
            Assert.AreEqual(expectedOutput.Trim(), output.ToString().Trim(), "Show должен выводить корректные данные.");
        }


    }
}
