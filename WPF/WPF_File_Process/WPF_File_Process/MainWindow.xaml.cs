using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static WPF_File_Process.MainWindow;

namespace WPF_File_Process
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getFileInFolder();
        }

        public class FileItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        public ObservableCollection<FileItem> FileItems { get; set; } = new ObservableCollection<FileItem>();

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (lvFolder.SelectedItem != null)
            {
                // Get the selected item
                var selectedItem = (FileItem)lvFolder.SelectedItem;

                // Get the name and path of the selected item
                string name = selectedItem.Name;
                string path = txtPath.Text + "\\" + name;

                // Ask the user for the new name
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter new name", "Rename", name);

                // Check if the user entered a new name
                if (!string.IsNullOrEmpty(newName))
                {
                    // Create the new path
                    string newPath = Path.GetDirectoryName(path) + "\\" + newName;

                    // Check if the selected item is a file or a directory
                    if (File.Exists(path))
                    {
                        // If it's a file, rename it
                        File.Move(path, newPath);
                    }
                    else if (Directory.Exists(path))
                    {
                        // If it's a directory, rename it
                        Directory.Move(path, newPath);
                    }

                    // Clear the ListView items
                    lvFolder.Items.Clear();

                    // Repopulate the ListView items
                    getFileInFolder();
                }
            }
        }

        private void getFileInFolder()
        {
            //Get File v3
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = System.IO.Directory.GetFiles(dialog.SelectedPath);
                lvFolder.Items.Add(new FileItem { Name = dialog.SelectedPath, Path = dialog.SelectedPath });
                foreach (string file in files)
                {
                    txtPath.Text = dialog.SelectedPath;
                    string fileName = System.IO.Path.GetFileName(file);
                    lvFolder.Items.Add(new FileItem { Name = fileName, Path = file });
                }
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Check if an item is selected
            if (lvFolder.SelectedItem != null)
            {
                // Get the selected item
                var selectedItem = (FileItem)lvFolder.SelectedItem;

                // Get the path of the selected item
                string path = selectedItem.Path;

                // Check if the selected item is a file or a directory
                if (File.Exists(path))
                {
                    // If it's a file, open it in the default application
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                else if (Directory.Exists(path))
                {
                    // If it's a directory, open it in the file explorer
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected
            if (lvFolder.SelectedItem != null)
            {
                // Get the selected item
                var selectedItem = (FileItem)lvFolder.SelectedItem;

                // Get the path of the selected item
                string path = selectedItem.Path;

                // Ask the user for confirmation
                System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Check if the selected item is a file or a directory
                    if (File.Exists(path))
                    {
                        // If it's a file, delete it
                        File.Delete(path);
                    }
                    else if (Directory.Exists(path))
                    {
                        // If it's a directory, delete it
                        Directory.Delete(path, true); // The second parameter is to delete recursively
                    }

                    // Remove the selected item from the ListView
                    FileItems.Remove(selectedItem);
                }
            }
        }
    }
}
