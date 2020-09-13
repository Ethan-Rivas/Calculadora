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

namespace Calculadora_Parcial1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Double total = 0;
        private Boolean op_state = false;
        private String operation = "";
        private String operation_next = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Botones
        private void BtnClick(object sender, RoutedEventArgs e)
        {
            String num = (string)(sender as Button).Content;

            if (op_state)
            {
                total_text.Text = "";
                op_state = false;
            }

            _ = total_text.Text != "0" ? total_text.Text += num : total_text.Text = num;
        }

        private void BtnOperationClick(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                op_state = true;
                operation = (String)(sender as Button).Content;
                operation_next = (String)(sender as Button).Content;
            }

            op_state = true;
            operation_next = (String)(sender as Button).Content;
            this.OperationHandler(operation, operation_next);
            operation = (String)(sender as Button).Content;
        }

        // Operaciones
        private void OperationHandler(String operation, String operation_next)
        {
            if (operation_text.Text == "")
            {
                operation_text.Text += total_text.Text + operation;
                total = Double.Parse(total_text.Text);
            }
            else
            {
                if (Char.IsDigit(operation_text.Text.Last()))
                {
                    operation_text.Text += total_text.Text + operation_next;
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation_text.Text.Last().ToString().Equals("="))
                {
                    operation_text.Text = "";
                    operation_text.Text += total_text.Text + operation_next;
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else
                {
                    if (total_text.Text == "")
                    {
                        operation_text.Text = operation_text.Text.Remove(operation_text.Text.Length - 1);
                        operation_text.Text += operation_next;
                    }
                    else
                    {
                        operation_text.Text += total_text.Text + operation_next;
                        this.Calculate();
                        total = Double.Parse(total_text.Text);
                    }
                }
            }
        }

        // Control
        private void BtnClearAllClick(object sender, RoutedEventArgs e)
        {
            operation_text.Text = "";
            total_text.Text = "0";
            total = 0;
            operation = "";
        }
        private void BtnClearClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = "0";
            total = 0;
        }
        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            _ = total_text.Text != "" && total_text.Text.Remove(total_text.Text.Length - 1) != "" ? total_text.Text = total_text.Text.Remove(total_text.Text.Length - 1) : total_text.Text = "0";
        }
        private void ButtonEqual(object sender, RoutedEventArgs e)
        {
            if (operation_text.Text != "" && !operation_text.Text.Last().ToString().Equals("="))
            {
                operation_text.Text += total_text.Text + "=";
                this.Calculate();

                total = Double.Parse(total_text.Text);
                total_text.Text = total.ToString();

                op_state = true;
                total = 0;
            }
        }
        private void Calculate()
        {
            switch (operation)
            {
                case "+":
                    total_text.Text = (total + Double.Parse(total_text.Text)).ToString();
                    break;
                case "-":
                    total_text.Text = (total - Double.Parse(total_text.Text)).ToString();
                    break;
                case "*":
                    total_text.Text = (total * Double.Parse(total_text.Text)).ToString();
                    break;
                case "/":
                    total_text.Text = (total / Double.Parse(total_text.Text)).ToString();
                    break;
                default:
                    break;
            }
        }

        private void BtnNegateClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = (Double.Parse(total_text.Text) * -1).ToString();
        }
    }
}
