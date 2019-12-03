using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BDL;
using BDL_GUI.Core;

namespace BDL_GUI
{
    /// <summary>
    /// Interaction logic for WndTerritorialUnits.xaml
    /// </summary>
    public partial class WndTerritorialUnits : Window, ICommon
    {
        public WndTerritorialUnits() 
        {
            InitializeComponent();
            CommonWindow.Implement(this);
        }
        
        public CommonWindowProperties GetProperties()
        {
            return new CommonWindowProperties()
            {
                DownloadHandler = Download
            };
        }

        private ResultList Download()
        {
            return TerritorialUnitsResultList.Convert(DataGetter.Units(100));
        }
    }

    public class TerritorialUnitsResultList : ResultList
    {
        internal static ResultList Convert(IEnumerable enumerable)
        {
            throw new NotImplementedException();
        }

        protected override void ApplyImpl(DataGrid dg)
        {
            throw new NotImplementedException();
        }
    }
}
