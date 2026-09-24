using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace LibraryForLaba1
{
    public class CEnemyTemplateObservableCollection
    {
        public ObservableCollection<CEnemyTemplate> Enemies { get; private set; }
        public CEnemyTemplateObservableCollection()
        {
            Enemies = new ObservableCollection<CEnemyTemplate>();
        }
        
        public void AddEnemy(CEnemyTemplate enemy)
        {
            Enemies.Add(enemy);
        }

        public CEnemyTemplate GetEnemyByName(string name)
        {
            
            foreach (CEnemyTemplate enemy in Enemies.ToList())
            {
                if (enemy.Name == name) return enemy;
            }

            return null;  
        }

        public CEnemyTemplate GetEnemyByIndex(int id)
        {
            if (Enemies.Count != 0)
            {
                return Enemies[id];
            }
            else { return null; }     
        }

        public void DeleteEnemyByName(string name)
        {
            CEnemyTemplate? enemy = Enemies.FirstOrDefault(e => e.Name == name);
            if (enemy != null) Enemies.Remove(enemy);
        }

        public ObservableCollection<string> GetListOfEnemyNames()
        {
            if ( Enemies.Count != 0)
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach(CEnemyTemplate enemy in Enemies.ToList())
                {
                    list.Add(enemy.Name);
                }
                return list;
            }
            else { return null; }
        }

        public void SaveToJson(string path)
        {
            string jsonString = JsonSerializer.Serialize(Enemies);
            File.WriteAllText(path, jsonString);
        }

        public void LoadFromJson(string path)
        {
            string jsonFromFile = File.ReadAllText(path);
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
                Enemies.Add(newEnemy);
            }
        }
    }
}
