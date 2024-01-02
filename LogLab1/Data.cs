﻿using Log_Lab_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//потом на второй лабе можно попробовать вкорячить partial

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

namespace Log_Lab_1 {
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
            return string.Format(format, X, Y[0], Y[1]);
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
    abstract partial class V2Datalist : V2Data { }
    abstract partial class V2DataArray : V2Data { }

    //•	класс V2MainCollection для коллекции объектов типа V2DataList и V2DataArray;
    partial class V2MainCollection { }
    //•	делегат void FValues(double x, ref double y1, ref double y2);
    public delegate void FValues(double x, ref double y1, ref double y2);

    //•	делегат DataItem FDI(double x);
    public delegate DataItem FDI(double x);
}