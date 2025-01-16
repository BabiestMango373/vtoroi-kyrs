using ClassLibrary10;


namespace FactoryTest
{
    [TestClass]
    public class FactoryUnitTests
    {
        [TestMethod]
        public void Factory_DefaultConstructor_ShouldInitializeDefaults()
        {
            // Arrange & Act
            Factory factory = new Factory();

            // Assert
            Assert.AreEqual("Без названия", factory.FactoryName, "FactoryName должен быть инициализирован значением 'Без названия'");
            Assert.AreEqual(0, factory.Weight, "Вес должен быть инициализирован значением 0");
        }

        [TestMethod]
        public void Factory_ParameterizedConstructor_ShouldSetValues()
        {
            // Arrange
            string expectedName = "Завод";
            int expectedEmployees = 50;
            string expectedFactoryName = "Фабрика 1";
            double expectedWeight = 123.45;

            // Act
            Factory factory = new Factory(expectedName, expectedEmployees, expectedFactoryName, expectedWeight);

            // Assert
            Assert.AreEqual(expectedName, factory.Name, "Имя должно быть установлено через конструктор");
            Assert.AreEqual(expectedEmployees, factory.Employees, "Количество работников должно быть установлено через конструктор");
            Assert.AreEqual(expectedFactoryName, factory.FactoryName, "FactoryName должен быть установлен через конструктор");
            Assert.AreEqual(expectedWeight, factory.Weight, "Вес должен быть установлен через конструктор");
        }

        [TestMethod]
        public void Factory_CopyConstructor_ShouldCreateIdenticalObject()
        {
            // Arrange
            Factory original = new Factory("Завод", 50, "Фабрика 2", 200.75);

            // Act
            Factory copy = new Factory(original);

            // Assert
            Assert.AreEqual(original.Name, copy.Name, "Имя в копии должно совпадать с оригиналом");
            Assert.AreEqual(original.Employees, copy.Employees, "Количество работников в копии должно совпадать с оригиналом");
            Assert.AreEqual(original.Weight, copy.Weight, "Вес в копии должен совпадать с оригиналом");
        }

        [TestMethod]
        public void Factory_Equals_ShouldReturnTrueForEqualObjects()
        {
            // Arrange
            Factory factory1 = new Factory("Завод", 50, "Фабрика 3", 150.5);
            Factory factory2 = new Factory("Завод", 50, "Фабрика 3", 150.5);

            // Act & Assert
            Assert.IsTrue(factory1.Equals(factory2), "Equals должен возвращать true для одинаковых объектов");
        }

        [TestMethod]
        public void Factory_Equals_ShouldReturnFalseForDifferentObjects()
        {
            // Arrange
            Factory factory1 = new Factory("Завод", 50, "Фабрика 3", 150.5);
            Factory factory2 = new Factory("Фабрика", 100, "Фабрика 4", 200.0);

            // Act & Assert
            Assert.IsFalse(factory1.Equals(factory2), "Equals должен возвращать false для разных объектов");
        }

        [TestMethod]
        public void Factory_RandomInit_ShouldGenerateValidData()
        {
            // Arrange
            Factory factory = new Factory();

            // Act
            factory.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(factory.FactoryName), "RandomInit должен генерировать непустое FactoryName");
            Assert.IsTrue(factory.Weight > 0, "RandomInit должен генерировать положительный вес");
            Assert.IsTrue(factory.Employees > 0, "RandomInit должен генерировать положительное количество работников");
        }

        [TestMethod]
        public void Factory_Clone_ShouldCreateDeepCopy()
        {
            // Arrange
            Factory original = new Factory("Завод", 100, "Фабрика 7", 300.5);

            // Act
            Factory clone = (Factory)original.Clone();

            // Assert
            Assert.AreEqual(original.Name, clone.Name, "Clone должен копировать Name");
            Assert.AreEqual(original.Employees, clone.Employees, "Clone должен копировать Employees");
            Assert.AreEqual(original.FactoryName, clone.FactoryName, "Clone должен копировать FactoryName");
            Assert.AreEqual(original.Weight, clone.Weight, "Clone должен копировать Weight");
            Assert.AreNotSame(original, clone, "Clone должен создавать новый объект");
        }

        [TestMethod]
        public void Factory_ShallowCopy_ShouldCreateShallowCopy()
        {
            // Arrange
            Factory original = new Factory("Завод", 150, "Фабрика 8", 400.75);

            // Act
            Factory shallowCopy = (Factory)original.ShallowCopy();

            // Assert
            Assert.AreEqual(original.Name, shallowCopy.Name, "ShallowCopy должен копировать Name");
            Assert.AreEqual(original.Employees, shallowCopy.Employees, "ShallowCopy должен копировать Employees");
            Assert.AreEqual(original.FactoryName, shallowCopy.FactoryName, "ShallowCopy должен копировать FactoryName");
            Assert.AreEqual(original.Weight, shallowCopy.Weight, "ShallowCopy должен копировать Weight");
            Assert.AreNotSame(original, shallowCopy, "ShallowCopy должен создавать новый объект");
        }

        [TestMethod]
        public void Factory_Equals_ShouldReturnFalseForNull()
        {
            // Arrange
            Factory factory = new Factory("Завод", 50, "Фабрика 1", 150.5);

            // Act & Assert
            Assert.IsFalse(factory.Equals(null), "Equals должен возвращать false для null");
        }

        
            [TestMethod]
            public void Factory_Show_ShouldDisplayCorrectData()
            {
                // Arrange
                Factory factory = new Factory("Завод", 100, "Фабрика 1", 123.45);
                var output = new StringWriter();
                Console.SetOut(output);

                // Act
                factory.Show();

                // Assert
                string expectedOutput = "Наименование: Завод, количество сотрудников: 100\nНазвание фабрики: Фабрика 1\nВес всех деталей: 123.45 кг";
                Assert.AreEqual(expectedOutput.Trim(), output.ToString().Trim(), "Show должен выводить корректные данные.");
            }

        [TestMethod]
        public void Factory_Init_ShouldSetValidValues()
        {
            // Arrange
            Factory factory = new Factory();
            var input = new StringReader("Фабрика 2\n150\n200.75\n");
            Console.SetIn(input);

            // Act
            factory.Init();

            // Assert
            Assert.AreEqual("Фабрика 2", factory.FactoryName, "FactoryName должен быть установлен через Init.");
            Assert.AreEqual(150, factory.Employees, "Employees должен быть установлен через Init.");
            Assert.AreEqual(200.75, factory.Weight, "Weight должен быть установлен через Init.");
        }






    }
}