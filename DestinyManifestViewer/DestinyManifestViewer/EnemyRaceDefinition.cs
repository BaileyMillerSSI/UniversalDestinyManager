using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer
{
    public class EnemyRaceDefinition
    {
        public Displayproperties displayProperties { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }

    public class Displayproperties
    {
        public string icon { get; set; }
        public bool hasIcon { get; set; }
    }

}
