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
            Input.Text = $"= {Calculation()}";
        }



        #region functions
        private double Calculation()
        {
            string input = Input.Text;
            double res = 0;
            bool calculate = true;
            HashSet<char> operations = new HashSet<char> { '+', '-', '*', '/'};
            string[] arrLine;
            List<string> NumLine = new List<string>();

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
            #region prasarna
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
                    catch { Input.Text = "Cannot divide by zero..."; }
                }

                else if (highPrio == true && NumLine.Contains("*"))  // multiply
                {
                    int index = NumLine.IndexOf("*");
                    NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) * Convert.ToDouble(NumLine[index + 1])).ToString();
                    NumLine.RemoveAt(index - 1);
                    NumLine.RemoveAt(index);
                }

                else highPrio = false;

                // lower operations
                if (highPrio == false && NumLine.Contains("-")) // substract
                {
                    int index = NumLine.IndexOf("-");
                    NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) - Convert.ToDouble(NumLine[index + 1])).ToString();
                    NumLine.RemoveAt(index - 1);
                    NumLine.RemoveAt(index);
                }
                else if (highPrio == false && NumLine.Contains("+")) // add
                {
                    int index = NumLine.IndexOf("+");
                    NumLine[index] = (Convert.ToDouble(NumLine[index - 1]) + Convert.ToDouble(NumLine[index + 1])).ToString();
                    NumLine.RemoveAt(index - 1);
                    NumLine.RemoveAt(index);
                }

                else if (highPrio == false) calculate = false;
            }
            #endregion

            res = Convert.ToDouble(NumLine[0]);

            return res;
        }
        #endregion
    }
}
