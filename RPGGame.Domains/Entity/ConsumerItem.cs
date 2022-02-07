using RPGGame.Domains.Common;

namespace RPGGame.Domains.Entity
{
    public class ConsumerItem : Item
    {
        public int Quantity { get; set; }
        public int HPRestore { get; set; }
        public int SPRestore { get; set; }
        public ConsumerType Type { get; set; }
    }
}
