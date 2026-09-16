using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryForLaba1
{
    public class CEnemyTemplateObservableCollection: ObservableCollection<CEnemyTemplate>
    {
        public ObservableCollection<CEnemyTemplate> enemies { get; private set; }
        public CEnemyTemplateObservableCollection()
        {
            enemies = new ObservableCollection<CEnemyTemplate>();
        }
        
        public void AddEnemy(CEnemyTemplate enemy)
        {
            enemies.Add(enemy);
        }

        public CEnemyTemplate GetEnemyByName(string name)
        {
            
            foreach (CEnemyTemplate enemy in enemies.ToList())
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

            foreach (CEnemyTemplate enemy in enemies.ToList())
            {
                if (enemy.Name == name) enemies.Remove(enemy);
            }
        }

        public ObservableCollection<string> GetListOfEnemyNames()
        {
            if ( enemies.Count != 0)
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach(CEnemyTemplate enemy in enemies.ToList())
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
            ObservableCollection<CEnemyTemplate> getedEnemy = new ObservableCollection<CEnemyTemplate>();
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                string name = element.GetProperty("Name").GetString();

                JsonElement JsonIcon = element.GetProperty("Icon");
                EnemyIcon icon = JsonSerializer.Deserialize<EnemyIcon>(JsonIcon);

                int baseLife = element.GetProperty("BaseLife").GetInt32();
                double lifeModifier = element.GetProperty("LifeModifier").GetDouble();
                int baseGold = element.GetProperty("BaseGold").GetInt32();
                double goldModifier = element.GetProperty("GoldModifier").GetDouble();
                double spawnChance = element.GetProperty("SpawnChance").GetDouble();

                CEnemyTemplate newEnemy = new CEnemyTemplate(name, icon, baseLife, lifeModifier,
                                                         baseGold, goldModifier, spawnChance);
                enemies.Add(newEnemy);
            }
        }
    }
}
