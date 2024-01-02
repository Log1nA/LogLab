using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Log_Lab_1
{
    //Класс V2DataArray определяется как производный от абстрактного базового класса V2Data.
    //В классе V2DataArray в массиве типа double[] хранятся координаты x, в которых измерено поле,
    //в прямоугольном массиве типа double[,] хранятся измеренные значения поля.
    //Прямоугольный массив типа double[,] состоит из двух  строк, длина которых  равна числу элементов в массиве x.
    //В первой строке массива хранятся значения компоненты y_1, во второй строке значения компоненты y_2.
    //Множество значений координат x, которые хранятся в массиве типа double[] можно рассматривать как узлы сетки,
    //каждому узлу сетки отвечают два значения типа double из массива типа double[,].
    public partial class V2DataArray: V2Data
    {
        //double[] X;
        //double[,] Y;
        //Класс V2DataArray содержит открытые
        //•	автореализуемое свойство типа double[] для сетки;
        public double[] X{ get; set;}
        //•	автореализуемое свойство типа double[,] для массива со значениями поля в узлах сетки;
        public double[,] Y { get; set;}
        //•	конструктор  V2DataArray(string key, DateTime date) для инициализации данных базового класса;
        //в этом конструкторе распределяется память для массивов, в которых хранятся данные, с нулевым числом элементов;
        public V2DataArray(string key, DateTime date): base(key, date)
        {
            this.X = new double[] { };
            this.Y = new double[,] { { }, { }};
        }
        //•	конструктор V2DataArray(string key, DateTime date, double[] x, FValues F), 
        //в который через параметр double[] x передается массив с координатами,
        //в которых измерено поле, распределяется память для массива double[,],
        //и для каждого элемента массива x вызывается метод F типа FValues, который вычисляет значения поля в узле;
        public V2DataArray(string key, DateTime date, double[] x, FValues F) : base(key, date)
        {
            this.X = x;
            this.Y = new double[,] { { 2 }, { X.Length } };
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = F(X[i]);
            }
        }
        //•	конструктор V2DataArray(string key, DateTime date, int nX, double xL, double xR, FValues F),
        //в который через параметры int nX, double xL, double xR передается информация о равномерной сетке,
        //на которой были выполнены измерения поля; nX точек, в которых было измерено поле, расположены на отрезке[xL, xR] с постоянным шагом;
        //в конструкторе распределяется память для массивов для координат и компонент поля, вычисляются координаты узлов сетки и для каждого узла сетки вызывается метод F типа FValues, который вычисляет значения поля в узле;
        V2DataArray(string key, DateTime date, int nX, double xL, double xR, FValues F): base(key, date)
        {
            this.X = new double[] {nX};
            this.Y = new double[,] { { 2 }, { nX } };
            double step = (Math.Abs(xL - xR)) / nX;
            X[0] = xL;
            Y[0] = F(X[0]);
            for (int i = 1; i < nX; i++)
            {   
                X[i] = X[i-1] + step;
                Y[i] = F(X[i]);
            }
        }
        //•	индексатор типа double[] (только с методом get) с целочисленным индексом, который может принимать только два значения 0,1;
        //индексатор возвращает ссылку на массив,  который содержит значения соответствующей компоненты поля для всех точек измерения;
        public double[] this[int key]
        {
            get
            {
                if (key != 0 || key != 1)
                    throw new IndexOutOfRangeException("Недопустимый индекс");
                double[] a = new double[X.Length];
                for(int i = 0; i < X.Length; i++) a[i] = Y[key,i];
                return a;
            }
        }
        //•	оператор преобразования типа V2DataArray к типу V2DataList ─ метод public static explicit operator V2DataList(V2DataArray source);
        //преобразующий данные, которых хранятся в объекте V2DataArray, в данные в формате V2DataList ─ для каждого элемента массива double[],
        //в котором хранятся координаты точек измерения поля, выбираются соответствующие значения поля из массива double[,], создается объект типа DataItem и добавляется в коллекцию List<DataItem>; 
        public static explicit operator V2DataList(V2DataArray source)
        {
            for (int i = 0;i < source.X.Length; i++)
            {

            }
        }


    }
}
