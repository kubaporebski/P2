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
        public Model InputData { get; private set; }

        public WndData(Model input)
        {
            InitializeComponent();
            InputData = input;
            CommonWindow.Implement(this);

            Title = $"Dane dla zmiennej: {InputData.Variable.N1}" + (string.IsNullOrEmpty(InputData.Variable.N1) ? "" : $" / {InputData.Variable.N2}");
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
            var territorialUnitId = InputData.TerritorialUnit?.Id;
            return ResultList.Convert<DataResultList>(
                DataGetter.DataByVariable(int.Parse(InputData.Variable.Id), territorialUnitId, InputData.YearFrom, InputData.YearTo, InputData.Level, 100));
        }
    }

    public class DataResultList : ResultList
    {
        protected override void ApplyImpl(DataGrid dg)
        {
            dg.Columns[0].Header = "Id zmiennej";
            dg.Columns[1].Header = "Id jednostki miary";
            dg.Columns[2].Header = "Id agregatu";
            dg.Columns[3].Header = "Id jednostki terytorialnej";
            dg.Columns[4].Header = "Nazwa jednostki terytorialnej";
            dg.Columns[5].Header = "Rok";
            dg.Columns[6].Header = "Wartość";
            dg.Columns[7].Header = "Id atrybutu";
        }

        protected override bool FilterImpl(object item, string text)
        {
            var currentItem = item as UnitData;
            return RegexChecker.Instance(text).Check(currentItem.Name, currentItem.Value, currentItem.Year.ToString());
        }
    }

    /// <summary>
    /// Model danych dla danych.
    /// Pozbierane wymagane informacje, aby móc pobrać konkretne dane.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Dane zmiennej BDL.
        /// </summary>
        public VariablesRow Variable { get; set; }

        /// <summary>
        /// Dane jednostki terytorialnej.
        /// </summary>
        public UnitRow TerritorialUnit { get; set; }

        /// <summary>
        /// Poziom pobierania danych dla jednostek terytorialnych.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Rok od.
        /// </summary>
        public int YearFrom { get; set; }

        /// <summary>
        /// Rok do.
        /// </summary>
        public int YearTo { get; set; }
    }
}
