using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archive.Services
{
    public class SaxStrategy : IXmlSearchStrategy
    {
        public List<string> Search(string keyword, string attribute, string xmlPath)
        {
            List<string> results = new List<string>();
            using var reader = XmlReader.Create(xmlPath);

            while (reader.Read())
            {
              
                if (reader.NodeType == XmlNodeType.Element &&
                    reader.Name == "material")
                {
                    
                    string attrValue = reader.GetAttribute(attribute);
                    if (attrValue == null) continue;

                   
                    if (attrValue.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        string nodeXml = reader.ReadOuterXml();
                        results.Add(nodeXml);
                    }
                }
            }

            return results;
        }
    }
}
