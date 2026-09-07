namespace LibraryForLaba1
{
    internal class CEnemyTemplateList
    {
        List<CEnemyTemplate> enemies;
        public CEnemyTemplateList()
        {
            enemies = new List<CEnemyTemplate>();
        }
        
        public void AddEnemy(string name, string iconName, int baseLife,
                             double lifeModifier, int baseGold,
                             double goldModifier, double spawnChance)
        {
            CEnemyTemplate newEnemy = new CEnemyTemplate(name,iconName,baseLife,lifeModifier,
                                                         baseGold,goldModifier,spawnChance);
            enemies.Add(newEnemy);
        }

        public CEnemyTemplate GetEnemyByName(string name)
        {
            
            foreach (CEnemyTemplate enemy in enemies)
            {
                if (enemy.Name == name) return enemy;
            }

            return null;
            
        }

        public CEnemyTemplate GetEnemyByIndex(int id)
        {
            if (enemies.Count != 0)
            {
                return enemies[id];
            }
            else { return null; }
            
        }

        public void DeleteEnemyByName(string name)
        {

            foreach (CEnemyTemplate enemy in enemies)
            {
                if (enemy.Name == name) enemies.Remove(enemy);
            }


        }

        public List<string> GetListOfEnemyNames()
        {
            if ( enemies.Count != 0)
            {
                List<string> list = new List<string>();
                foreach(CEnemyTemplate enemy in enemies)
                {
                    list.Add(enemy.Name);
                }
                return list;
            }
            else { return null; }
        }

        public void SaveToJson(string path)
        {
            string jsonString = JsonSerializer.Serialize(enemies);
            File.WriteAllText(path, jsonString);

        }

        public void LoadFromJson(string path)
        {
            string jsonFromFile = File.ReadAllText(path);
            List<CEnemyTemplate> getedEnemy = new List<CEnemyTemplate>();
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                string name = element.GetProperty("Name").GetString();
                string iconName = element.GetProperty("IconName").GetString();
                int baseLife = element.GetProperty("BaseLife").GetInt32();
                double lifeModifier = element.GetProperty("LifeModifier").GetDouble();
                int baseGold = element.GetProperty("BaseGold").GetInt32();
                double goldModifier = element.GetProperty("GoldModifier").GetDouble();
                double spawnChance = element.GetProperty("SpawnChance").GetDouble();

                CEnemyTemplate newEnemy = new CEnemyTemplate(name, iconName, baseLife, lifeModifier,
                                                         baseGold, goldModifier, spawnChance);
                enemies.Add(newEnemy);
            }

        }


    }
}
