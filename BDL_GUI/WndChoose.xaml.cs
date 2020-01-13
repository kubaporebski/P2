using BDL;
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
    /// Interaction logic for WndChoose.xaml
    /// </summary>
    public partial class WndChoose : Window
    {
        public VariablesRow Variable { get; private set; }

        public UnitRow SavedTerritorialUnit { get; private set; }

        public WndChoose(VariablesRow variable)
        {
            InitializeComponent();
            Variable = variable;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            var wndData = new WndData(new Model()
            {
                Variable = Variable,
                TerritorialUnit = SavedTerritorialUnit,
                YearFrom = int.Parse(tbYearFrom.Text),
                YearTo = int.Parse(tbYearTo.Text),
                Level = int.Parse(tbLevel.Text)
            });
            wndData.ShowDialog();
        }

        private void BtnTerritorialUnit_Click(object sender, RoutedEventArgs e)
        {
            var wndTU = new WndTerritorialUnits();
            wndTU.ShowDialog();
            SavedTerritorialUnit = wndTU.TerritorialUnit;
        }
    }
}
