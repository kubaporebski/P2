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
    /// Interaction logic for WndAttributes.xaml
    /// </summary>
    public partial class WndAttributes : Window, ICommon
    {
        public WndAttributes()
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

        [Cached]
        private AttributesResultList Download()
        {
            return ResultList.Convert<AttributesResultList>(DataGetter.Attributes());
        }
    }

    public class AttributesResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            if (dg.Columns.Count == 0)
                return;

            dg.Columns[0].Header = "Id";

            dg.Columns[1].Header = "Nazwa";
            dg.Columns[2].Width = new DataGridLength(80);

            dg.Columns[2].Header = "Symbol";
            dg.Columns[2].Width = new DataGridLength(80);

            dg.Columns[3].Header = "Opis";
            dg.Columns[3].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
        }

        protected override bool FilterImpl(object item, string text)
        {
            var currentItem = item as AttributeRow;
            var checker = RegexChecker.Instance(text);
            return checker.Check(currentItem.Id.ToString(), currentItem.Name, currentItem.Symbol, currentItem.Description);
        }
    }
}
