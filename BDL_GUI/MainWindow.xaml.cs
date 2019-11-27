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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BDL_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSubjects_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(new NotImplementedException().Message);
        }

        private void BntInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("GUI do BDL_DLL. Copyright MKM BI Enterprise Solutions.", "Info");
        }

        private void BtnMeasureUnits_Click(object sender, RoutedEventArgs e)
        {
            var w = new WndMeasureUnits();
            w.ShowDialog();
        }

        private void BtnUnits_Click(object sender, RoutedEventArgs e)
        {

            var w = new WndTerritorialUnits();
            w.ShowDialog();
        }
    }
}
