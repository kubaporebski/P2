using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BDL_GUI.Core
{
    /// <summary>
    /// Klasa opisująca zbiór wyników. 
    /// Korzystam z tej klasy w interakcjach z UI, a w szczególności do wypełniania datagrida danymi.
    /// </summary>
    public abstract class ResultList : IEnumerable<object>
    {
        /// <summary>
        /// Lista obiektów pobranych metodą pobierającą dane z BDL.
        /// </summary>
        protected IEnumerable<object> Items { get; private set; } = new List<object>();

        /// <summary>
        /// Abstrakcyjna metoda dokańczająca dzieła aplikowania listy wyników do datagrida. 
        /// Każda lista ma swoje niestandardowe czynności, które trzeba obsłużyć.
        /// </summary>
        /// <param name="dg"></param>
        protected abstract void ApplyImpl(DataGrid dg);

        /// <summary>
        /// Użyj tej metody, jeśli chcesz zaaplikować wyniki do podanego datagrida.
        /// </summary>
        /// <param name="dg"></param>
        public void Apply(DataGrid dg, IEnumerable<object> filteredItems = null)
        {
            dg.ItemsSource = filteredItems ?? Items; 
            ApplyImpl(dg);
        }

        /// <summary>
        /// Konwersja do podrzędnego typu listy.
        /// Metoda dodaje elementy z podanego `enumerable`.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static T Convert<T>(IEnumerable enumerable) where T : ResultList, new()
        {
            return new T()
            {
                Items = new List<object>(enumerable.ToList<object>())
            };
        }

        /// <summary>
        /// Szczegóły filtrowania już określi klasa podrzędna.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected abstract bool FilterImpl(object item, string text);

        /// <summary>
        /// Filtrowanie zawartości z użyciem LINQ.
        /// Jeśli choć w jednej kolumnie będzie zadany tekst (wyrażenie regularne), wówczas uznajemy rekord za znaleziony.
        /// 
        /// </summary>
        /// <param name="text"></param>
        public IEnumerable<object> Filter(string text)
        {
            var query = from item in Items
                        where string.IsNullOrEmpty(text) || FilterImpl(item, text)
                        select item;
            return query.ToList();
        }

        public IEnumerator<object> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
