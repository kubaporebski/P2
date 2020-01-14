using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BDL_GUI.Core
{
    public static class Tools
    {
        /// <summary>
        /// Zapamiętany cache dla metod.
        /// </summary>
        private static readonly Dictionary<MethodInfo, CachedAttribute> savedCache = new Dictionary<MethodInfo, CachedAttribute>();

        /// <summary>
        /// Konwersja ze standardowej kolekcji do generycznej listy obiektów.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this IEnumerable collection)
        {
            return collection.Cast<T>().ToList();
        }

        /// <summary>
        /// Pomocnicza metoda do właściwości, zamieniająca zwyczajny handler pobierający dane, na taki niezwyczajny - asynchroniczny.
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultList> AsyncDownloadHandler(this CommonWindowProperties properties)
        {
            Task<ResultList> tsk = new Task<ResultList>(() => WithOrWithoutCache(properties));
            tsk.Start();
            return await tsk;
        }

        // Pomocnicza metoda do użycia w Task.
        // Celem metody jest sprawdzenie czy podana metoda callback ma adnotację [Cached]. Jeśli tak, to cacheujemy wyniki działania.
        private static ResultList WithOrWithoutCache(CommonWindowProperties properties)
        {
            CachedAttribute attr = null;
            MethodInfo method = properties.DownloadHandler.Method;

            // Korzystamy ze statycznego pola, gdyż `method.GetCustomAttributes` za każdym razem tworzy nowy obiekt.
            // Wywołujemy więc `method.GetCustomAttributes` tylko raz, a następnie utworzony obiekt zapisujemy statycznie.
            if (savedCache.ContainsKey(method))
                attr = savedCache[method];
            else
            {
                var attrs = method.GetCustomAttributes(typeof(CachedAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    attr = attrs[0] as CachedAttribute;
                    savedCache[method] = attr;
                }
            }

            if (attr != null && attr.Valid)
                return attr.Data;
            else
            {
                ResultList data = properties.DownloadHandler();
                if (attr != null)
                    attr.Data = data;

                return data;
            }
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

        /// <summary>
        /// Sprawdzenie czy choć jedna z podanych wartości pasuje do wyrażenia regularnego.
        /// Jeśli tak - metoda zwraca true.
        /// Metoda zwraca false, gdy wyrażenie regularne nie dopasuje ani jednego z podanych ciągów znaków.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
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
