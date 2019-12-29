using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

    /// <summary>
    /// Pomocnicza klasa do odnajdywania tekstu (wyrażenia regularnego) w zadanej liście ciągów znaków.
    /// </summary>
    public class RegexChecker
    {
        /// <summary>
        /// Ustawienie wyrażenie regularnego pod kątem którego sprawdzać będziemy ciągi znaków.
        /// </summary>
        public string Pattern
        {
            get;
            private set;
        }

        public bool Check(params string[] values)
        {
            return values.Any(value => Regex.IsMatch(value, Pattern));
        }

        /// <summary>
        /// Zwrócenie instancji tej klasy.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static RegexChecker Instance(string pattern)
        {
            return new RegexChecker() { Pattern = pattern };
        }
    }
}
