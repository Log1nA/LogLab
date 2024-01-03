using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogLab1
{
    //•	абстрактный базовый класс V2Data и два производных от него класса V2DataList и V2DataArray;
    //в классе V2DataArray данные измерений хранятся в одномерных и двумерных массивах,
    //в классе V2DataList данные измерений хранятся в коллекции List<DataItem>;
    public abstract partial class V2Data
    {
        //Абстрактный базовый класс V2Data содержит открытые
        //•	два автореализуемых свойства типа string и DateTime; свойство типа string можно трактовать как ключ объекта;
        public string key { get; set; }
        public DateTime dateTime { get; set; }
        //•	конструктор с параметрами типа string и DateTime;
        public V2Data(string Key, DateTime DateTime)
        {
            this.key = Key;
            this.dateTime = DateTime;
        }
        //•	абстрактное свойство MinField типа double (только с методом get);
        abstract public double MinField
        {
            get;
        }
        //•	абстрактный метод string ToLongString(string format);
        public abstract string ToLongString(string format);
        //•	перегруженную (override) версию виртуального метода string ToString();
        public override string ToString() => $"Key {key}, Date and Time:{dateTime}";
    }
}