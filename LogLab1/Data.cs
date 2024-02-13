using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Лабораторная работа 1. 
//В работе определяется абстрактный базовый класс и два производных класса.
//В одном производном классе данные хранятся в массивах, в другом ─ в одном из типов коллекций стандартной библиотеки .NET. Кроме того, в работе определяется класс, содержащий коллекцию из элементов производных классов. 
//Привязка классов к данным измерений некоторых физических величин условная, так как типы, которые определяются в лабораторных работах, это “учебные” типы для изучения синтаксических конструкций языка C# и стандартной библиотеки .NET.
//Производные классы можно рассматривать как два разных формата для хранения данных измерений некоторой векторной физической величины, например, двух компонент электромагнитного поля.
//Поле измеряется в некотором множестве точек, расположенных на прямой. С каждой точкой связана ее координата и векторная величина с двумя элементами ─ значениями двух компонент поля.
//Множество точек, в которых измерено поле, можно рассматривать как одномерную сетку, с каждым узлом которой связана векторная величина с двумя элементами.
//Типы, определенные в лабораторной работе, используются в следующих лабораторных работах, в том числе в следующем семестре.
//Вариант 2
//В лабораторной работе надо определить типы для работы с данными измерений значений поля на одномерной сетке:

namespace LogLab {
    //•	struct DataItem для хранения данных, связанных с одной точкой;
    public struct DataItem {
        //Структура DataItem содержит открытые автореализуемые свойства
        //	типа double с координатой x точки, в которой измерено поле;
        public double X { get; set;}
        //	свойство типа double[] для двух значений поля {y_1,y_2 } в этой точке. 
        public double[] Y {get; set;}
        //В структуре DataItem определены открытые
        //•	конструктор DataItem (double x, double y1, double y2) для инициализации данных структуры;
        public DataItem(double x, double y1, double y2)
        {
            this.X = x;
            this.Y = new double[] { y1, y2 };
            
        }
        //•	метод string ToLongString(string format), возвращающий строку, которая содержит значение координаты точки измерения и значения поля;
        //параметр format задает формат вывода чисел с плавающей запятой; 
        public string ToLongString(string format)
        {
            return $"X: {string.Format(format, X)} Y: [{string.Format(format, Y[0])}, {string.Format(format, Y[1])}]";
        }
        //•	перегруженная (override) версия виртуального метода string ToString().
        public override string ToString()
        {
            return $"X: {X}, Y: [{Y[0]},{Y[1]}]";
        }
    }
    //    •	абстрактный базовый класс V2Data и два производных от него класса V2DataList и V2DataArray;
    partial class V2data { }
    //    в классе V2DataArray данные измерений хранятся в одномерных и двумерных массивах, в классе V2DataList данные измерений хранятся в коллекции List<DataItem>;
    partial class V2DataList : V2Data { }
    partial class V2DataArray : V2Data { }

    //•	класс V2MainCollection для коллекции объектов типа V2DataList и V2DataArray;
    partial class V2MainCollection { }

    //•	делегат void FValues(double x, ref double y1, ref double y2);
    public delegate void FValues(double x, ref double y1, ref double y2);

    //•	делегат DataItem FDI(double x);
    public delegate DataItem FDI(double x);

    //Статические методы, отвечающие делегатам FValues и FDI, можно определить в отдельном статическом классе или в одном из перечисленных выше типов.
    public static class Functions
    {
        //Объект изучений - газ под поршнем, однако комнатная температура непостоянна
        //Пусть x = расстояние на которое отклоняется поршень, относительно начального положения
        //Тогда y1 - значение температуры, а y2 - изменение давления от перемещения поршня(отрицательное, если давление упало)
        //Газ - Аргон, температура колеблется в пределах нормы - [273,298] градусов Кельвина
        const double n = 0.01;    //моль - Кол-во в-ва
        const double R = 8.3144e07;     //Газовая постоянная
        const double S = 2241.3962;     //кв.см - Площадь сечения поршня
        const double idealTemp = 285.5; //градусов Кельвина
        const double startP = 100; //см - Начальное положение поршня
        public const double idealP = (n * R * idealTemp) / (startP * S); // Изначальное давление

        public static void F(double x, ref double y1, ref double y2)
        {
            Random rnd = new Random();
            y1 = rnd.Next(273, 299);
            y2 = idealP - ((n * R * y1) / ((startP + x) * S));
        }
        public static DataItem F(double x)
        {
            DataItem dataItem = new DataItem(x, 0, 0);
            F(x, ref dataItem.Y[0], ref dataItem.Y[1]);
            return dataItem;
        }
    }
}