using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDL
{
    public class UnitData
    {
        public int VariableId { get; private set; }
        
        public int MeasureUnitId { get; private set; }

        public int AggregateId { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public int Year { get; private set; }

        public string Value { get; private set; }

        public int AttributeId { get; private set; }

        public static List<UnitData> FromXML(XDocument docu)
        {
            var result = new List<UnitData>();

            var variableId = int.Parse(docu.Descendants("variableId").Single().Value);
            var measureUnitId = int.Parse(docu.Descendants("measureUnitId").Single().Value);
            var aggregateId = int.Parse(docu.Descendants("aggregateId").Single().Value);

            return docu.Descendants("unitData").SelectMany(ud =>
            {
                var id = ud.Element("id").Value;
                var name = ud.Element("name").Value;

                return ud.Descendants("yearVal").Select(yv =>
                {
                    var row = new UnitData();
                    row.VariableId = variableId;
                    row.MeasureUnitId = measureUnitId;
                    row.AggregateId = aggregateId;
                    row.Id = id;
                    row.Name = name;
                    row.Year = int.Parse(yv.Element("year").Value);
                    row.Value = yv.Element("val").Value;
                    row.AttributeId = int.Parse(yv.Element("attrId").Value);
                    return row;
                });
            }).ToList();
        }
    }
}
