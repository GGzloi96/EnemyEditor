using LibraryForLaba1;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EnemyEditor
{
    public partial class MainEnemyEditor : Window
    {
        private List<EnemyIcon> enemyIcons = new List<EnemyIcon>();
        private CEnemyTemplateObservableCollection enemies = new CEnemyTemplateObservableCollection();

        public MainEnemyEditor()
        {
            InitializeComponent();
            EnemyListBox.ItemsSource = enemies.Enemies;
        }

        private void LoadIconsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            dialog.Title = "Выберите папку с иконками";

            if (dialog.ShowDialog() == true)
            {
                string selectedPath = dialog.FolderName;
                LoadIconsFromFolder(selectedPath);
            }
        }

        private void LoadIconsFromFolder(string path)
        {
            enemyIcons.Clear();
            IconsListBox.Items.Clear(); //предыдущие иконки удаляются

            foreach (string file in Directory.GetFiles(path, "*.png"))
            {
                enemyIcons.Add(new EnemyIcon
                {
                    Name = System.IO.Path.GetFileName(file),
                    ImagePath = file
                });
            }

            DisplayIconsInListBox();
        }

        private void DisplayIconsInListBox()
        {
            foreach (EnemyIcon icon in enemyIcons)
            {
                Image image = new Image();

                image.Source = new BitmapImage(new Uri(icon.ImagePath));

                image.Height = 64;
                image.Width = 64;
                image.Margin = new Thickness(5);

                IconsListBox.Items.Add(image);
            }
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox iconHolder = sender as ListBox;

            if (iconHolder.SelectedItem is Image selectedImage && iconHolder.SelectedItem != null)
            {
                string iconName = System.IO.Path.GetFileName(selectedImage.Source.ToString());
                EnemyIconImage.Source = selectedImage.Source;
            }
        }

        private void AddEnemyInOC(object sender, RoutedEventArgs e)
        {
            string Name = EnemyNameTextBox.Text;
            int Baselife = int.Parse(EnemyBaseHealthTextBox.Text);
            double Modifylife = double.Parse(EnemyModifyHealfTextBox.Text);
            int BaseGold = int.Parse(EnemyBaseGoldTextBox.Text);
            double Modifygold= double.Parse(EnemyModifyGoldTextBox.Text);
            string IconName = System.IO.Path.GetFileName(EnemyIconImage.Source.ToString());
            string IconPath = EnemyIconImage.Source.ToString();
            double SpawnChance = double.Parse(EnemySpawnChanceTextBox.Text);
            EnemyIcon icon = new EnemyIcon(IconName,IconPath);

            CEnemyTemplate newEnemy = new CEnemyTemplate(name:Name,
                                                         enemyIcon:icon,
                                                         baseLife:Baselife,
                                                         lifeModifier:Modifylife,
                                                         baseGold:BaseGold,
                                                         goldModifier:Modifygold,
                                                         spawnChance:SpawnChance
                                                         );
            enemies.AddEnemy(newEnemy);
        }

        private void DeleteEnemyFromOC(object sender, RoutedEventArgs e)
        {
            string name = EnemyNameTextBox.Text;
            enemies.DeleteEnemyByName(name);
        }

        private void EnemyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnemyListBox.SelectedItems.Count != 0)
            {
                CEnemyTemplate enemy = EnemyListBox.SelectedItem as CEnemyTemplate;
                EnemyNameTextBox.Text = enemy.Name;
                EnemyBaseHealthTextBox.Text = Convert.ToString(enemy.BaseLife);
                EnemyModifyHealfTextBox.Text = Convert.ToString(enemy.LifeModifier);
                EnemyBaseGoldTextBox.Text = Convert.ToString(enemy.BaseGold);
                EnemyModifyGoldTextBox.Text = Convert.ToString(enemy.GoldModifier);
                EnemyIconImage.Source = new BitmapImage(new Uri(enemy.Icon.ImagePath));
                EnemySpawnChanceTextBox.Text = Convert.ToString(enemy.SpawnChance);

            }
            
            
        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "ListEnemy"; // Default file name
            dialog.DefaultExt = ".json"; // Default file extension
            dialog.Filter = "JSON файлы (*.json)|*.json";
            dialog.Title = "Выберите куда сохранять";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                enemies.SaveToJson(filename);
            }
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {
            enemies.Enemies.Clear();
            var dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";

            if (dialog.ShowDialog() == true)
            {
                enemies.LoadFromJson(dialog.FileName);
            }
        }

        private void EditEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ((CEnemyTemplate)EnemyListBox.SelectedItem).Name;
            enemies.DeleteEnemyByName(name);
            AddEnemyInOC(sender, e);
        }
    }
}