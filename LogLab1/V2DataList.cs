using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Lab_1
{
    //Класс V2DataList является производным от класса V2Data. В классе V2DataList данные измерений хранятся в коллекции List<DataItem>.
    //Среди элементов DataItem, входящих в коллекцию List<DataItem>, не должно быть элементов с совпадающими значениями координат x, в которых измеряется поле.
     public partial class V2DataList: V2Data
    {
        // В классе V2DataList определить открытые
        //•	автореализуемое свойство типа List<DataItem>;
        public List<DataItem> dataItems { get; set;}
        //•	конструктор V2DataList (string key, DateTime date)
        public V2DataList(string key, DateTime date): base(key, date) 
        {
            this.dataItems = new List<DataItem>();
        }
        //конструктор V2DataList (string key, DateTime date, double[] x, FDI F), 
        //в который через параметр double[] x передается ссылка на массив с координатами, в которых измерено поле;
        //для каждого элемента массива вызывается метод F, который вычисляет значения поля {y_1,y_2 };
        //создаётся и добавляется в коллекцию List<DataItem> элемент DataItem; для равных элементов массива x в коллекцию добавляется только один элемент DataItem; 
        public V2DataList(string key, DateTime date, double[] x, FDI F) : base(key, date)
        {
            this.dataItems = new List<DataItem>();
            foreach (double item in x)
            {
                DataItem DT = new DataItem();
                DT = F(item);
                dataItems.Add(DT); 
            }
        }
        //•	реализацию абстрактного свойства MinField типа double, которое возвращает минимальное абсолютное значение компонент поля(среди всех элементов List<DataItem>);
        public override double MinField
        {
            get
            {
                double min = double.MaxValue;
                foreach (DataItem item in dataItems)
                {
                    if (Math.Abs(item.Y[0]) < min) min = item.Y[0];
                    if (Math.Abs(item.Y[1]) < min) min = item.Y[1];
                }
                return min;
            }
        }
        //•	свойство типа V2DataArray (только с методом get), которое возвращает объект типа V2DataArray, инициализированный данными класса;
        V2DataArray dataArray
        {
            get
            {
                dataArray = new V2DataArray(this.dataItems);
                return dataArray;
            }
        }
        //•	перегруженную (override) версию виртуального метода string ToString(),
        //который возвращает строку с именем типа объекта, данными базового класса и числом элементов в списке List<DataItem>;
        public override string ToString() => $"Object type: {this.GetType}, Base class data: {base.ToString}, Amount objects in list: {dataItems.Count}";

        //•	реализацию абстрактного метода string ToLongString(string format),
        //который возвращает строку с такими же данными, что и метод ToString(),
        //и дополнительно информацию о  каждом элементе из List<DataItem> – координату точки измерения и значения поля; параметр format задает формат вывода чисел с плавающей запятой.
        public override string ToLongString(string format)
        {
            string str = this.ToString();
            foreach (DataItem item in this.dataItems) { 
                str += item.ToLongString(format);
            }
            return str;
        }

    }
}
