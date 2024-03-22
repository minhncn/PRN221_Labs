using Microsoft.Win32;
using System.Windows;
using WPF_ScoreStorage.Repository;
using System.IO;
using System.Linq;
using System;
using WPF_ScoreStorage.Model;
using WPF_ScoreStorage.Enums;
using WPF_ScoreStorage.Entities;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Globalization;

namespace WPF_ScoreStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISchoolYearRepository _schoolYearRepository;
        private IStudentRepository _studentRepository;
        private ScoreStorageDbContext _scoreStorageDbContextRepository;
        private string filePathChosen;
        private Statistics statistics;
        public MainWindow()
        {
            _schoolYearRepository = new SchoolYearRepository();
            _studentRepository = new StudentRepository();
            _scoreStorageDbContextRepository = new ScoreStorageDbContext();

            InitializeComponent();
            for (int year = 2017; year <= 2021; year++)
            {
                yearDropdown.Items.Add(year.ToString());
                yearDropdownDeleted.Items.Add(year.ToString());
                yearDropdownValedictorian.Items.Add(year.ToString());
            }

            yearDropdown.SelectedIndex = 0;
            yearDropdownValedictorian.SelectedIndex = yearDropdownValedictorian.Items.Count - 1;

            filePathChosen = "";
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath.Text = openFileDialog.FileName;
                filePathChosen = openFileDialog.FileName;
            }
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            loadingGrid.Visibility = Visibility.Visible;

            try
            {
                if (int.TryParse(yearDropdown.SelectedItem.ToString(), out int selectedYear))
                {
                    if (filePathChosen == "")
                    {
                        MessageBox.Show("Please select the file path to import first.");
                        return;
                    }

                    var isDeletedSchoolYearYet = _schoolYearRepository.GetDeactiveSchoolYearByExamYear(selectedYear);

                    if (isDeletedSchoolYearYet != null)
                    {
                        var deactiveSchoolYear = await _scoreStorageDbContextRepository.SchoolYears.
                            FirstOrDefaultAsync(y => y.ExamYear == selectedYear);
                        deactiveSchoolYear.Status = SchoolYearStatus.Active.ToString();

                        await _scoreStorageDbContextRepository.SaveChangesAsync();
                        MessageBox.Show("Import successfully.");
                        return;
                    }

                    var existingSchoolYear = _schoolYearRepository.GetSchoolYearByExamYear(selectedYear);

                    if (existingSchoolYear != null)
                    {
                        MessageBox.Show("There is already data of the year \"" + selectedYear.ToString() + "\".");
                        return;
                    }

                    if (!File.Exists(filePathChosen))
                    {
                        MessageBox.Show("File does not exist.");
                        return;
                    }

                    var lines = File.ReadLines(filePathChosen);
                    var dataRows = lines.Skip(1);

                    var schoolYearId = Guid.NewGuid().ToString();
                    var schoolYear = new SchoolYear()
                    {
                        Id = schoolYearId,
                        Name = selectedYear.ToString(),
                        ExamYear = selectedYear,
                        Status = SchoolYearStatus.Active.ToString(),
                    };
                    _schoolYearRepository.AddSchoolYear(schoolYear);

                    var studentData = dataRows.Select(line => line.Split(','))
                        .Where(cols => int.Parse(cols[6]) == selectedYear)
                        .Select((cols, index) => new StudentDto
                        {
                            Id = Guid.NewGuid().ToString(),
                            StudentCode = cols[0],
                            SchoolYearId = schoolYearId,
                            Math = decimal.TryParse(cols[1], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal MathScore) ? MathScore : (decimal?)null,
                            Literature = decimal.TryParse(cols[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal LiteratureScore) ? LiteratureScore : (decimal?)null,
                            Physics = decimal.TryParse(cols[3], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal PhysicsScore) ? PhysicsScore : (decimal?)null,
                            Biology = decimal.TryParse(cols[4], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mathScore) ? mathScore : (decimal?)null,
                            English = decimal.TryParse(cols[5], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal EnglishScore) ? EnglishScore : (decimal?)null,
                            Chemistry = decimal.TryParse(cols[7], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ChemistryScore) ? ChemistryScore : (decimal?)null,
                            History = decimal.TryParse(cols[8], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal HistoryScore) ? HistoryScore : (decimal?)null,
                            Geography = decimal.TryParse(cols[9], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal GeographyScore) ? GeographyScore : (decimal?)null,
                            Civic = decimal.TryParse(cols[10], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal CivicScore) ? CivicScore : (decimal?)null,
                            ProvinceId = cols[11],
                        }).ToList();

                    Helper.SqlBulkyCopy.WriteStudentToDatabase(studentData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            finally
            {
                loadingGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnClearYearData_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(yearDropdownDeleted.SelectedItem.ToString(), out int selectedYear))
            {
                var isDeletedSchoolYearYet = _schoolYearRepository.GetDeactiveSchoolYearByExamYear(selectedYear);

                if (isDeletedSchoolYearYet != null)
                {
                    MessageBox.Show("Data of the year \"" + selectedYear.ToString() + "\" is already deleted.");
                    return;
                }

                var existingSchoolYear = _schoolYearRepository.GetSchoolYearByExamYear(selectedYear);

                if (existingSchoolYear == null)
                {
                    MessageBox.Show("There does not exist data of the year \"" + selectedYear.ToString() + "\" yet.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Do you want to continue clearing data of the year " + selectedYear + "?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var schoolYear = await _scoreStorageDbContextRepository.SchoolYears.
                        FirstOrDefaultAsync(y => y.ExamYear == selectedYear);
                    schoolYear.Status = SchoolYearStatus.Deactive.ToString();

                    await _scoreStorageDbContextRepository.SaveChangesAsync();
                    MessageBox.Show("Clear successfully.");
                }
            }
        }

        private void btnViewNumberCandidatesStatistics_Click(object sender, RoutedEventArgs e)
        {
            statistics = new Statistics((int)StatisticsAction.ViewNumberCandidates);
            statistics.Show();
        }

        private void btnViewValedictorianStatistics_Click(object sender, RoutedEventArgs e)
        {
            var selectedYearComboBox = yearDropdownValedictorian.SelectedValue;
            if (selectedYearComboBox == null)
            {
                MessageBox.Show("Please select a correct province first.");
                return;
            }
            statistics = new Statistics((int)StatisticsAction.ViewValedictorian, selectedYearComboBox.ToString());
            statistics.Show();
        }
    }
}
