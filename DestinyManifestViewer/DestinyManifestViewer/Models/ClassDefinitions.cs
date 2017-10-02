using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer.Models
{
    public class ClassDefinitions
    {
        public int classType { get; set; }
        public DescriptiveDisplayproperties displayProperties { get; set; }
        public Genderedclassnames genderedClassNames { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }

    public class Genderedclassnames
    {
        public string Male { get; set; }
        public string Female { get; set; }
    }

}
