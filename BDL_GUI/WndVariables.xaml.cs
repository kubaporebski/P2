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
            var territorialUnitRow = Application.Current.Properties[WndTerritorialUnits.SESSION_KEY] as UnitRow;
            if (territorialUnitRow != null)
            {
                var info = MessageBox.Show(
                    $"Czy chcesz wczytać dane dla zapamiętanej wcześniej jednostki terytorialnej ( {territorialUnitRow.Name} )? \r\n" +
                    "Kliknij 'NIE' aby wybrać inną.\r\nKliknij 'ANULUJ' aby anulować operację.", "Weryfikacja", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (info)
                {
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }

                // TODO
            }
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
            
        }

        protected override bool FilterImpl(object item, string text)
        {
            // TODO
            return true; 
        }
    }
}
