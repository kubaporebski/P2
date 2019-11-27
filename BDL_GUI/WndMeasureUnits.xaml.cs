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
                DownloadHandler = Download,
                GridColumnHeaders = CreateGridColumns()
            };
        }

        private List<string> CreateGridColumns()
        {
            return new List<string>(new string[] {
                "Id",
                "Skrócona nazwa (symbol)",
                "Opis"
            });
        }

        private List<object> Download()
        {
            return DataGetter.Measures().ToObjectList();
        }
    }
}
