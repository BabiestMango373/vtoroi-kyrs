delegate void MatrixHandler(string matrName, int number, int row, int column);

internal class Matr
{
    Random rnd = new Random();
    int[,] matrix;

    public string Name { get; set; }
    public int GetSize => matrix.GetLength(0);

    public event MatrixHandler? createNumber;

    public Matr(int size, string name)
    {
        Name = name;
        matrix = new int[size, size];
    }

    public void Generate()
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = rnd.Next(-20, 20);
                OnCreateNumber(Name, matrix[i, j], i, j);
            }
    }

    public void PrintMatr()
    {
        Console.WriteLine($"{Name}:");
        for (int i = 0; i < GetSize; i++)
        {
            for(int j = 0;j < GetSize; j++)
            {
                Console.Write($"{matrix[i, j],4}");
            }
            Console.WriteLine();
        }
    }

    public void OnCreateNumber(string matrName, int number, int row, int column)
    {
        if (createNumber != null)
            createNumber(matrName, number, row, column);
    }
}

internal class Journal
{
    List<string> journal=new List<string>();

    public void WriteRecord(string name, int number, int row, int column)
    {
        string record=$"В матрицу {name} записано число {number} в строку {row} столбец {column}";
        journal.Add(record);
    }

    public void PrintJournal()
    {
        foreach(string record in journal)
        {
            Console.WriteLine(record);
        }
    }
        
}

class Program
{
    static void Main(string[] args)
    {
        Matr m1 = new(5, "Matr1");

        Journal journal1 = new();
        m1.createNumber += journal1.WriteRecord;

        m1.Generate();
        m1.PrintMatr();

        Console.WriteLine($"Печать журнала для {m1.Name} ");
        journal1.PrintJournal();
    }
}

