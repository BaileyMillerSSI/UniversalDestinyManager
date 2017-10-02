using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDestinyManager.Models
{
    public enum PlatformType
    {
        None = 0,
        TigerXbox = 1,
        TigerPsn = 2,
        TigerBlizzard = 4,
        TigerDemon = 10,
        BungieNext = 254,
        All = -1
    }
}
