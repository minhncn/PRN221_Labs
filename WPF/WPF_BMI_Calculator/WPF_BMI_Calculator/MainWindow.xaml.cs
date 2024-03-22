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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double weight;
        double height;
        double bmi;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                weight = double.Parse(((TextBox)sender).Text);
            }
            catch 
            {
                
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                height = double.Parse(((TextBox)sender).Text) / 100;
            }
            catch 
            {
                
            }
        }

        private double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bmi = CalculateBMI(weight, height);
            if (bmi < 16)
            {
                ResultLabel.Content = $"{bmi:F1}";
                ResultStatus.Content = "Underweight";
                ResultLabel.Background = new SolidColorBrush(Colors.Blue);
            }
            else if (bmi >= 18.5 && bmi <= 22.9)
            {
                ResultLabel.Content = $"{bmi:F1}";
                ResultStatus.Content = "Normal weight";
                ResultLabel.Background = new SolidColorBrush(Colors.Green);
            }
            else if (bmi >= 23 && bmi <= 24.9)
            {
                ResultLabel.Content = $"{bmi:F1}";
                ResultStatus.Content = "Over weight";
                ResultLabel.Background = new SolidColorBrush(Colors.Yellow);
            }
            else if (bmi >= 25 && bmi <= 29.9)
            {
                ResultLabel.Content = $"{bmi:F1}";
                ResultStatus.Content = "Obese 1";
                ResultLabel.Background = new SolidColorBrush(Colors.Orange);
            }
            else if (bmi >= 30)
            {
                ResultLabel.Content = $"{bmi:F1}";
                ResultStatus.Content = "Extremely Obese";
                ResultLabel.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtHeight.Text = String.Empty; 
            txtWeight.Text = String.Empty;
            ResultLabel.Content = string.Empty;
            ResultLabel.Background = new SolidColorBrush(Colors.White);
            ResultStatus.Content = string.Empty;
        }
    }
}
