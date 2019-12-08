using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BDL_GUI.Core
{
    /// <summary>
    /// Klasa opisująca zbiór wyników. 
    /// Korzystam z tej klasy w interakcjach z UI, a w szczególności do wypełniania datagrida danymi.
    /// </summary>
    public abstract class ResultList 
    {
        /// <summary>
        /// Lista obiektów pobranych metodą pobierającą dane z BDL.
        /// </summary>
        protected List<object> Items { get; private set; } = new List<object>();

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
        public void Apply(DataGrid dg)
        {
            dg.ItemsSource = Items;
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
            var list = new T();
            list.Items.AddRange(enumerable.ToList<object>());
            return list;
        }
    }
}
