using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.IO;



namespace LogLab1 {
    class Program
    {
        static void Main(string[] args)
        {
            //В методе Main()
            //1.Создать объект типа V2DataArray, вывести его данные с помощью метода ToLongString(string format).С помощью оператора преобразования,
            //определенного в классе V2DataArray, преобразовать его в объект типа V2DataList и вывести его данные с помощью метода ToLongString(string format).

            Console.WriteLine("Выполнение 1-го пункта:\n");
            FValues F = Functions.F;
            V2DataArray v21DataArray = new V2DataArray("Пункт 1", DateTime.Now, 5, -10, 10, F);
            Console.WriteLine(v21DataArray.ToLongString("{0,4:F3}"));
            V2DataList v21DataList = (V2DataList)v21DataArray;
            Console.WriteLine(v21DataList.ToLongString("{0,4:F3}"));

            //2.Создать объект типа V2DataList и вывести значение его свойства типа V2DataArray с помощью метода ToLongString(string format).

            Console.WriteLine("Выполнение 2-го пункта:\n");
            V2DataList v22DataList = new V2DataList("Пункт 2", DateTime.Now);
            Console.WriteLine(v22DataList.DataArray.ToLongString("{0,4:F3}"));

            //3.Создать объект типа V2MainCollection и вывести данные объекта V2MainCollection с помощью метода ToLongString(string format). 

            Console.WriteLine("Выполнение 3-го пункта:\n");
            V2MainCollection v23MainCollection = new V2MainCollection(2, 2);
            Console.WriteLine(v23MainCollection.ToLongString("{0,4:F3}"));

            //4.Для всех элементов из V2MainCollection вывести значения свойства MinField.

            Console.WriteLine("Выполнение 4-го пункта:\n");
            foreach (var item in v23MainCollection)
            {
                Console.WriteLine($"{item.ToString()}    MinField: {item.MinField}");
            }

            //Console.WriteLine("Debug\n");
        }
    }
}