using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer.Models
{
    public class DestinyActivityDefinition
    {
        public DescriptiveDisplayproperties displayProperties { get; set; }
        public string releaseIcon { get; set; }
        public int releaseTime { get; set; }
        public int activityLevel { get; set; }
        public int completionUnlockHash { get; set; }
        public int activityLightLevel { get; set; }
        public long destinationHash { get; set; }
        public long placeHash { get; set; }
        public long activityTypeHash { get; set; }
        public int tier { get; set; }
        public string pgcrImage { get; set; }
        public Reward[] rewards { get; set; }
        public object[] modifiers { get; set; }
        public bool isPlaylist { get; set; }
        public object[] challenges { get; set; }
        public object[] optionalUnlockStrings { get; set; }
        public bool inheritFromFreeRoam { get; set; }
        public bool suppressOtherRewards { get; set; }
        public object[] playlistItems { get; set; }
        public Matchmaking matchmaking { get; set; }
        public long directActivityModeHash { get; set; }
        public int directActivityModeType { get; set; }
        public long[] activityModeHashes { get; set; }
        public int[] activityModeTypes { get; set; }
        public bool isPvP { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }

    public class DescriptiveDisplayproperties
    {
        public string description { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public bool hasIcon { get; set; }
    }

    public class Matchmaking
    {
        public bool isMatchmade { get; set; }
        public int minParty { get; set; }
        public int maxParty { get; set; }
        public int maxPlayers { get; set; }
        public bool requiresGuardianOath { get; set; }
    }

    public class Reward
    {
        public Rewarditem[] rewardItems { get; set; }
    }

    public class Rewarditem
    {
        public long itemHash { get; set; }
        public int quantity { get; set; }
    }

}
