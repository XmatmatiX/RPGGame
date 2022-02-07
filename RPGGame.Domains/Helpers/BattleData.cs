using RPGGame.Domains.Entity;
using System.Collections.Generic;

namespace RPGGame.Domains.Helpers
{
    public class BattleData
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AtackPoints { get; set; }
        public int ArmorPoints { get; set; }
        public List<ConsumerItem> ConsumerLoot { get; set; }
    }
}
