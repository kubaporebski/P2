﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BDL_GUI.Core
{
    /// <summary>
    /// Klasa okienka zawierająca kilka wspólnych kontrolek: datagrid, przycisk 'Pobierz', itd.
    /// </summary>
    public class CommonWindow : Grid
    {
        private Button btnDownload;

        private DataGrid dgData;

        /// <summary>
        /// Właściwości okienka
        /// </summary>
        private CommonWindowProperties properties;

        // Konstruktor jest prywatny
        private CommonWindow(CommonWindowProperties props)
        {
            properties = props;
            CreateEverything();
        }

        /// <summary>
        /// Metoda tworząca konstrukcję, wszystkie potrzebne kontrolki.
        /// </summary>
        private void CreateEverything()
        {
            Margin = new Thickness(25);
            HorizontalAlignment = HorizontalAlignment.Stretch;

            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(75.0) });

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
            SetRow(btnDownload, 1);
            return btnDownload;
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var run = true;
            do
            {
                try
                {
                    dgData.ItemsSource = properties.DownloadHandler();

                    for (var i = 0; i < properties.GridColumnHeaders.Count; i++)
                        dgData.Columns[i].Header = properties.GridColumnHeaders[i];

                    break;
                }
                catch (Exception ex)
                {
                    var msgBoxRet = MessageBox.Show(
                        $"Wystąpił błąd podczas pobierania danych:\r\n{ex.Message}. Czy ponowić próbę?", "Usterka!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    run = (msgBoxRet == MessageBoxResult.Yes);
                }
            } while (run);
        }

        private UIElement CreateDataGrid()
        {
            dgData = new DataGrid();
            dgData.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            SetRow(dgData, 0);
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

            return new CommonWindow(properties);
        }

        /// <summary>
        /// Zaimplementowanie wspólnego okienka w podanym okienku.
        /// </summary>
        /// <param name="wnd"></param>
        public static void Implement(ICommon wnd)
        {
            var cwnd = Instance(wnd.GetProperties());
            wnd.Content = cwnd;

            if (wnd is Window w)
            {
                w.ShowInTaskbar = false;
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

        }
    }
}
