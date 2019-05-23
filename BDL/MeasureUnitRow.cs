using System.Xml.Linq;

namespace BDL
{
    public class MeasureUnitRow
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public MeasureUnitRow(XElement unit)
        {
            Id = int.Parse(unit.Element("id").Value);
            Name = unit.Element("name").Value;
            Description = unit.Element("description").Value;
        }
    }
}