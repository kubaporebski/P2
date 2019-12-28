using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BDL_GUI.Core
{
    /// <summary>
    /// Klasa okienka zawierająca kilka wspólnych kontrolek: datagrid, przycisk 'Pobierz', itd.
    /// </summary>
    public class CommonWindow : Grid
    {
        private readonly Thickness BOTTOM_TOP_THICKNESS = new Thickness(0, 5, 0, 5);

        private Button btnDownload;

        private DataGrid dgData;

        private Panel pFilter;

        private TextBox txtFilterBy;

        /// <summary>
        /// Wynik działania operacji pobrania danych.
        /// </summary>
        private ResultList list;

        /// <summary>
        /// Właściwości okienka
        /// </summary>
        private CommonWindowProperties properties { get; set; }

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
            HorizontalAlignment = HorizontalAlignment.Stretch;

            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50.0) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50.0) });

            AddChild(CreateFilter());
            AddChild(CreateDataGrid());
            AddChild(CreateDownloadButton());
        }

        private int currentRow = 0;

        private void AddChild(UIElement child)
        {
            Children.Add(child);
            SetRow(child, currentRow++);
        }

        private UIElement CreateFilter()
        {
            pFilter = new DockPanel() 
            { 
                LastChildFill = true,
                Margin = BOTTOM_TOP_THICKNESS,
                VerticalAlignment = VerticalAlignment.Center
            };

            var lblFilterInfo = new Label()
            {
                Content = Application.Current.FindResource("lblFilterInfoContent")
            };
            pFilter.Children.Add(lblFilterInfo);
            txtFilterBy = new TextBox()
            {
                Height = 25,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            txtFilterBy.TextChanged += TxtFilterBy_TextChanged;
            pFilter.Children.Add(txtFilterBy);
            return pFilter;
        }

        private void TxtFilterBy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (list == null)
                return;

            var text = (sender as TextBox).Text;
            try
            {
                var newList = list.Filter(text);
                newList.Apply(dgData);
                txtFilterBy.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            catch (ArgumentException)
            {
                // błąd wyrażenia regularnego
                txtFilterBy.Background = new SolidColorBrush(Color.FromRgb(200, 0, 0));
            }
            catch (Exception ex)
            {
                // inny błąd - wyświetl szczegóły
                MessageBox.Show(ex.Message, "Błąd podczas filtrowania tabeli", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private UIElement CreateDownloadButton()
        {
            btnDownload = new Button()
            {
                Content = Application.Current.FindResource("btnDownloadContentDownload"),
                Margin = BOTTOM_TOP_THICKNESS
            };
            btnDownload.Click += BtnDownload_Click;
            return btnDownload;
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var run = true;
            do
            {
                btnDownload.IsEnabled = false;
                btnDownload.Content = Application.Current.FindResource("btnDownloadContentWait");
                
                txtFilterBy.Clear();

                try
                {
                    list = await properties.AsyncDownloadHandler();
                    list.Apply(dgData);
                    break;
                }
                catch (Exception ex)
                {
                    var msgBoxRet = MessageBox.Show(
                        $"Wystąpił błąd podczas pobierania danych:\r\n{ex.Message}. Czy ponowić próbę?", "Usterka!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    run = (msgBoxRet == MessageBoxResult.Yes);
                }
            } while (run);

            btnDownload.IsEnabled = true;
            btnDownload.Content = Application.Current.FindResource("btnDownloadContentDownload");
        }

        private UIElement CreateDataGrid()
        {
            dgData = new DataGrid();
            dgData.HorizontalAlignment = HorizontalAlignment.Stretch;
            dgData.IsReadOnly = true;
            
            return dgData;
        }

        /// <summary>
        /// Pobranie instancji klasy.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static CommonWindow Instance(CommonWindowProperties cwndprops)
        {
            if (cwndprops == null)
                throw new ArgumentNullException(nameof(cwndprops));

            return new CommonWindow() { properties = cwndprops };
        }

        /// <summary>
        /// Zaimplementowanie wspólnego okienka w podanym okienku.
        /// </summary>
        /// <param name="wnd"></param>
        public static void Implement(ICommon wnd)
        {
            var cwnd = Instance(wnd.GetProperties());
            wnd.Content = cwnd;

            // to jest niefajne
            // zrobiłbym abstrakcyjną klasę pośredniczączą (np. `MiddleWindow : Window, ICommon`), ale! 
            //   niestety XAML rozbija jedną klasę na dwa pliki, w której są partial class
            //   i musiałbym modyfikować dwa pliki XAML
            if (wnd is Window w)
            {
                w.ShowInTaskbar = false;
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

        }
    }
}
