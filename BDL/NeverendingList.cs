using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDL
{
    /// <summary>
    /// Niekończąca się lista.
    /// Lista pozwalająca na ciągłe iterowanie - ciągłe pobieranie następnego elementu.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class NeverendingList<T> : List<T> 
    {
        private int currentIndex = 0;

        /// <summary>
        /// Pobranie następnego elementu z listy.
        /// Właściwość zwraca pierwszy, drugi, ..., n-1 -ty element z listy (gdzie n to ilość elementów), po czym zaczyna od początku.
        /// </summary>
        public T NextElement
        {
            get
            {
                if (Count == 0)
                    throw new IndexOutOfRangeException();

                var ret = this[currentIndex];

                currentIndex += 1;
                if (currentIndex >= Count)
                    currentIndex = 0;

                return ret;
            }
        }
    }
}
