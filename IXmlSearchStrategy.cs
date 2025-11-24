using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Services
{
    public interface IXmlSearchStrategy
    {
        List<string> Search(string keyword, string attribute, string xmlPath);
    }
}
