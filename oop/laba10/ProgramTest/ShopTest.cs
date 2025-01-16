using ClassLibrary10;


namespace ShopTests
{
    [TestClass]
    public class ShopUnitTest
    {
        [TestMethod]
        public void Shop_DefaultConstructor_ShouldInitializeDefaults()
        {
            // Arrange & Act
            Shop shop = new Shop();

            // Assert
            Assert.AreEqual("Без названия", shop.ShopName, "ShopName должен быть 'Без названия'");
            Assert.AreEqual("Без типа", shop.Type, "Type должен быть 'Без типа'");
        }

        [TestMethod]
        public void Shop_ParameterizedConstructor_ShouldSetValues()
        {
            // Arrange
            string expectedName = "Цех 1";
            int expectedEmployees = 100;
            string expectedShopName = "Основной цех";
            string expectedType = "основной";

            // Act
            Shop shop = new Shop(expectedName, expectedEmployees, "Фабрика 1", 200.5, expectedShopName, expectedType);

            // Assert
            Assert.AreEqual(expectedName, shop.Name, "Name должен быть установлен через конструктор");
            Assert.AreEqual(expectedEmployees, shop.Employees, "Employees должен быть установлен через конструктор");
            Assert.AreEqual(expectedShopName, shop.ShopName, "ShopName должен быть установлен через конструктор");
            Assert.AreEqual(expectedType, shop.Type, "Type должен быть установлен через конструктор");
        }

        [TestMethod]
        public void Shop_RandomInit_ShouldGenerateValidData()
        {
            // Arrange
            Shop shop = new Shop();

            // Act
            shop.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(shop.ShopName), "RandomInit должен генерировать непустое ShopName");
            Assert.IsTrue(shop.Employees > 0, "RandomInit должен генерировать положительное количество сотрудников");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(shop.Type), "RandomInit должен генерировать непустой Type");
        }

        [TestMethod]
        public void Shop_Equals_ShouldReturnTrueForEqualObjects()
        {
            // Arrange
            Shop shop1 = new Shop("Цех", 50, "Фабрика 1", 150.5, "Цех 1", "основной");
            Shop shop2 = new Shop("Цех", 50, "Фабрика 1", 150.5, "Цех 1", "основной");

            // Act & Assert
            Assert.IsTrue(shop1.Equals(shop2), "Equals должен возвращать true для одинаковых объектов");
        }


        

        [TestMethod]
        public void Shop_Equals_ShouldReturnFalseForNull()
        {
            // Arrange
            Shop shop = new Shop("Цех", 50, "Фабрика", 150.5, "Цех 1", "основной");

            // Act & Assert
            Assert.IsFalse(shop.Equals(null), "Equals должен возвращать false для null");
        }

        [TestClass]
        public class ShopTests
        {
            [TestMethod]
            public void Shop_Type_ShouldNotBeEmpty()
            {
                // Arrange
                Shop shop = new Shop();
                var output = new StringWriter();
                Console.SetOut(output);

                // Act
                shop.Type = "";

                // Assert
                Assert.AreEqual("Без типа", shop.Type, "Type должен оставаться 'Без типа', если ввод пуст.");
                StringAssert.Contains(output.ToString(), "Ошибка: Тип цеха не может быть пустым", "Сообщение об ошибке должно быть выведено.");
            }
        }

        [TestMethod]
        public void Shop_CopyConstructor_ShouldCopyValuesCorrectly()
        {
            // Arrange
            Shop original = new Shop("Завод", 150, "Фабрика 3", 200.5, "Основной цех", "основной");

            // Act
            Shop copy = new Shop(original);

            // Assert
            Assert.AreEqual(original.Name, copy.Name, "Name должен быть скопирован.");
            Assert.AreEqual(original.ShopName, copy.ShopName, "ShopName должен быть скопирован.");
            Assert.AreEqual(original.Type, copy.Type, "Type должен быть скопирован.");
        }

        [TestMethod]
        public void Shop_Init_ShouldSetValidValues()
        {
            // Arrange
            Shop shop = new Shop();
            var input = new StringReader("Цех 1\n200\nвспомогательный\n");
            Console.SetIn(input);

            // Act
            shop.Init();

            // Assert
            Assert.AreEqual("Цех 1", shop.ShopName, "ShopName должен быть установлен через Init.");
            Assert.AreEqual(200, shop.Employees, "Employees должен быть установлен через Init.");
            Assert.AreEqual("вспомогательный", shop.Type, "Type должен быть установлен через Init.");
        }

        [TestMethod]
        public void Shop_Show_ShouldDisplayCorrectData()
        {
            // Arrange
            Shop shop = new Shop("Цех 1", 150, "Фабрика 3", 300.5, "Основной цех", "основной");
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            shop.Show();

            // Assert
            string expectedOutput = "Наименование: Цех 1, количество сотрудников: 150\nНазвание цеха: Основной цех\nТип цеха: основной";
            Assert.AreEqual(expectedOutput.Trim(), output.ToString().Trim(), "Show должен выводить корректные данные.");
        }



    }
}
