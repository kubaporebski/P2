using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BDL
{
    public class SubjectRow
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public bool HasVariables { get; private set; }

        public string Children { get; private set; }

        public SubjectRow(XElement subject)
        {
            Id = subject.Element("id").Value;
            Name = subject.Element("name").Value;
            HasVariables = bool.Parse(subject.Element("hasVariables").Value);
            
            Children = string.Join(",", subject.XPathSelectElements("children/id").Select(xChild => xChild.Value).ToList());
        }
    }
}
