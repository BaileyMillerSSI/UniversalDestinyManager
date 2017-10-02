using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer.Models
{
    public class GenderDefinition
    {
        public int genderType { get; set; }
        public DescriptiveDisplayproperties displayProperties { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }

}
