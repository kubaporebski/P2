using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BDL_GUI.Core
{
    public class CommonWindowProperties
    {
        /// <summary>
        /// Obsługa pobrania danych z Banku Danych Lokalnych. Na wyjściu jest lista encji.
        /// </summary>
        public Func<ResultList> DownloadHandler { get; set; }

        public Action<CommonWindow> LoadedHandler { get; set; }
    }


}
