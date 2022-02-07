using RPGGame.Domains.Helpers;

namespace RPGGame.Domains.Entity
{
    public class Building
    {
        public int BuildingID { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public Requirement Requirement { get; set; }
        public Multiplier Multiplier { get; set; }
    }
}
