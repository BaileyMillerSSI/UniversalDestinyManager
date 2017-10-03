using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDestinyManager.Models
{
    public class GetCharacterResponse
    {
        public CharactersResponse Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Messagedata MessageData { get; set; }
    }


    public class Rootobject
    {
        
    }

    public class CharactersResponse
    {
        public Characters characters { get; set; }
        public Itemcomponents itemComponents { get; set; }
    }

    public class Characters
    {
        public Dictionary<String, CharacterData> data { get; set; }
        public int privacy { get; set; }
    }

    public class CharacterData
    {
        public string membershipId { get; set; }
        public int membershipType { get; set; }
        public string characterId { get; set; }
        public DateTime dateLastPlayed { get; set; }
        public string minutesPlayedThisSession { get; set; }
        public string minutesPlayedTotal { get; set; }
        public int light { get; set; }
        public Dictionary<String, String> stats { get; set; }
        public long raceHash { get; set; }
        public long genderHash { get; set; }
        public long classHash { get; set; }
        public int raceType { get; set; }
        public int classType { get; set; }
        public int genderType { get; set; }
        public string emblemPath { get; set; }
        public string emblemBackgroundPath { get; set; }
        public long emblemHash { get; set; }
        public Levelprogression levelProgression { get; set; }
        public int baseCharacterLevel { get; set; }
        public float percentToNextLevel { get; set; }
    }
    
    public class Levelprogression
    {
        public long progressionHash { get; set; }
        public int dailyProgress { get; set; }
        public int dailyLimit { get; set; }
        public int weeklyProgress { get; set; }
        public int weeklyLimit { get; set; }
        public int currentProgress { get; set; }
        public int level { get; set; }
        public int levelCap { get; set; }
        public int stepIndex { get; set; }
        public int progressToNextLevel { get; set; }
        public int nextLevelAt { get; set; }
    }

    public class Itemcomponents
    {
    }

}
