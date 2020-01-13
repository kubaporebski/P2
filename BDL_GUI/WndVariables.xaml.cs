using BDL;
using BDL_GUI.Core;
using System;
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
    /// Interaction logic for WndVariables.xaml
    /// </summary>
    public partial class WndVariables : Window, ICommon
    {
        public string ParentSubjectId { get; set; }

        public WndVariables()
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
            var variableUnitRow = (sender as DataGrid).CurrentItem as VariablesRow;
            
            var wnd = new WndChoose(variableUnitRow);
            wnd.ShowDialog();

        }

        private ResultList Download()
        {
            return ResultList.Convert<VariablesResultList>(DataGetter.Variables(ParentSubjectId, 100));
        }
    }

    public class VariablesResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            dg.Columns[1].Header = "Id tematu";
            dg.Columns[2].Header = "Podrodzaj 1";
            dg.Columns[3].Header = "Podrodzaj 2";
            dg.Columns[4].Header = "Id jednostki miary";
            dg.Columns[5].Header = "Jednostka miary";
        }

        protected override bool FilterImpl(object item, string text)
        {
            var currentItem = item as VariablesRow;
            return RegexChecker.Instance(text).Check(currentItem.Id, currentItem.N1, currentItem.N2);
        }
    }
}
