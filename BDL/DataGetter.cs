using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async static IEnumerable Subjects(int? parentId)
        {
            // await Downloader.DownloadXML("").Result;
            return null;
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
