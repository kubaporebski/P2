using System.Xml.Linq;

namespace BDL
{
    public class UnitRow
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string ParentId { get; private set; }

        public int Level { get; private set; }
        
        public UnitRow(XElement unit)
        {
            Id = unit.Element("id").Value;
            Name = unit.Element("name").Value;
            ParentId = unit.Element("parentId").Value;
            Level = int.Parse(unit.Element("level").Value);
        }
    }
}