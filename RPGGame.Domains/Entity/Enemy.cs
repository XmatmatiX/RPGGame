using RPGGame.Domains.Common;

namespace RPGGame.Domains.Entity
{
    public class Enemy : Character
    {
        public ConsumerItem[] Loot { get; set; }
        public bool CanRunAway { get; set; }
    }
}
