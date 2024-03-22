using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;
using WPF_ScoreStorage.Entities;
using WPF_ScoreStorage.Enums;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls.Primitives;

namespace WPF_ScoreStorage
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private ScoreStorageDbContext _scoreStorageDbContextRepository;
        DataGrid dataGrid;
        public Statistics(int action)
        {
            Init();
            switch (action)
            {
                case (int)StatisticsAction.ViewNumberCandidates:
                    this.Title = "Number Of Candidates By Year";
                    LoadNumberCandidatesAsync();
                    break;
                case (int)StatisticsAction.ViewValedictorian:
                    this.Title = "Valedictorians By Year";
                    LoadValedictorianAsync();
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        public Statistics(int action, string selectedIndex)
        {
            Init();
            switch (action)
            {
                case (int)StatisticsAction.ViewValedictorian:
                    this.Title = "Valedictorians Of Years";
                    LoadValedictorianAsync(selectedIndex);
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        private void Init()
        {
            _scoreStorageDbContextRepository = new ScoreStorageDbContext();
            InitializeComponent();
            loadingGrid.Visibility = Visibility.Visible;
            LoadingNotificationAnimation();
            dataStatistics.Children.Remove(dataGrid);
            dataGrid = new DataGrid();
            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(FontWeightProperty, FontWeights.Bold));
            columnHeaderStyle.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0.5)));
            columnHeaderStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(PaddingProperty, new Thickness(2, 0, 2, 0)));
            dataGrid.ColumnHeaderStyle = columnHeaderStyle;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserSortColumns = true;
        }
        private async void LoadingNotificationAnimation()
        {
            for (var i = 0; i <= 19; i++)
            {
                if (i % 4 == 0)
                {
                    txtLoadingNotification.Text = "Loading Data";
                }
                else if (i % 4 == 1)
                {
                    txtLoadingNotification.Text = "Loading Data.";
                }
                else if (i % 4 == 2)
                {
                    txtLoadingNotification.Text = "Loading Data..";
                }
                else if (i % 4 == 3)
                {
                    txtLoadingNotification.Text = "Loading Data...";
                }
                await Task.Delay(500);
            }
        }

        private async Task LoadNumberCandidatesAsync()
        {
            loadingGrid.Visibility = Visibility.Visible;
            dataStatistics.Children.Remove(dataGrid);
            dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = false;

            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Year",
                Binding = new Binding("Year"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Student Count",
                Binding = new Binding("StudentCount"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Toan",
                Binding = new Binding("SubjectCounts.Math"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Van",
                Binding = new Binding("SubjectCounts.Literature"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ly",
                Binding = new Binding("SubjectCounts.Physics"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Sinh",
                Binding = new Binding("SubjectCounts.Biology"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ngoai Ngu",
                Binding = new Binding("SubjectCounts.English"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Hoa",
                Binding = new Binding("SubjectCounts.Chemistry"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Lich Su",
                Binding = new Binding("SubjectCounts.History"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Dia Ly",
                Binding = new Binding("SubjectCounts.Geography"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "GDCD",
                Binding = new Binding("SubjectCounts.Civic"),
            });

            try
            {
                var query = await _scoreStorageDbContextRepository.Students
                //.Where(s => s.SchoolYear.Status == SchoolYearStatus.Active.ToString())
                .GroupBy(s => s.SchoolYearId)
                .Select(g => new
                {
                    Year = g.FirstOrDefault().SchoolYear.ExamYear.ToString(),
                    StudentCount = g.Count().ToString(),
                    SubjectCounts = new
                    {
                        Math = g.Count(s => s.Math != null),
                        Literature = g.Count(s => s.Literature != null),
                        Physics = g.Count(s => s.Physics != null),
                        Biology = g.Count(s => s.Biology != null),
                        English = g.Count(s => s.English != null),
                        Chemistry = g.Count(s => s.Chemistry != null),
                        History = g.Count(s => s.History != null),
                        Geography = g.Count(s => s.Geography != null),
                        Civic = g.Count(s => s.Civic != null)
                    }
                })
                .OrderBy(g => g.Year)
                .ToListAsync();

                dataGrid.ItemsSource = query;
                dataStatistics.Children.Add(dataGrid);
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

        private async Task LoadValedictorianAsync()
        {
            loadingGrid.Visibility = Visibility.Visible;
            dataStatistics.Children.Remove(dataGrid);
            dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = false;

            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Year",
                Binding = new Binding("Year"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "A00 (Toan, Ly, Hoa)",
                Binding = new Binding("DiemThuKhoaKhoiA00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "A01 (Toan, Ly, Anh)",
                Binding = new Binding("DiemThuKhoaKhoiA01"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "B00 (Toan, Hoa, Sinh)",
                Binding = new Binding("DiemThuKhoaKhoiB00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "C00 (Van, Su, Dia)",
                Binding = new Binding("DiemThuKhoaKhoiC00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "D01 (Toan, Van, Anh)",
                Binding = new Binding("DiemThuKhoaKhoiD01"),
            });

            try
            {
                var statistics = await _scoreStorageDbContextRepository.Students
                .GroupBy(s => s.SchoolYearId)
                .Select(g => new
                {
                    Year = g.FirstOrDefault().SchoolYear.ExamYear.ToString(),
                    DiemThuKhoaKhoiA00 = g.Where(s => s.Math.HasValue && s.Physics.HasValue && s.Chemistry.HasValue)
                                          .Max(s => (s.Math + s.Physics + s.Chemistry)),

                    DiemThuKhoaKhoiA01 = g.Where(s => s.Math.HasValue && s.Physics.HasValue && s.English.HasValue)
                                          .Max(s => (s.Math + s.Physics + s.English)),

                    DiemThuKhoaKhoiB00 = g.Where(s => s.Math.HasValue && s.Chemistry.HasValue && s.Biology.HasValue)
                                          .Max(s => (s.Math + s.Chemistry + s.Biology)),

                    DiemThuKhoaKhoiC00 = g.Where(s => s.Literature.HasValue && s.History.HasValue && s.Geography.HasValue)
                                          .Max(s => (s.Literature + s.History + s.Geography)),

                    DiemThuKhoaKhoiD01 = g.Where(s => s.Math.HasValue && s.Literature.HasValue && s.English.HasValue)
                                          .Max(s => (s.Math + s.Literature + s.English)),
                })
                .OrderBy(g => g.Year)
                .ToListAsync();

                dataGrid.ItemsSource = statistics;
                dataStatistics.Children.Add(dataGrid);
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

        private async Task LoadValedictorianAsync(string selectedYear)
        {
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Year",
                Binding = new Binding("Year"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "A00 (Toán, Lý, Hóa)",
                Binding = new Binding("DiemThuKhoaKhoiA00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "B00 (Toán, Hóa, Sinh)",
                Binding = new Binding("DiemThuKhoaKhoiB00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "C00 (Văn, Sử, Địa)",
                Binding = new Binding("DiemThuKhoaKhoiC00"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "D01 (Toán, Văn, Anh)",
                Binding = new Binding("DiemThuKhoaKhoiD01"),
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "A01 (Toán, Lý, Anh)",
                Binding = new Binding("DiemThuKhoaKhoiA01"),
            });
            dataGrid.FontSize = 11;

            try
            {
                var statistics = await _scoreStorageDbContextRepository.Students
                .Where(s => s.SchoolYear.ExamYear.ToString() == selectedYear)
                .GroupBy(s => s.SchoolYearId)
                .Select(g => new
                {
                    Year = g.FirstOrDefault().SchoolYear.ExamYear.ToString(),
                    DiemThuKhoaKhoiA00 = g.Where(s => s.Math.HasValue && s.Physics.HasValue && s.Chemistry.HasValue)
                                          .Max(s => (s.Math + s.Physics + s.Chemistry)),

                    DiemThuKhoaKhoiB00 = g.Where(s => s.Math.HasValue && s.Chemistry.HasValue && s.Biology.HasValue)
                                          .Max(s => (s.Math + s.Chemistry + s.Biology)),

                    DiemThuKhoaKhoiC00 = g.Where(s => s.Literature.HasValue && s.History.HasValue && s.Geography.HasValue)
                                          .Max(s => (s.Literature + s.History + s.Geography)),

                    DiemThuKhoaKhoiD01 = g.Where(s => s.Math.HasValue && s.Literature.HasValue && s.English.HasValue)
                                          .Max(s => (s.Math + s.Literature + s.English)),

                    DiemThuKhoaKhoiA01 = g.Where(s => s.Math.HasValue && s.Physics.HasValue && s.English.HasValue)
                                          .Max(s => (s.Math + s.Physics + s.English)),
                })
                .OrderBy(g => g.Year)
                .ToListAsync();
                StackPanel dataStatisticsStackPanel = new StackPanel();

                dataGrid.ItemsSource = statistics;

                Label labelForDatagrid = new Label();
                labelForDatagrid.FontSize = 11;
                labelForDatagrid.FontWeight = FontWeights.Bold;
                labelForDatagrid.Content = "Thống Kê Điểm Thủ Khoa Các Khối Năm " + selectedYear;


                dataStatisticsStackPanel.Children.Add(labelForDatagrid);
                dataStatisticsStackPanel.Children.Add(dataGrid);
                loadingGrid.Visibility = Visibility.Collapsed;

                DataGrid dataGrid1 = new DataGrid();
                dataGrid1.AutoGenerateColumns = false;
                dataGrid1.CanUserSortColumns = true;

                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Năm Và Khối Thi",
                    Binding = new Binding("YearAndBlock"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "SBD",
                    Binding = new Binding("StudentCode"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Tỉnh Thành",
                    Binding = new Binding("Province"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Môn 1",
                    Binding = new Binding("Subject01"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Môn 2",
                    Binding = new Binding("Subject02"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Môn 3",
                    Binding = new Binding("Subject03"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Tổng Điểm",
                    Binding = new Binding("TotalScore"),
                });
                dataGrid1.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Tên Môn",
                    Binding = new Binding("SubjectsName"),
                });

                Label labelForDatagrid1 = new Label();
                labelForDatagrid1.FontSize = 11;
                labelForDatagrid1.FontWeight = FontWeights.Bold;
                labelForDatagrid.Foreground = new SolidColorBrush(Colors.BlueViolet);
                labelForDatagrid1.Foreground = new SolidColorBrush(Colors.BlueViolet);
                labelForDatagrid1.Content = "Thống Kê Thông Tin Thủ Khoa Các Khối Năm " + selectedYear;

                Label labelLoadingForForDatagrid1 = new Label();
                labelLoadingForForDatagrid1.FontSize = 11;
                labelLoadingForForDatagrid1.FontWeight = FontWeights.Bold;
                labelLoadingForForDatagrid1.Content = "Loading data... Please wait for a few seconds...";

                dataGrid1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                dataGrid1.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
                dataGrid1.FontSize = 11;

                Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
                columnHeaderStyle.Setters.Add(new Setter(FontWeightProperty, FontWeights.Bold));
                columnHeaderStyle.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0.5)));
                columnHeaderStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.Black));
                columnHeaderStyle.Setters.Add(new Setter(PaddingProperty, new Thickness(2, 0, 2, 0)));

                dataGrid1.ColumnHeaderStyle = columnHeaderStyle;

                dataStatisticsStackPanel.Children.Add(labelForDatagrid1);
                dataStatisticsStackPanel.Children.Add(labelLoadingForForDatagrid1);

                dataStatistics.Children.Add(dataStatisticsStackPanel);

                var listThuKhoaInformation = new List<dynamic>();

                foreach (var statisticsItem in statistics)
                {
                    var examYear = await _scoreStorageDbContextRepository.SchoolYears
                        .Where(y => y.ExamYear.ToString() == statisticsItem.Year)
                        .FirstOrDefaultAsync();
                    var examYearId = examYear.Id;

                    if (statisticsItem.DiemThuKhoaKhoiA00 != null)
                    {
                        var listThuKhoaA00 = await _scoreStorageDbContextRepository.Students
                        .Where(s => (s.Math + s.Physics + s.Chemistry) == statisticsItem.DiemThuKhoaKhoiA00 && s.SchoolYearId == examYearId)
                        .Select(s => new {
                            YearAndBlock = "A00",
                            StudentCode = s.StudentCode,
                            Province = s.Province != null ? s.Province.ProvinceName : (string?)null,
                            Subject01 = s.Math,
                            Subject02 = s.Physics,
                            Subject03 = s.Chemistry,
                            TotalScore = statisticsItem.DiemThuKhoaKhoiA00,
                            SubjectsName = "Toán, Lý, Hóa",
                        })
                        .ToListAsync();
                        listThuKhoaInformation.AddRange(listThuKhoaA00);
                    }

                    if (statisticsItem.DiemThuKhoaKhoiB00 != null)
                    {
                        var listThuKhoaB00 = await _scoreStorageDbContextRepository.Students
                        .Where(s => (s.Math + s.Chemistry + s.Biology) == statisticsItem.DiemThuKhoaKhoiB00 && s.SchoolYearId == examYearId)
                        .Select(s => new {
                            YearAndBlock = "B00",
                            StudentCode = s.StudentCode,
                            Province = s.Province != null ? s.Province.ProvinceName : (string?)null,
                            Subject01 = s.Math,
                            Subject02 = s.Chemistry,
                            Subject03 = s.Biology,
                            TotalScore = statisticsItem.DiemThuKhoaKhoiB00,
                            SubjectsName = "Toán, Hóa, Sinh",
                        })
                        .ToListAsync();
                        listThuKhoaInformation.AddRange(listThuKhoaB00);
                    }

                    if (statisticsItem.DiemThuKhoaKhoiC00 != null)
                    {
                        var listThuKhoaC00 = await _scoreStorageDbContextRepository.Students
                        .Where(s => (s.Literature + s.History + s.Geography) == statisticsItem.DiemThuKhoaKhoiC00 && s.SchoolYearId == examYearId)
                        .Select(s => new {
                            YearAndBlock = "C00",
                            StudentCode = s.StudentCode,
                            Province = s.Province != null ? s.Province.ProvinceName : (string?)null,
                            Subject01 = s.Literature,
                            Subject02 = s.History,
                            Subject03 = s.Geography,
                            TotalScore = statisticsItem.DiemThuKhoaKhoiC00,
                            SubjectsName = "Văn, Sử, Địa",
                        })
                        .ToListAsync();
                        listThuKhoaInformation.AddRange(listThuKhoaC00);
                    }

                    if (statisticsItem.DiemThuKhoaKhoiD01 != null)
                    {
                        var listThuKhoaD01 = await _scoreStorageDbContextRepository.Students
                        .Where(s => (s.Math + s.Literature + s.English) == statisticsItem.DiemThuKhoaKhoiD01 && s.SchoolYearId == examYearId)
                        .Select(s => new {
                            YearAndBlock = "D01",
                            StudentCode = s.StudentCode,
                            Province = s.Province != null ? s.Province.ProvinceName : (string?)null,
                            Subject01 = s.Math,
                            Subject02 = s.Literature,
                            Subject03 = s.English,
                            TotalScore = statisticsItem.DiemThuKhoaKhoiD01,
                            SubjectsName = "Toán, Văn, Anh",
                        })
                        .ToListAsync();
                        listThuKhoaInformation.AddRange(listThuKhoaD01);
                    }

                    if (statisticsItem.DiemThuKhoaKhoiA01 != null)
                    {
                        var listThuKhoaA01 = await _scoreStorageDbContextRepository.Students
                        .Where(s => (s.Math + s.Physics + s.English) == statisticsItem.DiemThuKhoaKhoiA01 && s.SchoolYearId == examYearId)
                        .Select(s => new {
                            YearAndBlock = "A01",
                            StudentCode = s.StudentCode,
                            Province = s.Province != null ? s.Province.ProvinceName : (string?)null,
                            Subject01 = s.Math,
                            Subject02 = s.Physics,
                            Subject03 = s.English,
                            TotalScore = statisticsItem.DiemThuKhoaKhoiA01,
                            SubjectsName = "Toán, Lý, Anh",
                        })
                        .ToListAsync();
                        listThuKhoaInformation.AddRange(listThuKhoaA01);
                    }

                }
                dataGrid1.ItemsSource = listThuKhoaInformation;
                dataStatisticsStackPanel.Children.Remove(labelLoadingForForDatagrid1);
                dataStatisticsStackPanel.Children.Add(dataGrid1);
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


    }
}
