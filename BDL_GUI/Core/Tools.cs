using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDL_GUI.Core
{
    public static class Tools
    {
        /// <summary>
        /// Konwersja ze standardowej kolekcji do generycznej listy obiektów.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static  List<T> ToList<T>(this IEnumerable collection)
        {
            return collection.Cast<T>().ToList();
        }
    }
}
