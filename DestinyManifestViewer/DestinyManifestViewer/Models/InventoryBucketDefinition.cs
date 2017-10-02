using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer.Models
{
    public class InventoryBucketDefinition
    {
        public DescriptiveDisplayproperties displayProperties { get; set; }
        public int scope { get; set; }
        public int category { get; set; }
        public int bucketOrder { get; set; }
        public int itemCount { get; set; }
        public int location { get; set; }
        public bool hasTransferDestination { get; set; }
        public bool enabled { get; set; }
        public bool fifo { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }

}
