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
                EnemyIconImage.Source = selectedImage.Source;
            }
        }

        private void AddEnemyInOC(object sender, RoutedEventArgs e) //EXCEPTION: сделать поля непустыми 
        {
            CEnemyTemplate newEnemy = CreateEnemyFromData();
            if (newEnemy != null) enemies.AddEnemy(newEnemy);
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
                EnemyBaseHealthTextBox.Text = enemy.BaseLife.ToString();
                EnemyModifyHealfTextBox.Text = enemy.LifeModifier.ToString();
                EnemyBaseGoldTextBox.Text = enemy.BaseGold.ToString();
                EnemyModifyGoldTextBox.Text = enemy.BaseGold.ToString();
                EnemyIconImage.Source = new BitmapImage(new Uri(enemy.Icon.ImagePath));
                EnemySpawnChanceTextBox.Text = enemy.SpawnChance.ToString();
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
            if (EnemyListBox.SelectedItem is not CEnemyTemplate oldEnemy)
            {
                MessageBox.Show("Выберите врага");
                return;
            }

            CEnemyTemplate updatedEnemy = CreateEnemyFromData();

            if (updatedEnemy == null) { return; }

            int index = enemies.Enemies.IndexOf((CEnemyTemplate)EnemyListBox.SelectedItem);
            enemies.Enemies[index] = updatedEnemy;  

            //string name = ((CEnemyTemplate)EnemyListBox.SelectedItem).Name;
            //enemies.DeleteEnemyByName(name);
            //AddEnemyInOC(sender, e);
        }

        private CEnemyTemplate? CreateEnemyFromData()
        {
            #region Exceptions
            if (string.IsNullOrEmpty(EnemyNameTextBox.Text)) { MessageBox.Show("Имя пустое"); return null; }

            if (!int.TryParse(EnemyBaseHealthTextBox.Text, out int baseHealth)) { MessageBox.Show("Базовое здоровье пустое"); return null; }

            if (!double.TryParse(EnemyModifyHealfTextBox.Text, out double healthModif)) { MessageBox.Show("Модификатор здоровья пустой"); return null; }

            if (!int.TryParse(EnemyBaseGoldTextBox.Text, out int baseGold)) { MessageBox.Show("Базовое золото пустое"); return null; }

            if (!double.TryParse(EnemyModifyGoldTextBox.Text, out double goldModif)) { MessageBox.Show("Модификатор золота пустой"); return null; }

            if (!double.TryParse(EnemySpawnChanceTextBox.Text, out double spawnChance)) { MessageBox.Show("Шанс спавна пустой"); return null; }

            if (EnemyIconImage.Source == null) { MessageBox.Show("Выберите иконку"); return null; }
            #endregion

            string IconPath = EnemyIconImage.Source.ToString();
            string IconName = Path.GetFileName(IconPath);
            EnemyIcon enemyIcon = new EnemyIcon(IconName, IconPath);

            return new CEnemyTemplate(EnemyNameTextBox.Text, enemyIcon, baseHealth, healthModif,
                baseGold, goldModif, spawnChance);
        }
    }
}