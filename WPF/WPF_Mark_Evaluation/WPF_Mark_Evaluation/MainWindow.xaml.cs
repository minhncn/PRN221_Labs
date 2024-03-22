using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Mark_Evaluation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, List<Student>> schoolYearData = new Dictionary<string, List<Student>>();
        private List<string> schoolYears = new List<string> { "2017", "2018", "2019", "2020", "2021" };
        
        public class Student
        {
            public string? StudentCode { get; set; }
            public string? ProvinceCode { get; set; }
            public double Mark { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            //btnFile_Click(this, new RoutedEventArgs());
            schoolYearComboBox.ItemsSource = schoolYears;
        }

        private void LoadData(string filePath)
        {
            var students = File.ReadAllLines(filePath)
                .Skip(1) // skip header line
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Student
                    {
                        StudentCode = parts[0],
                        ProvinceCode = parts[1],
                        Mark = double.TryParse(parts[2], out double mark) ? mark : 0
                    };
                })
                .ToList();

            var averageMarks = students
                .GroupBy(s => s.ProvinceCode)
                .Select(g => new
                {
                    ProvinceCode = g.Key,
                    AverageMark = g.Average(s => s.Mark)
                })
                .OrderByDescending(g => g.AverageMark)
                .ToList();
            Dispatcher.Invoke(() =>
            {
                dataGrid.ItemsSource = averageMarks;
            });
        }

        private async void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                statusLabel.Content = "Uploading file...";

                await Task.Run(() => LoadData(openFileDialog.FileName));

                Dispatcher.Invoke(() =>
                {
                    statusLabel.Content = "Done";
                    txtPath.Text = openFileDialog.FileName;
                });
            }
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string schoolYear = schoolYearComboBox.SelectedItem.ToString();
            if(schoolYear.IsNullOrEmpty())
            {
                MessageBox.Show("Please select a school year.");
                return;
            }
            if (DataExistsForSchoolYear(schoolYear))
            {
                MessageBox.Show("Data for the selected school year already exists.");
                return;
            }

            statusLabel.Content = "Importing data...";

            await Task.Run(() => LoadData(txtPath.Text));

            statusLabel.Content = "Done";
        }

        private bool DataExistsForSchoolYear(string schoolYear)
        {
            return schoolYearData.ContainsKey(schoolYear);
        }

        private void ClearDataForSchoolYear(string schoolYear)
        {
            // Clear the data for the school year.
            // This depends on how you're storing your data.
        }
    }
}
