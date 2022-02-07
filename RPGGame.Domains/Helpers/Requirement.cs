namespace RPGGame.Domains.Helpers
{
    public class Requirement
    {
        public int RequirementWood { get; set; }
        public int RequirementStone { get; set; }
        public int RequirementIron { get; set; }
        public int RequirementWater { get; set; }

        public Requirement(int wood, int stone, int iron, int water)
        {
            RequirementWood = wood;
            RequirementStone = stone;
            RequirementIron = iron;
            RequirementWater = water;
        }

        public bool IsZero()
        {
            bool zero = true;
            if (RequirementIron >0)
            {
                zero = false;
            }
            else if (RequirementWood > 0)
            {
                zero = false;
            }
            else if (RequirementWater > 0)
            {
                zero = false;
            }
            else if (RequirementStone > 0)
            {
                zero = false;
            }

            return zero;
        }
    }
}
