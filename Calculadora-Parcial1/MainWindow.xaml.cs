using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// Habilitar uso de PerformClick para simular la presión de botones con el teclado.
namespace System.Windows.Controls { public static class MyExt { public static void PerformClick(this Button btn) { btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); } } }

namespace Calculadora_Parcial1
{
    public partial class MainWindow : Window
    {
        readonly Operation operation = new Operation(); // Clase con Operaciones (add, sub, mult, div).

        private Boolean op_state = false; // Estado de operación (Si está operandose algo o no).
        private String operand = ""; // Signo de Operación a realizar.
        private Double total, times, memory; // Iniclialización para el total, memoria y nth

        public MainWindow()
        {
            InitializeComponent();
        }

        // Botones (Lógica de Botón numérico y punto)
        private void BtnClick(object sender, RoutedEventArgs e)
        {
            String num = (string)(sender as Button).Content; // Obtenemos el valor del objeto como un botón y convertimos su contenido a String.

            // Verificar si está realizandose una operación y limpia el texto para asignar un nuevo valor.
            if (op_state)
            {
                total_text.Text = "";
                op_state = false;
            }

            // Verificar si la calculadora está en 0 para limpiarla y proceder a ingresar nuestros números
            if (total_text.Text == "0") total_text.Text = "";
            // Verificar si el botón presionado es un punto y si ya existe un punto asignado para que el punto "no se asigne"
            if (num == "." && total_text.Text.Contains(".")) num = "";

            total_text.Text += num; // Asigna el valor a la calculadora (total_text)
        }

        // Botón de Operación (Lógica de Botón de operaciones).
        private void BtnOperationClick(object sender, RoutedEventArgs e)
        {
            // Cambiar el estado de operación a activa (true)
            op_state = true;
            operand = (String)(sender as Button).Content;

            this.OperationHandler(operand); // Ejecutar OperationHandler con los parámetros de operación requeridos
        }

        /* 
        * Manejador de operaciones, recibe la operación y la operación siguiente a realizar
        * Así mismo, muestra en pantalla la secuencia de operaciones realizadas para llegar al resultado
        */
        private void OperationHandler(String operand)
        {
            if (operation_text.Text != "" && operation_text.Text.Last().ToString().Equals("="))
            {
                operation_text.Text = ""; // Limpia la secuencia
            }

            switch(operand)
            {
                case "√":
                    times = Double.Parse(total_text.Text);
                    operation_text.Text += times + operand;
                    break;
                case "1/x":
                    total = Double.Parse(total_text.Text);
                    operation_text.Text += "1/" + total.ToString();
                    break;
                default:
                    total = Double.Parse(total_text.Text);
                    operation_text.Text += total.ToString() + operand;
                    break;
            }

        }

        // Control (Borra todo)
        private void BtnClearAllClick(object sender, RoutedEventArgs e)
        {
            operation_text.Text = "";
            total_text.Text = "0";
            total = 0;
            operand = "";
        }

        // Control (Borra el estado de calculadora a evaluar)
        private void BtnClearClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = "0";
            total = 0;
        }

        // Control (Borra el último carácter de la calculadora)
        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = total_text.Text.Remove(total_text.Text.Length - 1) == "" ? "0" : total_text.Text.Remove(total_text.Text.Length - 1);
        }

        // Control (Evalúa la secuencia de operaciones)
        private void ButtonEqual(object sender, RoutedEventArgs e)
        {
            if (!op_state)
                operation_text.Text += total_text.Text + "=";
                op_state = true;

            this.Calculate();

            total = 0;
        }

        // Calculo (Basado en el operando)
        private void Calculate()
        {
            switch (operand)
            {
                case "+":
                    total += Double.Parse(total_text.Text);
                    break;
                case "-":
                    total -= Double.Parse(total_text.Text);
                    break;
                case "*":
                    total *= Double.Parse(total_text.Text);
                    break;
                case "/":
                    if (total_text.Text == "0")
                    {
                        MessageBox.Show("No se puede dividir entre 0", "Aceptar");
                        operation_text.Text = "";
                        total_text.Text = "0";
                        total = 0;
                        operand = "";
                    } 
                    else
                    {
                        total /= Double.Parse(total_text.Text);
                    }
                    break;
                case "√":
                    total = this.operation.squaret(times, Double.Parse(total_text.Text));
                    break;
                case "x^":
                    total = this.operation.pot(total, Double.Parse(total_text.Text));
                    break;
                case "1/x":
                    total = (1 / total);
                    break;
                case "%":
                    total = this.operation.porcentage(total, Double.Parse(total_text.Text));
                    break;
                default:
                    break;
            }

            total_text.Text = total.ToString();
        }

        // Botón para cambiar el signo del número
        private void BtnNegateClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = (Double.Parse(total_text.Text) * -1).ToString();
        }

        // Botón de memoria (Limpia la memoria)
        private void BtnMcClick(object sender, RoutedEventArgs e)
        {
            memory = 0;
        }
        // Botón de memoria (Recuperar memoria)
        private void BtnMrClick(object sender, RoutedEventArgs e)
        {
            total_text.Text = memory.ToString();
        }
        // Botón de memoria (Sumar a memoria)
        private void BtnMAddClick(object sender, RoutedEventArgs e)
        {
            memory += Double.Parse(total_text.Text);
        }
        // Botón de memoria (Restar a memoria)
        private void BtnMSubClick(object sender, RoutedEventArgs e)
        {
            memory -= Double.Parse(total_text.Text);
        }
        // Botón de memoria (Almacenar a memoria)
        private void BtnMsClick(object sender, RoutedEventArgs e)
        {
            memory = Double.Parse(total_text.Text);
        }

        // Binding de TECLAS a calculadora
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
