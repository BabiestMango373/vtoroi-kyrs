using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary10
{
    public class ProductionComparer : IComparer 
    {
        public int Compare(object x, object y)
        {
            // Проверяем, что оба объекта — экземпляры класса Production
            if (x is Production p1 && y is Production p2)
            {
                // Сравниваем по числу работников
                return p1.Employees.CompareTo(p2.Employees);
            }

            return 0;
        }
    }
}
