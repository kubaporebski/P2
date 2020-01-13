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
        public const string SESSION_KEY = "SavedTerritorialUnit";

        public UnitRow TerritorialUnit { get; private set; }

        public WndTerritorialUnits() 
        {
            InitializeComponent();
            CommonWindow.Implement(this);
        }
        
        public CommonWindowProperties GetProperties()
        {
            return new CommonWindowProperties()
            {
                DownloadHandler = Download,
                LoadedHandler = OnLoaded
            };
        }

        private void OnLoaded(CommonWindow wnd)
        {
            wnd.DgData.MouseDoubleClick += DgData_MouseDoubleClick;
        }

        private void DgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentItem = (sender as DataGrid).CurrentItem as UnitRow;
            //Application.Current.Properties[SESSION_KEY] = currentItem;
            TerritorialUnit = currentItem;

            MessageBox.Show("Zapamiętano wybraną jednostkę terytorialną.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ResultList Download()
        {
            return ResultList.Convert<TerritorialUnitsResultList>(DataGetter.Units(100));
        }
    }

    public class TerritorialUnitsResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            if (dg.Columns.Count == 0)
                return;

            dg.Columns[1].Header = "Nazwa";
            dg.Columns[2].Header = "Id nadrzędnej";
            dg.Columns[3].Header = "Poziom";
        }

        protected override bool FilterImpl(object item, string text)
        {
            var currentItem = item as UnitRow;
            var checker = RegexChecker.Instance(text);
            return checker.Check(currentItem.Id.ToString(), currentItem.Level.ToString(), currentItem.Name, currentItem.ParentId.ToString());
        }
    }
}
