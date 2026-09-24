using System.Text.Json.Serialization;

namespace LibraryForLaba1
{
    public class CEnemyTemplate
    {
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public EnemyIcon Icon { get; private set; }
        [JsonInclude]
        public int BaseLife { get; private set; } //проверка на полодительное значение и переполнение числа
        [JsonInclude]
        public double LifeModifier { get; private set; } //проверка на полодительное значение и переполнение числа
        [JsonInclude]
        public int BaseGold { get; private set; } //проверка на полодительное значение и переполнение числа
        [JsonInclude]
        public double GoldModifier { get; private set; } //проверка на полодительное значение и переполнение числа
        [JsonInclude]
        public double SpawnChance { get; private set; } //проверка на полодительное значение и переполнение числа

        public CEnemyTemplate(string name, EnemyIcon enemyIcon , int baseLife,
                              double lifeModifier, int baseGold,double goldModifier, double spawnChance
                             )
        {
            Name = name;
            Icon = enemyIcon;
            BaseLife = baseLife;
            LifeModifier = lifeModifier;
            BaseGold = baseGold;
            GoldModifier = goldModifier;
            SpawnChance = spawnChance;
        }
    }
}
