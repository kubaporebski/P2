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
    /// Interaction logic for WndData.xaml
    /// </summary>
    public partial class WndData : Window, ICommon
    {

        public VariablesRow Variable { get; set; }

        public UnitRow TerritorialUnit { get; set; }

        public int Level { get; set; } = 1;

        public int YearFrom { get; set; } = DateTime.Today.Year - 1;

        public int YearTo { get; set; } = DateTime.Today.Year;

        public WndData()
        {
            InitializeComponent();
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
            return ResultList.Convert<DataResultList>(
                DataGetter.DataByVariable(int.Parse(Variable.Id), TerritorialUnit.Id, YearFrom, YearTo, Level, 100));
        }
    }

    public class DataResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            
        }

        protected override bool FilterImpl(object item, string text)
        {
            // TODO
            return true;
        }
    }
}
