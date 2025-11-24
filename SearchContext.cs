using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Services
{
    public class SearchContext
    {
        private IXmlSearchStrategy _strategy;

        public void SetStrategy(IXmlSearchStrategy strategy)
        {
            this._strategy = strategy;
        }

        public List<string> Search(string keyword, string attribute, string xmlPath)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Search strategy not set");
            }
            return _strategy.Search(keyword, attribute, xmlPath);
        }
    }
}
