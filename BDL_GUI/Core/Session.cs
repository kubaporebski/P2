using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BDL_GUI.Core
{
    public class Session
    {
        /// <summary>
        /// Singleton. Jedna jedyna instancja tej klasy.
        /// </summary>
        public static Session Current { get; private set; } = new Session();

        /// <summary>
        /// Pobranie właściwości z sesji.
        /// Pod spodem właściwości zapisywane są w `Application.Current.Properties`.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return Application.Current.Properties.Contains(key) ?
                    Application.Current.Properties[key] :
                    null;
            }

            set
            {
                Application.Current.Properties[key] = value;
            }
        }

        /// <summary>
        /// Zwrócenie właściwości dla danego okienka.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public object this[ICommon window]
        {
            get
            {
                return this[window.GetType().FullName];
            }

            set
            {
                this[window.GetType().FullName] = value;
            }
        }
    }
}
