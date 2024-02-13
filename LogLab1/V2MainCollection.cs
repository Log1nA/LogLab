using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogLab
{
    //Класс V2MainCollection определяется как производный от класса System.Collections.ObjectModel.ObservableCollection<V2Data>.
    //Коллекция базового класса содержит элементы типа V2DataList и V2DataArray.
    

    partial class V2MainCollection: System.Collections.ObjectModel.ObservableCollection<V2Data>
    {
        //Класс V2MainCollection содержит:
        //•	открытый метод bool Contains (string key), который возвращает значение true,
        //если среди элементов коллекции есть элемент с заданным значением key свойства типа string в базовом классе, и false в противном случае;
        public bool Contains(string key)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i].key == key) return true;
            }
            return false;
        }

        //•	открытый метод bool Add (V2Data v2Data), который добавляет в коллекцию новый элемент;
        //элемент добавляется только в том случае, когда в коллекции нет элемента с таким же значением свойства типа string в базовом классе,
        //как в параметре v2Data; метод возвращает значение true, если элемент был добавлен, и значение false в противном случае;
        public new bool Add (V2Data v2Data) 
        {
            if (Contains(v2Data.key))
            {
                return false;
            }
            Insert(Count, v2Data);
            return true;
        }

        //•	конструктор V2MainCollection (int nV2DataArray, int nV2DataList), в котором в коллекцию добавляются nV2DataArray элементов типа V2DataArray и nV2DataList элементов типа nV2DataList;
        //объекты типа V2DataArray и V2DataList создаются в самом конструкторе; для параметров конструкторов выбираются некоторые значения; конструктор используется для отладки;
        public V2MainCollection(int nV2DataArray, int nV2DataList)
        {
            FValues F1 = Functions.F;
            FDI F2 = Functions.F;
            Random rnd = new Random();
            for (int i = 0; i < nV2DataArray; i++)
            {   
                double[] grid = { rnd.Next(-100, 100), rnd.Next(-100, 100)};
                base.Add(new V2DataArray("nV2array", DateTime.Now, grid, F1));
            }

            for (int i = 0; i < nV2DataList; i++)
            {
                double[] grid = { rnd.Next(-100, 100), rnd.Next(-100, 100)};
                base.Add(new V2DataList("nV2list", DateTime.Now, grid, F2));
            }
        }
        //•	открытый метод string ToLongString (string format), который возвращает строку с информацией о каждом элементе коллекции; при создании строки для каждого элемента коллекции вызывается метод ToLongString (string format);
        public string ToLongString(string format)
        {
            string str = $"Object type: {this.GetType()}\nCollection contains {Count} elements.\n";
            if(Count != 0)
            {
                for (var i = 0; i < Count; i++)
                {
                    str += $"{i+1}.{this[i].ToLongString(format)} \n";
                }
            }
            return str;
        }

        //•	перегруженную(override) версию виртуального метода string ToString(), который возвращает строку с информацией о каждом элементе коллекции; при создании строки для каждого элемента из списка List<V2Data> вызывается метод ToString().
        public override string ToString()
        {
            string str = " ";
            for (var i = 0; i < Count; i++)
            {
                str += this[i].ToString();
            }
            return str;
        }
    }
}
