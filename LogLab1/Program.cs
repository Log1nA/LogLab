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



namespace LogLab 
{
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


            Console.WriteLine($"ЭЭ  \n{v23MainCollection.MaxField1}\n");
            foreach (var elem in v23MainCollection.CoordinatesOrder)
            {
                Console.Write(elem.ToString() + " ");
            }

            //Lab2
            //Отладка программы
            //Для отладки программы в классе, который содержит статический метод Main, определить два статических метода – один метод для отладки чтения/записи данных в файл,
            //второй метод для отладки свойств класса V2MainCollection с запросами LINQ. Эти методы вызываются из метода Main.
            //В методе для отладки чтения/записи данных в файл надо создать объект V2DataArray. Сохранить его в файле. Восстановить объект из файла и вывести исходный и восстановленный объекты.
            static void DebugSaveLoad(V2DataArray saving, V2DataArray loading, string route)
            {
                Console.WriteLine("\n DEBUGSAVELOAD STARTED ");
                bool saved = saving.Save(route);
                if (saved)
                {
                    bool loaded = V2DataArray.Load(route, ref loading);
                    Console.WriteLine($"loaded = {loaded}");
                    if (!loaded)
                    {
                        Console.WriteLine("Fail Loading...");
                    }
                }
                else
                {
                    Console.WriteLine("Fail Saving...");
                }
            }
            //Во втором методе
            //Создать объект типа V2MainCollection и вывести всю коллекцию.В коллекцию надо добавить такой набор элементов, чтобы можно было проверить, что все запросы LINQ работают правильно. Среди элементов коллекции должен быть элемент типа V2DataList, у которого в списке List<DataItem> нет элементов, и элемент типа V2DataArray, в котором число узлов сетки равно 0.
            //Вызвать все перечисленные выше свойства класса V2MainCollection с запросами LINQ и вывести результаты выполнения запросов.Вывод должен быть подписан - перед выводом результата выполнения каждого запроса должна быть выведена информация с описанием запроса.
            static void DebugLINQ()
            {
                Console.WriteLine("LINQ DEBUGGER STARTED");

                FValues F1 = Functions.F;
                FDI F2 = Functions.F;
                V2MainCollection DebugCollection = new V2MainCollection(1, 1);
                double[] grid1 = { 0, 0, 0, -4, 10 };
                double[] grid2 = { -4, 2, 0, 0 };
                DebugCollection.Add(new V2DataList("debugDataList", DateTime.Now, grid1, F2));
                DebugCollection.Add(new V2DataArray("debugDataArray", DateTime.Now, grid2, F1));
                DebugCollection.Add(new V2DataArray("emptyDataArray", DateTime.Now));
                DebugCollection.Add(new V2DataList("emptyDataList", DateTime.Now));

                Console.WriteLine(DebugCollection.ToLongString("{0,4:F3}"));
                Console.WriteLine("Zero module elements in collection amount: " + DebugCollection.MaxZeroRes);
                Console.WriteLine("Max module element in collection: " + DebugCollection.MaxField1);
                Console.WriteLine("X in collection: ");
                foreach (var elem in DebugCollection.CoordinatesOrder)
                {
                    Console.Write(elem.ToString() + " ");
                }
            }

            double[] grid = new double[] { 2.2F, 2.4F, 2 };
            FValues F1 = Functions.F;
            V2DataArray v2darray = new V2DataArray("111", DateTime.Now, grid, F1); ;
            Console.WriteLine("Program Lab2\n");
            //DebugLINQ();
            V2DataArray v2darray_new = new V2DataArray("load", new DateTime(1, 1, 1), grid, F1);
            DebugSaveLoad(v2darray, v2darray_new, "test.txt");
            Console.WriteLine(v2darray_new.ToLongString("{0,4:F3}"));
        }
    }
}
//Console.WriteLine("Debug\n");