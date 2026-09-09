using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryForLaba1;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EnemyEditor
{

    public partial class MainEnemyEditor : Window
    {
        private List<EnemyIcon> enemyIcons = new List<EnemyIcon>();
        public MainEnemyEditor()
        {
            InitializeComponent();
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
            // проверка что выбранный элемент является изображением
            // и что элемент не равен null
            if (iconHolder.SelectedItem is Image selectedImage && iconHolder.SelectedItem != null)
            {
                // получение имени файла из источника изображения
                // так как Source это Uri, то для получения имени файла
                // нужно преобразовать его в строку и использовать Path.GetFileName.
                // ListBox хранит в себе объекты типа Image
                // selectedImage.Source.ToString() возвращает полный путь до изображения
                string iconName = System.IO.Path.GetFileName(selectedImage.Source.ToString());
                MessageTextBlock.Text = iconName;
                // присвоение имени иконки в шаблон врага
                //someEnemy.IconName = iconName;
            }
        }
    }
}