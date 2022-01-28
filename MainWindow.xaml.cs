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
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HashSet<char> operations = new HashSet<char> { '+', '-', '*', '/' };
        #region init
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Input.Text = $"= {Calculation()}";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            separator.Content = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Input.Focus();
        }
        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Input.Text = $"= {Calculation()}";
        }

        #endregion

        #region functions
        private string Calculation()
        {
            string input = Input.Text;
            string res = "";
            bool calculate = true;
            string[] arrLine;
            List<string> NumLine = new List<string>();

            ErrorCheck();
            // whitespace removal
            if (input.Contains(" "))
            {
                for (int i = 0; i < input.Length; i++)
                    if (input[i] == ' ') input = input.Remove(i);
            }
            // number isolation
            for (int i = 0; i < input.Length; i++)
            {
                if (operations.Contains(input[i]))
                {
                    input = input.Insert(i, " ");
                    input = input.Insert(i + 2, " ");
                    i++;
                }
            }

            arrLine = input.Split(' ');
            for (int i = 0; i < arrLine.Length; i++)
                NumLine.Add(arrLine[i]);

            // actuall calculation
            #region calculation
            while (calculate)
            {
                // higher operations
                bool highPrio = true;

                if (highPrio == true && NumLine.Contains("/")) // divide
                {
                    int index = NumLine.IndexOf("/");
                    try
                    {
                        NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) / Convert.ToDouble(NumLine[index + 1])).ToString();
                        NumLine.RemoveAt(index - 1);
                        NumLine.RemoveAt(index);
                    }
                    catch { res = "Syntax error"; }
                }

                else if (highPrio == true && NumLine.Contains("*"))  // multiply
                {
                    try
                    {
                        int index = NumLine.IndexOf("*");
                        NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) * Convert.ToDouble(NumLine[index + 1])).ToString();
                        NumLine.RemoveAt(index - 1);
                        NumLine.RemoveAt(index);
                    }
                    catch { res = "Syntax error"; }
                }

                else highPrio = false;

                // lower operations
                if (highPrio == false && NumLine.Contains("-")) // substract
                {
                    try
                    {
                        int index = NumLine.IndexOf("-");
                        NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) - Convert.ToDouble(NumLine[index + 1])).ToString();
                        NumLine.RemoveAt(index - 1);
                        NumLine.RemoveAt(index);
                    }
                    catch { res = "Syntax error"; }

                }
                else if (highPrio == false && NumLine.Contains("+")) // add
                {
                    try
                    {
                        int index = NumLine.IndexOf("+");
                        NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) + Convert.ToDouble(NumLine[index + 1])).ToString();
                        NumLine.RemoveAt(index - 1);
                        NumLine.RemoveAt(index);
                    }
                    catch { res = "Syntax error"; }

                }
                else if (highPrio == false) calculate = false;
            }
            #endregion

            try
            {
                Convert.ToDouble(NumLine[0]);
                res = NumLine[0];
            }
            catch { res = "Syntax error"; }         
            return res;
        }

        private void ErrorCheck()
        {
            string input = Input.Text;
            for (int i = 0; i < input.Length; i++)
            {
                if (operations.Contains(input[i]))
                {
                    try
                    {
                        Convert.ToDouble(input[i - 1]);
                        Convert.ToDouble(input[i + 1]);
                    }
                    catch
                    {
                        ShowError(i);
                        return;
                    }                                      
                }
            }
        }

        private void ShowError(int index)
        {

        }

        #region numpad
        private void one_Click(object sender, RoutedEventArgs e) => Input.Text += "1";
        private void two_Click(object sender, RoutedEventArgs e) => Input.Text += "2";
        private void three_Click(object sender, RoutedEventArgs e) => Input.Text += "3";
        private void four_Click(object sender, RoutedEventArgs e) => Input.Text += "4";
        private void five_Click(object sender, RoutedEventArgs e) => Input.Text += "5";
        private void six_Click(object sender, RoutedEventArgs e) => Input.Text += "6";
        private void seven_Click(object sender, RoutedEventArgs e) => Input.Text += "7";
        private void eight_Click(object sender, RoutedEventArgs e) => Input.Text += "8";
        private void nine_Click(object sender, RoutedEventArgs e) => Input.Text += "9";
        private void zero_Click(object sender, RoutedEventArgs e) => Input.Text += "0";

        private void Divide_Click(object sender, RoutedEventArgs e) => Input.Text += "/";
        private void Multiply_Click(object sender, RoutedEventArgs e) => Input.Text += "*";
        private void Substract_Click(object sender, RoutedEventArgs e) => Input.Text += "-";
        private void Add_Click(object sender, RoutedEventArgs e) => Input.Text += "+";
        private void separator_Click(object sender, RoutedEventArgs e) => Input.Text += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


        #endregion

        #endregion
    }
}
