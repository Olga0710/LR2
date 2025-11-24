using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Archive.Services
{
    internal class LinqStrategy : IXmlSearchStrategy
    {
        public List<string> Search(string keyword, string attribute, string xmlPath)
        {
            var result = new List<string>();

            var doc = XDocument.Load(xmlPath);

            foreach (var mat in doc.Root.Elements("material"))
            {
                var attr = mat.Attribute(attribute);
                if (attr == null) continue;

                if (attr.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(mat.ToString());
                }
            }

            return result;
        }
    }
}
