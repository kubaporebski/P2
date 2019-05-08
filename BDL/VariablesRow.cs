using System.Xml.Linq;

namespace BDL
{
    public class VariablesRow
    {
        public string Id { get; private set; }

        public string SubjectId { get; private set; }

        public string N1 { get; private set; }

        public string N2 { get; private set; }

        public int MeasureUnitId { get; private set; }

        public string MeasureUnitName { get; private set; }

        public VariablesRow(XElement variable)
        {
            Id = variable.Element("Id").Value;
            SubjectId = variable.Element("subjectId").Value;
            N1 = variable.Element("n1").Value;
            N2 = variable.Element("n2").Value;
            MeasureUnitId = int.Parse(variable.Element("measureUnitId").Value);
            MeasureUnitName = variable.Element("measureUnitName").Value;
        }
    }
}