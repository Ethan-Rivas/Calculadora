using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// Habilitar uso de PerformClick.
namespace System.Windows.Controls { public static class MyExt { public static void PerformClick(this Button btn) { btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); } } }

namespace Calculadora_Parcial1
{
    public partial class MainWindow : Window
    {
        private Double total = 0;
        private Boolean op_state = false;
        private String operation = "";
        private String operation_next = "";

        private Double memory = 0;

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

            if(num == ".")
            {
                if(!total_text.Text.Contains("."))
                {
                    _ = total_text.Text != "0" ? total_text.Text += num : total_text.Text = "0" + num;
                }
            } else
            {
                _ = total_text.Text != "0" ? total_text.Text += num : total_text.Text = num;
            }

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
                if (operation == "√")
                {
                    this.operation = operation_next;
                    operation_text.Text += operation + total_text.Text;
                    total = Double.Parse(total_text.Text);
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation == "x²")
                {
                    this.operation = operation_next;
                    operation_text.Text += total_text.Text + "²";
                    total = Double.Parse(total_text.Text);
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation == "1/x")
                {
                    this.operation = operation_next;
                    operation_text.Text += "1/" + total_text.Text;
                    total = Double.Parse(total_text.Text);
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation == "%")
                {
                    this.operation = operation_next;
                    operation_text.Text += operation + total_text.Text;
                    total = Double.Parse(total_text.Text);
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else
                {
                    operation_text.Text += total_text.Text + operation;
                    total = Double.Parse(total_text.Text);
                }
            }
            else
            {
                if (Char.IsDigit(operation_text.Text.Last()))
                {
                    if (operation == "√" || operation == "x²" || operation == "1/x" || operation == "%")
                    {
                        this.operation = operation_next;
                        operation_text.Text += operation_next;
                        total = Double.Parse(total_text.Text);
                    }
                    else
                    {
                        operation_text.Text += total_text.Text + operation_next;
                        this.Calculate();
                        total = Double.Parse(total_text.Text);
                    }
                }
                else if (operation_text.Text.Last().ToString().Equals("="))
                {
                    operation_text.Text = "";
                    operation_text.Text += total_text.Text + operation_next;
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation == "√" || operation == "%" || operation == "x²" || operation == "1/x")
                {
                    this.operation = operation_next;
                    operation_text.Text += operation_next;
                    total = Double.Parse(total_text.Text);
                }
                else if (operation_next == "√" || operation_next == "%")
                {
                    operation_text.Text += operation_next + total_text.Text;
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation_next == "x²")
                {
                    operation_text.Text += total_text.Text + "²";
                    this.Calculate();
                    total = Double.Parse(total_text.Text);
                }
                else if (operation_next == "1/x")
                {
                    operation_text.Text += "1/" + total_text.Text;
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
                case "√":
                    total_text.Text = Math.Sqrt(total).ToString();
                    break;
                case "x²":
                    total_text.Text = (total * total).ToString();
                    break;
                case "1/x":
                    total_text.Text = (1/total).ToString();
                    break;
                case "%":
                    total_text.Text = (total % Double.Parse(total_text.Text)).ToString();
                    break;
                default:
                    break;
            }
        }
        private void BtnNegateClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = (Double.Parse(total_text.Text) * -1).ToString();
        }

        private void BtnMcClick(object sender, RoutedEventArgs e)
        {
            memory = 0;
        }

        private void BtnMrClick(object sender, RoutedEventArgs e)
        {
            operation_text.Text = "";
            total_text.Text = memory.ToString();
        }

        private void BtnMAddClick(object sender, RoutedEventArgs e)
        {
            memory += Double.Parse(total_text.Text);
        }

        private void BtnMSubClick(object sender, RoutedEventArgs e)
        {
            memory -= Double.Parse(total_text.Text);
        }

        private void BtnMsClick(object sender, RoutedEventArgs e)
        {
            memory = Double.Parse(total_text.Text);
        }

        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Teclado
                case Key.D0:
                    btn_0.PerformClick();
                    break;
                case Key.D1:
                    btn_1.PerformClick();
                    break;
                case Key.D2:
                    btn_2.PerformClick();
                    break;
                case Key.D3:
                    btn_3.PerformClick();
                    break;
                case Key.D4:
                    btn_4.PerformClick();
                    break;
                case Key.D5:
                    btn_5.PerformClick();
                    break;
                case Key.D6:
                    btn_6.PerformClick();
                    break;
                case Key.D7:
                    btn_7.PerformClick();
                    break;
                case Key.D8:
                    btn_8.PerformClick();
                    break;
                case Key.D9:
                    btn_9.PerformClick();
                    break;
                // NumPad
                case Key.NumPad0:
                    btn_0.PerformClick();
                    break;
                case Key.NumPad1:
                    btn_1.PerformClick();
                    break;
                case Key.NumPad2:
                    btn_2.PerformClick();
                    break;
                case Key.NumPad3:
                    btn_3.PerformClick();
                    break;
                case Key.NumPad4:
                    btn_4.PerformClick();
                    break;
                case Key.NumPad5:
                    btn_5.PerformClick();
                    break;
                case Key.NumPad6:
                    btn_6.PerformClick();
                    break;
                case Key.NumPad7:
                    btn_7.PerformClick();
                    break;
                case Key.NumPad8:
                    btn_8.PerformClick();
                    break;
                case Key.NumPad9:
                    btn_9.PerformClick();
                    break;
                // Control
                case Key.Delete:
                    btn_clear.PerformClick();
                    break;
                case Key.Back:
                    btn_del.PerformClick();
                    break;
                // Operaciones
                case Key.Add:
                    btn_add.PerformClick();
                    break;
                case Key.Subtract:
                    btn_sub.PerformClick();
                    break;
                case Key.Multiply:
                    btn_multi.PerformClick();
                    break;
                case Key.Divide:
                    btn_div.PerformClick();
                    break;
            }

        }

    }
}
