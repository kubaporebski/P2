using BDL;
using BDL_GUI.Core;
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

namespace BDL_GUI
{
    /// <summary>
    /// Interaction logic for WndMeasureUnits.xaml
    /// </summary>
    public partial class WndMeasureUnits : Window, ICommon
    {
        public WndMeasureUnits()
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

        private MeasureUnitResultList Download()
        {
            return MeasureUnitResultList.Convert(DataGetter.Measures());
        }
    }


    public class MeasureUnitResultList : ResultList
    {
        
        internal static MeasureUnitResultList Convert(IEnumerable enumerable)
        {
            var murl = new MeasureUnitResultList();
            foreach (var ob in enumerable)
            {
                murl.Items.Add(ob);
            }
            return murl;
        }

        protected override void ApplyImpl(DataGrid dg)
        {
            dg.Columns[0].Header = "Id";
            dg.Columns[1].Header = "Symbol";
            dg.Columns[1].Width = new DataGridLength(0.3, DataGridLengthUnitType.Star);

            dg.Columns[2].Header = "Opis";
            dg.Columns[2].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);

        }
    }
}
