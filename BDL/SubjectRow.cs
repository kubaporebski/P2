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

        public SubjectRow(XElement xSubject)
        {
            Id = xSubject.Element("id").Value;
            Name = xSubject.Element("name").Value;
            HasVariables = bool.Parse(xSubject.Element("hasVariables").Value);
            
            Children = string.Join(",", xSubject.XPathSelectElements("children/id").Select(xChild => xChild.Value).ToList());
        }
    }
}
