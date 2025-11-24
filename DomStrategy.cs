using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archive.Services
{
    public class DomStrategy : IXmlSearchStrategy
    {
        public List<string> Search(string keyword, string attribute, string xmlPath)
        {
            var result = new List<string>();

            var doc = new XmlDocument();
            doc.Load(xmlPath);

            var nodes = doc.GetElementsByTagName("material");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes == null) continue;

                var attr = node.Attributes[attribute];
                if (attr == null) continue;

                if (attr.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(node.OuterXml);
                }
            }

            return result;
        }
    }
}
