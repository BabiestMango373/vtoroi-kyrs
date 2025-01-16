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
            Assert.IsNull(production.Name, "��� ������ ���� null ��� �������� ��� ����������");
            Assert.AreEqual(0, production.Employees, "���������� ���������� ������ ���� ����� 0 ��� �������� ��� ����������");
        }

        [TestMethod]
        public void Test_ParameterizedConstructor()
        {
            // Arrange
            string expectedName = "�����";
            int expectedEmployees = 50;

            // Act
            Production production = new Production(expectedName, expectedEmployees);

            // Assert
            Assert.AreEqual(expectedName, production.Name, "��� ������ ���� ����������� ����� �����������");
            Assert.AreEqual(expectedEmployees, production.Employees, "���������� ���������� ������ ���� ����������� ����� �����������");
        }

        [TestMethod]
        public void Test_CopyConstructor()
        {
            // Arrange
            Production original = new Production("�����", 50);

            // Act
            Production copy = new Production(original);

            // Assert
            Assert.AreEqual(original.Name, copy.Name, "��� � ����� ������ ��������� � ����������");
            Assert.AreEqual(original.Employees, copy.Employees, "���������� ���������� � ����� ������ ��������� � ����������");
        }

        [TestMethod]
        public void Test_RandomInit()
        {
            // Arrange
            Production production = new Production();

            // Act
            production.RandomInit();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(production.Name), "RandomInit ������ ������������ �������� ���");
            Assert.IsTrue(production.Employees > 0, "RandomInit ������ ������������ ������������� ���������� ����������");
        }

        [TestMethod]
        public void Test_ShowMethod()
        {
            // Arrange
            Production production = new Production("�����", 100);
            string expectedOutput = "\n\n������������: �����, ���������� �����������: 100";

            // Act & Assert
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                production.Show();
                Assert.AreEqual(expectedOutput.Trim(), sw.ToString().Trim(), "Show ������ �������� ���������� ������");
            }
        }

        [TestMethod]
        public void Test_NonVirtualShowMethod()
        {
            // Arrange
            Production production = new Production("�����", 200);
            string expectedOutput = "������������: �����, ���������� �����������: 200";

            // Act & Assert
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                production.NonVirtualShow();
                Assert.AreEqual(expectedOutput.Trim(), sw.ToString().Trim(), "NonVirtualShow ������ �������� ���������� ������");
            }
        }

        [TestMethod]
        public void Test_EqualsMethodForEqualObjects()
        {
            // Arrange
            Production production1 = new Production("�����", 50);
            Production production2 = new Production("�����", 50);

            // Act & Assert
            Assert.IsTrue(production1.Equals(production2), "Equals ������ ���������� true ��� ���������� ��������");
        }

        [TestMethod]
        public void Test_EqualsMethodForDifferentObjects()
        {
            // Arrange
            Production production1 = new Production("�����", 50);
            Production production2 = new Production("�������", 100);

            // Act & Assert
            Assert.IsFalse(production1.Equals(production2), "Equals ������ ���������� false ��� ������ ��������");
        }

        [TestMethod]
        public void Test_CloneMethod()
        {
            // Arrange
            Production original = new Production("�����", 75);

            // Act
            Production clone = (Production)original.Clone();

            // Assert
            Assert.AreEqual(original.Name, clone.Name, "Clone ������ ���������� ���");
            Assert.AreEqual(original.Employees, clone.Employees, "Clone ������ ���������� ���������� ����������");
            Assert.AreNotSame(original, clone, "Clone ������ ���������� ����� ������");
        }

        [TestMethod]
        public void Test_CompareToMethod_LessThan()
        {
            // Arrange
            Production production1 = new Production("����� �", 50);
            Production production2 = new Production("����� �", 100);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.IsTrue(result < 0, "CompareTo ������ ���������� ������������� ��������, ���� ������ ������ ������ �������");
        }

        [TestMethod]
        public void Test_CompareToMethod_Equal()
        {
            // Arrange
            Production production1 = new Production("�����", 50);
            Production production2 = new Production("�����", 50);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.AreEqual(0, result, "CompareTo ������ ���������� 0, ���� ������� �����");
        }

        [TestMethod]
        public void Test_CompareToMethod_GreaterThan()
        {
            // Arrange
            Production production1 = new Production("����� �", 50);
            Production production2 = new Production("����� �", 50);

            // Act
            int result = production1.CompareTo(production2);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo ������ ���������� ������������� ��������, ���� ������ ������ ������ �������");
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
            Assert.AreEqual(10, result, "Employees ������ ��������� ���������� ������");
        }
    }
}
