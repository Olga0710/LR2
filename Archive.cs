using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Models
{
    public class Archive
    {
        public string _authorName { get; set; }
        public string _title { get; set; }
        public string _faculty { get; set; }
        public string _department { get; set; }
        public string _type { get; set; }
        public string _size { get; set; }
        public DateTime _creationDate { get; set; }
    }
}
