using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Xml.XPath;

namespace BDL
{
    public class DataGetter
    {
        #region Randomizer

        /// <summary>
        /// Testowa funkcja zwracająca losowe dane.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "RandomizerFillRow")]
        public static IEnumerable Randomizer(int count)
        {
            ArrayList resultCollection = new ArrayList();
            for (int i = 1; i <= count; i++)
                resultCollection.Add(new RandomizerRow(i));

            return resultCollection;
        }

        public static void RandomizerFillRow(object obRow, out int Id, out double Value)
        {
            var row = obRow as RandomizerRow;
            Id = row.Id;
            Value = row.Value;
        }

        #endregion

        #region RequestLog

        /// <summary>
        /// Procedurka pozwalająca na wyświetlenie logów żądań do BDL.
        /// Każdy log składa się z daty oraz adresu URL żądania.
        /// 
        /// Funkcja wyświetla log w okienku Messages w aplikacji SSMS.
        /// </summary>
        [SqlProcedure]
        public static void RequestLog()
        {
            Downloader.Logs.ForEach(log =>
            {
                SqlContext.Pipe?.Send(log?.ToString());
            });
        }

        #endregion

        #region Subjects

        /// <summary>
        /// Funkcja pobierająca listę top-level tematów .
        /// Jest to pkt 'Dane dla jednej zmiennej i wielu jednostek terytorialnych ' w dokumentacji na stronie 
        ///     https://bdl.stat.gov.pl/api/v1/home/tutorial?theme=Default
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "SubjectsFillRow")]
        public static IEnumerable Subjects(string parentId, int pageSize)
        {
            var uri = "https://bdl.stat.gov.pl/api/v1/subjects?format=xml";
            if (!string.IsNullOrEmpty(parentId))
                uri += $"&parent-id={parentId}";
            if (pageSize > 0)
                uri += $"&page-size={pageSize}";

            return FlattenPages(uri, docu =>
            {
                return docu.Descendants("subject").Select(subject => new SubjectRow(subject));
            });
        }

        public static void SubjectsFillRow(object obRow, out string Id, out string Name, out bool HasVariables, out string Children)
        {
            var row = obRow as SubjectRow;
            Id = row.Id;
            Name = row.Name;
            HasVariables = row.HasVariables;
            Children = row.Children;
        }

        #endregion

        #region Variables

        /// <summary>
        /// Funkcja pobierająca listę zmiennych dla danego tematu.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "VariablesFillRow")]
        public static IEnumerable Variables(string subjectId, int pageSize)
        {
            var uri = $"https://bdl.stat.gov.pl/api/v1/variables?format=xml&subject-id={subjectId}";
            if (pageSize > 0)
                uri += $"&page-size={pageSize}";

            return FlattenPages(uri, docu =>
            {
                return docu.Descendants("variable").Select(variable => new VariablesRow(variable));
            });
        }

        public static void VariablesFillRow(object obRow, out string Id, out string SubjectId, out string N1, out string N2, out int MeasureUnitId, out string MeasureUnitName)
        {
            var row = obRow as VariablesRow;
            Id = row.Id;
            SubjectId = row.SubjectId;
            N1 = row.N1;
            N2 = row.N2;
            MeasureUnitId = row.MeasureUnitId;
            MeasureUnitName = row.MeasureUnitName;
        }

        #endregion

        #region Measures

        /// <summary>
        /// Funkcja pobierająca listę wszystkich jednostek miary.
        /// </summary>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "MeasuresFillRow")]
        public static IEnumerable Measures()
        {
            var uri = "https://bdl.stat.gov.pl/api/v1/measures?format=xml";

            return Downloader.DownloadXML(uri).Descendants("measureUnit").Select(unit => new MeasureUnitRow(unit)).ToList();
        }

        public static void MeasuresFillRow(object obRow, out int Id, out string Name, out string Description)
        {
            var row = obRow as MeasureUnitRow;
            Id = row.Id;
            Name = row.Name;
            Description = row.Description;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Funkcja pobierająca listę atrybutów.
        /// </summary>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "AttributesFillRow")]
        public static IEnumerable Attributes()
        {
            var uri = "https://bdl.stat.gov.pl/api/v1/attributes?format=xml";

            return Downloader.DownloadXML(uri).Descendants("attribute").Select(attr => new AttributeRow(attr)).ToList();
        }

        public static void AttributesFillRow(object obRow, out int Id, out string Name, out string Symbol, out string Description)
        {
            var row = obRow as AttributeRow;
            Id = row.Id;
            Name = row.Name;
            Symbol = row.Symbol;
            Description = row.Description;
        }

        #endregion

        #region DataByVariable

        /// <summary>
        /// Pobranie danych wg przekazanych parametrów.
        /// 
        /// </summary>
        /// <param name="variableId">id zmiennej</param>
        /// <param name="unitParentId">id jednostki terytorialnej</param>
        /// <param name="yearFrom">rok od</param>
        /// <param name="yearTo">rok do</param>
        /// <param name="pageSize">rozmiar strony</param>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "DataByVariableFillRow")]
        public static IEnumerable DataByVariable(int variableId, string unitParentId, int yearFrom, int yearTo, int pageSize)
        {
            var uri = $"https://bdl.stat.gov.pl/api/v1/data/by-variable/{variableId}?format=xml";
            for (int yr = yearFrom; yr <= yearTo; yr++)
                uri += $"&year={yr}";
            uri += $"&page-size={pageSize}";

            if (unitParentId != null)
                uri += $"unit-parent-id={unitParentId}";

            return FlattenPages(uri, docu => UnitData.FromXML(docu));
        }

        public static void DataByVariableFillRow(object obUnitData, out int VariableId, out int MeasureUnitId, out int AggregateId, out string Id, out string Name, out int Year, out string Value, out int AttributeId)
        {
            var row = obUnitData as UnitData;
            VariableId = row.VariableId;
            MeasureUnitId = row.MeasureUnitId;
            AggregateId = row.AggregateId;
            Id = row.Id;
            Name = row.Name;
            Year = row.Year;
            Value = row.Value;
            AttributeId = row.AttributeId;
        }

        #endregion

        #region Units

        /// <summary>
        /// Pobranie listy wszystkich jednostek terytorialnych
        /// </summary>
        /// <returns></returns>
        [SqlFunction(FillRowMethodName = "UnitsFillRow")]
        public static IEnumerable Units(int pageSize)
        {
            var uri = "https://bdl.stat.gov.pl/api/v1/units?format=xml";
            uri += $"&page-size={pageSize}";

            return FlattenPages(uri, docu =>
            {
                return docu.Descendants("unit").Select(u => new UnitRow(u));
            });
        }

        public static void UnitsFillRow(object obRow, out string Id, out string Name, out string ParentId, out int Level)
        {
            var unit = obRow as UnitRow;
            Id = unit.Id;
            Name = unit.Name;
            ParentId = unit.ParentId;
            Level = unit.Level;
        }

        #endregion

        #region Funkcje narzędziowe

        private static IEnumerable FlattenPages<T>(string uri, Func<XDocument, IEnumerable<T>> callback)
        {
            if (!uri.Contains("&page="))
                uri += "&page=0";
            
            var rows = new List<T>();
            do
            {
                var docu = Downloader.DownloadXML(uri);
                rows.AddRange(callback.Invoke(docu));

                var uriNext = docu.XPathSelectElement("//links/next");
                if (uriNext == null)
                    break;

                uri = uriNext.Value;
            } while (true);

            return rows;
        }

        #endregion
    }
}
