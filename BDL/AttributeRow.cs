using System.Xml.Linq;

namespace BDL
{
    public class AttributeRow
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Symbol { get; private set; }

        public string Description { get; private set; }

        public AttributeRow(XElement attr)
        {
            Id = int.Parse(attr.Element("id").Value);
            Name = attr.Element("name").Value;
            Symbol = attr.Element("symbol").Value;
            Description = attr.Element("description").Value;
        }
    }
}