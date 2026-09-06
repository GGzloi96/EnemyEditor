using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
