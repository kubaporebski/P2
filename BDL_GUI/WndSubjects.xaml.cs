﻿using BDL;
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
    /// Interaction logic for WndSubjects.xaml
    /// </summary>
    public partial class WndSubjects : Window, ICommon
    {
        public string ParentSubjectId { get; set; }

        public WndSubjects()
        {
            InitializeComponent();
            CommonWindow.Implement(this);
        }

        public CommonWindowProperties GetProperties()
        {
            return new CommonWindowProperties()
            {
                DownloadHandler = Download,
                LoadedHandler = OnLoad
            };
        }

        private ResultList Download()
        {
            return ResultList.Convert<SubjectResultList>(DataGetter.Subjects(ParentSubjectId, 100));
        }

        private void OnLoad(CommonWindow window)
        {
            window.DgData.MouseDoubleClick += DgData_MouseDoubleClick;
        }

        private void DgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dg = sender as DataGrid;
            var item = dg.CurrentItem as SubjectRow;
            Window nextWindow;

            if (item.HasVariables)
            {
                //MessageBox.Show("Brak tematów podrzędnych dla wybranego tematu", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                // return;
                nextWindow = new WndVariables()
                {
                    ParentSubjectId = item.Id,
                    Title = $"Zmienne dla tematu: {item.Name}"
                };
            }
            else
            {
                nextWindow = new WndSubjects()
                {
                    ParentSubjectId = item.Id,
                    Title = $"Tematy podrzędne dla: {item.Name}"
                };
            }

            nextWindow.ShowDialog();
        }
    }


    public class SubjectResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            if (dg.Columns.Count == 0)
                return;

            dg.Columns[1].Header = "Nazwa";

            dg.Columns[2].Header = "Czy liść";

            dg.Columns[3].Header = "Id tematów podrzędnych";
        }

        protected override bool FilterImpl(object item, string text)
        {
            var currentItem = item as SubjectRow;
            var checker = RegexChecker.Instance(text);
            return checker.Check(currentItem.Id, currentItem.Name);
        }
    }
}
