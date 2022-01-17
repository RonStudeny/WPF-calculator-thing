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

namespace WpfApp1
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

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Result.Content = $"= {Calculation()}";
        }

        private double Calculation()
        {
            string input = Input.Text;
            double res = 0;
            HashSet<char> operations = new HashSet<char> { '+', '-', '*', '/'};
            string[] arrLine;
            List<string> Line = new List<string>();


            if (input.Contains(" "))
            {
                for (int i = 0; i < input.Length; i++)
                    if (input[i] == ' ') input = input.Remove(i);
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (operations.Contains(input[i]))
                {
                    input = input.Insert(--i, " ");
                    input = input.Insert(i + 2, " ");
                }
            }

            arrLine = input.Split(' ');
            for (int i = 0; i < arrLine.Length; i++)
                Line.Add(arrLine[i]);





            return res;
        }
    }
}
