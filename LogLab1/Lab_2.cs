using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLab
{
    //   В лабораторной работе 2 в класс V2DataArray из лабораторной работы 1 надо добавить новые методы и свойства, связанные с записью и чтением данных из файла.
    //   В классе V2MainCollection надо определить свойства с запросами LINQ к данным, которые хранятся в коллекции V2MainCollection.

    //Реализация интерфейса IEnumerable<DataItem>

    //Абстрактный класс V2Data надо объявить как реализующий интерфейс IEnumerable<DataItem>.
    public abstract partial class V2Data : IEnumerable<DataItem>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public abstract IEnumerator<DataItem> GetEnumerator();
    }

//В производных классах V2DataList и V2DataArray надо реализовать интерфейс IEnumerable<DataItem>:
    public partial class V2DataList : IEnumerable<DataItem>
    {
        //в классе V2DataList реализация интерфейса IEnumerable<DataItem> перечисляет все элементы DataItem из списка List<DataItem>;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public override IEnumerator<DataItem> GetEnumerator()
        {
            return dataItems.GetEnumerator();
        }
    }

    //    в классе V2DataArray реализация интерфейса IEnumerable<DataItem> перечисляет все данные как экземпляры DataItem − для каждого узла сетки создается экземпляр DataItem с координатой x узла сетки и значениями поля { y_1,y_2 }
    //    в этом узле.
    public partial class V2DataArray : IEnumerable<DataItem>
    {
        public override IEnumerator<DataItem> GetEnumerator()
        {
            List<DataItem> list = new List<DataItem>();
            for (int i = 0; i < X.Length; i++)
            {
                DataItem newItem = new DataItem(X[i], Y[0, i], Y[1, i]);
                list.Add(newItem);
            }
            return list.GetEnumerator();
        }
    }


    //    Запросы LINQ

    //В классе V2MainCollection надо определить свойства(только с методом get) для выполнения операций с данными, использующие интегрированные в язык C# запросы LINQ.
    //В этих свойствах не должно быть операторов foreach или операторов цикла, только запросы LINQ.
    //Результат измерений – это данные для одного узла сетки(координата x и значения поля { y_1,y_2 }
    //    в этом узле) для элементов, которые имеют тип V2DataArray, и элемент DataItem в списке List<DataItem> для элементов, которые имеют тип V2DataList. 
    //Точка, в которой измерено поле, – это узел сетки в элементах V2DataArray и значение координаты x в элементах DataItem в типе V2DataList.
    //Число результатов измерений в элементах V2DataList – это число элементов в списке List<DataItem>. Число результатов измерений в элементах V2DataArray – это число узлов сетки.
           
    public partial class V2MainCollection
    {
        //В классе V2MainCollection надо определить
        //Cвойство типа int, возвращающее максимальное число результатов измерений с нулевым значением модуля поля √(y_1^2+y_2^2 ) среди всех элементов V2Data в коллекции V2MainCollection.
        //Если в коллекции нет элементов, свойство возвращает значение -1. (Для каждого элемента V2MainCollection вычисляется число результатов измерений с нулевым значением модуля поля.
        //Свойство возвращает максимальное значение элементов из этой последовательности).
        public int MaxZeroRes
        {
            get
            {
                if (!this.Any()) return -1;
                var zeroItem = from dataElement in this
                             from DataItem in dataElement
                             where (Math.Pow(DataItem.Y[0], 2) + Math.Pow(DataItem.Y[1], 2)) == 0
                             select DataItem;
                return zeroItem.Count();
            }
        }

        //Свойство типа DataItem?, возвращающее объект DataItem, в котором модуль вектора поля имеет максимальное значение среди всех результатов измерений.
        //Если в коллекции V2MainCollection несколько таких элементов, свойство возвращает любой из них.Если в коллекции нет элементов, свойство возвращает значение null.
        public DataItem? MaxField1
        {
            get
            {
                if (!this.Any()) return null;
                var modules = from dataElement in this
                                from DataItem in dataElement
                                select Math.Pow(DataItem.Y[0], 2) + Math.Pow(DataItem.Y[1], 2);
                var maxModule = modules.Max(p => p);
                var maxModuleItem = from DataElement in this
                                        from DataItem in DataElement
                                        where Math.Pow(DataItem.Y[0], 2) + Math.Pow(DataItem.Y[1], 2) == maxModule
                                        select DataItem;
                return maxModuleItem.First();
            }
        }

        //Свойство типа IEnumerable<double>, которое перечисляет в порядке возрастания все координаты x точек,
        //в которых измерено поле, такие, что среди всех элементов коллекции V2MainCollection они встречаются только один раз.Если в коллекции нет элементов, свойство возвращает значение null.
        //(В каждом отдельном элементе коллекции V2MainCollection среди точек, в которых измерено поле, нет точек с равными значениями координаты x.В разных элементах V2MainCollection координаты x могут совпадать. Надо выбрать те значения x, которые не повторяются).
        public IEnumerable<double> CoordinatesOrder
        {
            get
            {
                if (!this.Any()) return null;
                IEnumerable<double> coordinates = from dataElement in this
                                                    from DataItem in dataElement
                                                    select DataItem.X;
                IEnumerable<double> uniqueCoordinates = coordinates.Distinct().OrderByDescending(x => x).Reverse(); 
                return uniqueCoordinates;
            }
        }
    }


    //Запись и восстановление данных
    //В класс V2DataArray добавить
    public partial class V2DataArray
    {
        //экземплярный метод bool Save(string filename) или статический метод bool Save(string filename, V2DataArray );
        //Метод Save сохраняет все данные объекта(в том числе данные из базового класса) в файле с именем filename.
        public bool Save(string filename)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.WriteLine(this.key);
                writer.WriteLine(this.dateTime);
                writer.WriteLine(X.Length);
                for (int i = 0; i < X.Length; i++)
                {
                    writer.WriteLine(X[i]);
                    writer.WriteLine(Y[0, i]);
                    writer.WriteLine(Y[1, i]);
                }
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("from Exception");
                return false;
            }
            finally
            {
                Console.WriteLine("from finally");
            }
        }

        //    статический метод bool Load(string filename, ref V2DataArray ).
        //Метод Load восстанавливает все данные объекта из файла с именем filename.
        public static bool Load(string filename, ref V2DataArray v2)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                v2.key = reader.ReadLine();
                v2.dateTime = Convert.ToDateTime(reader.ReadLine());
                int nn = Convert.ToInt32(reader.ReadLine());

                v2.X = new double[nn];
                v2.Y = new double[2, nn];

                for (int j = 0; j < nn; j++)
                {
                    Console.WriteLine($"j = {j}");
                    double x = Convert.ToDouble(reader.ReadLine());
                    Console.WriteLine(x);
                    double y = Convert.ToDouble(reader.ReadLine());
                    Console.WriteLine(y);
                    double z = Convert.ToDouble(reader.ReadLine());
                    Console.WriteLine(z);
                    v2.X[j] = x;
                    v2.Y[0, j] = y;
                    v2.Y[1, j] = z;
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("from Exception");
                return false;
            }
            finally
            {
                Console.WriteLine("from finally");
            }
        }
        //Для сохранения/восстановления объекта типа V2DataArray можно использовать JSON-сериализацию или методы для записи/чтения из классов BinaryWriter/BinaryReader или StreamWriter/ StreamReader.
        //Коды, которые сохраняют данные в файле, читают данные из файла и преобразуют их в объекты соответствующего типа, должны находиться в блоке try-catch-finally и обрабатывать исключения, которые могут быть брошены при записи и чтении из файла.
    }
   

    //Отладка программы
    //Для отладки программы в классе, который содержит статический метод Main, определить два статических метода – один метод для отладки чтения/записи данных в файл,
    //второй метод для отладки свойств класса V2MainCollection с запросами LINQ. Эти методы вызываются из метода Main.
    //В методе для отладки чтения/записи данных в файл надо создать объект V2DataArray. Сохранить его в файле. Восстановить объект из файла и вывести исходный и восстановленный объекты.
    //Во втором методе
    //Создать объект типа V2MainCollection и вывести всю коллекцию.В коллекцию надо добавить такой набор элементов, чтобы можно было проверить, что все запросы LINQ работают правильно. Среди элементов коллекции должен быть элемент типа V2DataList, у которого в списке List<DataItem> нет элементов, и элемент типа V2DataArray, в котором число узлов сетки равно 0.
    //Вызвать все перечисленные выше свойства класса V2MainCollection с запросами LINQ и вывести результаты выполнения запросов.Вывод должен быть подписан - перед выводом результата выполнения каждого запроса должна быть выведена информация с описанием запроса.

}
