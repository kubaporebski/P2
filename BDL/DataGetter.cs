using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace BDL
{
    public class DataGetter
    {
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

        [SqlFunction]
        public static int SetClientId(string clientId)
        {
            Downloader.SetClientId(clientId);
            return 1;
        }

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
            string uri = "https://bdl.stat.gov.pl/api/v1/subjects?format=xml";

            if (!string.IsNullOrEmpty(parentId))
                uri += $"&parent-id={parentId}";

            if (pageSize > 0)
                uri += $"&page-size={pageSize}";

            XDocument docu = Downloader.DownloadXML(uri);
            ArrayList resultCollection = new ArrayList();
            foreach (var subject in docu.Descendants("subject"))
            {
                resultCollection.Add(new SubjectRow(subject));
            }
            return resultCollection;
        }

        public static void SubjectsFillRow(object obRow, out string Id, out string Name, out bool HasVariables, out string Children)
        {
            var row = obRow as SubjectRow;
            Id = row.Id;
            Name = row.Name;
            HasVariables = row.HasVariables;
            Children = row.Children;
        }

        public static IEnumerable Variables(int subjectId)
        {
            return null;
        }

        public static IEnumerable DataByVariable(int variableId)
        {
            return null;
        }

    }

    public class RandomizerRow
    {
        private static readonly Random rnd = new Random();

        public int Id;
        public double Value = rnd.NextDouble();

        public RandomizerRow(int id)
        {
            this.Id = id;
        }
    }
}
