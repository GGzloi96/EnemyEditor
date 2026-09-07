using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForLaba1
{
    internal class EnemyIcon
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public void LoadIconsFromFolder(string path)
        {
            //фильтр расширения изображения
            string filter = "*.png";
            //получение массива строк содержащих пути до изображений
            string[] files = Directory.GetFiles(path, filter);
            //перебор всех полученных путей
            //в file содержится путь до изображения с расширением .png
            foreach (string file in files)
            {
                enemyIcons.Add(
                new EnemyIcon
                {
                    // получение имени файла с расширением
                    Name = System.IO.Path.GetFileName(file),
                    // получение полного пути до файла
                    ImagePath = file
                }
                );
            }
        }
    }
}
