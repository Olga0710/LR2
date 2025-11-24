using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Archive.Services
{
    internal class XslTransformer
    {
        public static void Transform(string xml, string xsl, string outputHtml)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load(xsl);
            xslt.Transform(xml, outputHtml);
        }
    }
}
