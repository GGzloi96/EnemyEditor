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
        }


    }
}