using System;
using System.Runtime;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Windows.Markup;
using System.Globalization;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CoordMaker
{
    [Serializable]
    public class DynMatrix2D<T> : DependencyObject
    {

        public Int32 Rows 
        {
            get {
                return Items.Count;
            }
        }

        public Int32 Cols
        {
            get {
                return Items[0].Count;
            }
        }

        public DynMatrix2D()
        {
            Items = new ObservableCollection<ObservableCollection<T>>();
        }

        public DynMatrix2D(Int32 ysize, Int32 xsize)
        {
            Items = new ObservableCollection<ObservableCollection<T>>();
            setsizes(ysize, xsize);
        }

        public bool ElementExists(int j, int i)
        {
            if (j > Items.Count - 1) return false;
            else
            {
                if (i > Items[j].Count - 1)
                    return false;
            }
            return true;
        }

        // Dependency Property
        public static readonly DependencyProperty ItemsDP = DependencyProperty.Register("Items", typeof(ObservableCollection<ObservableCollection<T>>), typeof(DynMatrix2D<T>), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ObservableCollection<ObservableCollection<T>> Items
        {
            get
            {
                return (ObservableCollection<ObservableCollection<T>>)GetValue(ItemsDP);
            }
            set { SetValue(ItemsDP, value); }
        }
      
        public T this[int j, int i]
        {
            get
            {
                // This indexer is very simple, and just returns or sets
                // the corresponding element from the internal array.
          /*      if (j > arr.Count) return null;
                if (i > arr[j].Count) return null;*/
                return Items[j][i];
            }
            set
            {
                if (j > Items.Count - 1)
                {
                    setsizes(j+1, Cols);
                }

                if (i > Items[0].Count - 1)
                {
                    setsizes(Rows, i + 1);
                }
                Items[j][i] = value;
            }
        }

        public void setsizes(Int32 ysize, Int32 xsize)
        {

            while (ysize > Items.Count)
            {
                Items.Add(new ObservableCollection<T>());
            }

            while (ysize < Items.Count)
            {
                Items.RemoveAt(Items.Count - 1);
            }

        /*    while (xsize > Items[0].Count)
            {*/
                foreach (ObservableCollection<T> c in Items)
                {
                    while (xsize > c.Count)
                    {
                        c.Add(default(T));
                    }

                    while (xsize < c.Count)
                    {
                        Items.RemoveAt(c.Count - 1);
                    }
                }
      //      }
           
        }

        public void Reset()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i= 0; i < Cols; i++)
                {
                    this[j, i] = default(T);
                }
            }
        }

        // Перегружаем бинарный оператор +
        public static DynMatrix2D<T> operator +(DynMatrix2D<T> M1, DynMatrix2D<T> M2)
        {
            DynMatrix2D<T> Summ = new DynMatrix2D<T>((M1.Rows>M2.Rows)? M1.Rows : M2.Rows, (M1.Cols>M2.Cols)? M1.Cols : M2.Cols);
            for (int j = 0; j < Summ.Rows; j++)
            {
                for (int i = 0; i < Summ.Rows; i++)
                {
                    if (M1.ElementExists(j,i))
                    {
                        if (M2.ElementExists(j, i))
                        {
                            Summ[j, i] = (dynamic)M1[j, i] + (dynamic)M2[j, i];
                        }
                    }
                    var m1_item = M1[j,i];
                }
            }
            return Summ;
        }

        // Перегружаем бинарный оператор - 
        public static DynMatrix2D<T> operator -(DynMatrix2D<T> M1, DynMatrix2D<T> M2)
        {
            DynMatrix2D<T> Summ = new DynMatrix2D<T>((M1.Rows > M2.Rows) ? M1.Rows : M2.Rows, (M1.Cols > M2.Cols) ? M1.Cols : M2.Cols);
            /* arr.x = obj1.x + obj2.x;
             arr.y = obj1.y + obj2.y;
             arr.z = obj1.z + obj2.z;*/
            return Summ;
        }
    }
}
