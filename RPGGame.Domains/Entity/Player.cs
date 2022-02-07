using RPGGame.Domains.Common;
using RPGGame.Domains.Helpers;
using System.Collections.Generic;

namespace RPGGame.Domains.Entity
{
    public class Player : Character
    {
        public int StaminaPoints { get; set; }
        public Multiplier Multipliers { get; set; }
        public List<ConsumerItem> ConsumerBackpack { get; set; }
    }
}
