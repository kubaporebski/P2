using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BDL_GUI.Core
{
    public class CommonWindow : Grid
    {
        private Button btnDownload;

        private DataGrid dgData;

        /// <summary>
        /// Właściwości okienka
        /// </summary>
        private CommonWindowProperties properties;

        // Konstruktor jest prywatny
        private CommonWindow()
        {
            CreateEverything();
        }

        /// <summary>
        /// Metoda tworząca konstrukcję, wszystkie potrzebne kontrolki.
        /// </summary>
        private void CreateEverything()
        {
            Margin = new Thickness(25);

            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(75.0) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength() });

            Children.Add(CreateDataGrid());
            Children.Add(CreateDownloadButton());
        }

        private UIElement CreateDownloadButton()
        {
            btnDownload = new Button()
            {
                Content = "Pobierz"
            };
            btnDownload.Click += BtnDownload_Click;
            return btnDownload;
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            dgData.ItemsSource = properties.DownloadHandler();
        }

        private UIElement CreateDataGrid()
        {
            dgData = new DataGrid();

            return dgData;
        }

        /// <summary>
        /// Pobranie instancji klasy.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static CommonWindow Instance(CommonWindowProperties properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            return new CommonWindow()
            {
                properties = properties
            };
        }

        /// <summary>
        /// Zaimplementowanie wspólnego okienka w podanym okienku.
        /// </summary>
        /// <param name="wnd"></param>
        public static void Implement(ICommon wnd)
        {
            var cwnd = Instance(wnd.GetProperties());
            wnd.Content = cwnd;

        }
    }
}
