namespace LibraryForLaba1
{
    public class CEnemyTemplate
    {
        public string Name { get; private set; }
        public string IconName { get; private set; }
        public int BaseLife { get; private set; }
        public double LifeModifier { get; private set; }
        public int BaseGold { get; private set; }
        public double GoldModifier { get; private set; }
        public double SpawnChance { get; private set; }

        public CEnemyTemplate(string name, string iconName, int baseLife,
                              double lifeModifier, int baseGold,double goldModifier, double spawnChance
                             )
        {
            Name = name;
            IconName = iconName;
            BaseLife = baseLife;
            LifeModifier = lifeModifier;
            BaseGold = baseGold;
            GoldModifier = goldModifier;
            SpawnChance = spawnChance;
        }
    }
}
