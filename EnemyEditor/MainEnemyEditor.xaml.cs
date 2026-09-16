using LibraryForLaba1;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Text.Json;
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
            EnemyListBox.ItemsSource = enemies.enemies;
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
            string filter = "*.png";
            string[] files = Directory.GetFiles(path, filter);

            foreach (string file in files)
            {
                EnemyIcon icon = new EnemyIcon();

                icon.Name = System.IO.Path.GetFileName(file);
                icon.ImagePath = file;

                enemyIcons.Add(icon);
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
            string name = EnemyNameTextBox.Text;
            int baselife = int.Parse(EnemyBaseHealthTextBox.Text);
            int baseGold = int.Parse(EnemyBaseGoldTextBox.Text);
            string iconName = System.IO.Path.GetFileName(EnemyIconImage.Source.ToString());
            string iconPath = EnemyIconImage.Source.ToString();
            EnemyIcon icon = new EnemyIcon(iconName,iconPath);

            CEnemyTemplate newEnemy = new CEnemyTemplate(name:name,
                                                         enemyIcon:icon,
                                                         baseLife:baselife,
                                                         lifeModifier:1,
                                                         baseGold:baseGold,
                                                         goldModifier:1,
                                                         spawnChance:1 
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
                EnemyBaseGoldTextBox.Text = Convert.ToString(enemy.BaseGold);
                EnemyIconImage.Source = new BitmapImage(new Uri(enemy.Icon.ImagePath));
            } 
        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt";
            dialog.Title = "Выберите куда сохранять";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                enemies.SaveToJson(filename);
            }
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {
            enemies.enemies.Clear();
            var dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";

            if (dialog.ShowDialog() == true)
            {
                enemies.LoadFromJson(dialog.FileName);
            }
        }
    }
}