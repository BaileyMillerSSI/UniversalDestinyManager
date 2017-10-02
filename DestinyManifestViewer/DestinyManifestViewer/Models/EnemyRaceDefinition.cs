using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer.Models
{

    public class EnemyRaceDefinition
    {
        public DescriptiveDisplayproperties displayProperties { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }

        public void ProcessDisplayData()
        {
            if (displayProperties != null && displayProperties.hasIcon)
            {
                displayProperties.name = ToProperCase(ExtractNameFromIcon().ToLower());
            }
        }

        private string ToProperCase(string badString)
        {
            var builder = new StringBuilder();
            builder.Append(badString[0].ToString().ToUpper());
            for (int i = 1; i < badString.Length; i++)
            {
                builder.Append(badString[i]);
            }
            return builder.ToString();
        }

        private string ExtractNameFromIcon()
        {
            var lastSlashIndex = displayProperties.icon.LastIndexOf("/")+1;
            var dotIndex = displayProperties.icon.IndexOf(".");

            var name = Slice(displayProperties.icon, lastSlashIndex, dotIndex);
            return name;
        }

        private string Slice(string source, int start, int end)
        {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }
    }

}
