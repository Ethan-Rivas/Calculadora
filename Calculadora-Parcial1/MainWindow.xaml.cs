using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// Habilitar uso de PerformClick para simular la presión de botones con el teclado.
namespace System.Windows.Controls { public static class MyExt { public static void PerformClick(this Button btn) { btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); } } }

/* 
 * TODO:
 * No permitir:
 * Divisiones entre 0.
 * Arreglar BUG de operaciones (devuelve números no esperados con algunas secuencias)
 * Agregar 0 antes del . si no tiene un número anterior.
 * Mostrar ERROR para casos requeridos.
 */
namespace Calculadora_Parcial1
{
    public partial class MainWindow : Window
    {
        Operation operation = new Operation(); // Clase con Operaciones (add, sub, mult, div).

        private Boolean op_state = false; // Estado de operación (Si está operandose algo o no).
        private String operand = ""; // Signo de Operación a realizar.
        private String next_operand = ""; // Signo de Operación siguiente.
        private Double total = 0; // Total de operación (Almacén de cantidad)

        private Double memory = 0; // Variable de Memoria

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

            // Si no se a asignado una operación previa, asignarla
            if (operand == "")
            {
                operand = (String)(sender as Button).Content;
            }

            next_operand = (String)(sender as Button).Content; // Asignar el operando como la operación siguiente a realizar
            this.OperationHandler(operand, next_operand); // Ejecutar OperationHandler con los parámetros de operación requeridos
            operand = (String)(sender as Button).Content; //  Asignar el operando como la operación que se realizará
        }

        /* 
        * Manejador de operaciones, recibe la operación y la operación siguiente a realizar
        * Así mismo, muestra en pantalla la secuencia de operaciones realizadas para llegar al resultado
        */
        private void OperationHandler(String operand, String next_operand)
        {
            // Si la secuencia de operaciones está vacía
            if (operation_text.Text == "")
            {
                // Si el operando (a evaluar) es una raíz
                if (operand == "√")
                {
                    this.operand = next_operand; // Cambia el operando al operando siguiente
                    operation_text.Text += operand + total_text.Text; // Pone el operando delante del valor (secuencia de operaciones)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    this.Calculate(); // Calcula la operación
                    total = Double.Parse(total_text.Text); // Asigna el valor devuelto como total
                }
                // Si el operando (a evaluar) es una potencia
                else if (operand == "x²")
                {
                    this.operand = next_operand; // Cambia el operando al operando siguiente
                    operation_text.Text += total_text.Text + "²"; // Devuelve el valor con su potencia (secuencia de operaciones)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    this.Calculate(); // Calcula la operación
                    total = Double.Parse(total_text.Text); // Asigna el valor devuelto como total
                }
                // Si el operando (a evaluar) es una 1/x
                else if (operand == "1/x")
                {
                    this.operand = next_operand; // Cambia el operando al operando siguiente
                    operation_text.Text += "1/" + total_text.Text; // Devuelve el valor con su operación (secuencia de operaciones)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    this.Calculate(); // Calcula la operación
                    total = Double.Parse(total_text.Text); // Asigna el valor devuelto como total
                }
                // Si el operando (a evaluar) es un porcentaje
                else if (operand == "%")
                {
                    this.operand = next_operand;  // Cambia el operando al operando siguiente
                    operation_text.Text += operand + total_text.Text; // Devuelve el valor con su operación(secuencia de operaciones)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    this.Calculate(); // Calcula la operación
                    total = Double.Parse(total_text.Text); // Asigna el valor devuelto como total
                }
                // Si el operando (a evaluar) no es especial (add, sub, mult, div)
                else
                {
                    operation_text.Text += total_text.Text + operand; // Devuelve el valor con su operación(secuencia de operaciones)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
            }
            // Si la secuencia de operaciones no está vacía
            else
            {
                // Si el último carácter de la secuencia es un Dígito
                if (Char.IsDigit(operation_text.Text.Last()))
                {
                    // Si el operando es √, x², 1/x, %
                    if (operand == "√" || operand == "x²" || operand == "1/x" || operand == "%")
                    {
                        this.operand = next_operand; // Cambia el operando al operando siguiente
                        operation_text.Text += next_operand; // Asigna a la secuencia el signo de operación
                        total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    }
                    // Si el operando (a evaluar) no es especial (add, sub, mult, div)
                    else
                    {
                        operation_text.Text += total_text.Text + next_operand; // Devuelve el valor con su operación(secuencia de operaciones)
                        this.Calculate(); // Calcula la operación
                        total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    }
                }
                // Si el último carácter de la secuencia es el signo de Igual (=)
                else if (operation_text.Text.Last().ToString().Equals("="))
                {
                    operation_text.Text = ""; // Limpia la secuencia
                    operation_text.Text += total_text.Text + next_operand; // Asigna a la secuencia el valor de la calculadora con su operando
                    this.Calculate(); // Calcula la operación 
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
                // Si el operando es √, x², 1/x, %
                else if (operand == "√" || operand == "x²" || operand == "1/x" || operand == "%")
                {
                    this.operand = next_operand; // Cambia el operando al operando siguiente
                    operation_text.Text += next_operand; // Asigna a la secuencia el signo de operación
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
                // Si el operando siguiente es √, %
                else if (next_operand == "√" || next_operand == "%")
                {
                    operation_text.Text += next_operand + total_text.Text; // Devuelve el valor con su operación(secuencia de operaciones)

                    // Asigna el valor de la calculadora de la operación a evaluar para calcular con OTRA operación
                    total_text.Text = next_operand == "√" ? this.operation.squaret(Double.Parse(total_text.Text), total).ToString() : this.operation.porcentage(Double.Parse(total_text.Text), total).ToString();
                    this.Calculate(); // Calcula la operación (En éste caso el total_text que sería el valor a operar ya está calculado con la operación anterior)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
                // Si el operando siguiente es x²
                else if (next_operand == "x²")
                {
                    operation_text.Text += total_text.Text + "²"; // Devuelve el valor con su operación(secuencia de operaciones)
                    // Asigna el valor de la calculadora de la operación a evaluar para calcular con OTRA operación
                    total_text.Text = this.operation.pot(Double.Parse(total_text.Text), total).ToString();
                    this.Calculate(); // Calcula la operación (En éste caso el total_text que sería el valor a operar ya está calculado con la operación anterior)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
                else if (next_operand == "1/x")
                {
                    operation_text.Text += "1/" + total_text.Text; // Devuelve el valor con su operación(secuencia de operaciones)
                    // Asigna el valor de la calculadora de la operación a evaluar para calcular con OTRA operación
                    total_text.Text = this.operation.onex(Double.Parse(total_text.Text), total).ToString();
                    this.Calculate(); // Calcula la operación (En éste caso el total_text que sería el valor a operar ya está calculado con la operación anterior)
                    total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                }
                // Si el último carácter de la secuencia NO es un dígito
                else
                {
                    // Si el valor de la calculadora está vacío
                    if (total_text.Text == "")
                    {
                        // Asigna a la secuencia de operaciones la secuencia de operaciones anterior PERO removiento el último carácter (que representaría el operando)
                        operation_text.Text = operation_text.Text.Remove(operation_text.Text.Length - 1);
                        operation_text.Text += next_operand; // Agrega a la secuencia de operaciones el operando
                    }
                    // Si la secuencia de la calculadora NO está vacía
                    else
                    {
                        // Asigna a la secuencia de operaciones el valor de la calculadora más su operando
                        operation_text.Text += total_text.Text + next_operand;
                        this.Calculate(); // Calcula la operación
                        total = Double.Parse(total_text.Text); // Asigna el valor como total para evaluar
                    }
                }
            }
        }

        // Control (Borra todo)
        private void BtnClearAllClick(object sender, RoutedEventArgs e)
        {
            operation_text.Text = "";
            total_text.Text = "0";
            total = 0;
            operand = "";
            next_operand = "";
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

        // Calculo (Basado en el operando)
        private void Calculate()
        {
            switch (operand)
            {
                case "+":
                    total_text.Text = this.operation.add(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "-":
                    total_text.Text = this.operation.sub(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "*":
                    total_text.Text = this.operation.mult(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "/":
                    total_text.Text = this.operation.div(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "√":
                    total_text.Text = this.operation.squaret(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "x²":
                    total_text.Text = this.operation.pot(total, Double.Parse(total_text.Text)).ToString();
                    break;
                case "1/x":
                    total_text.Text = (1 / total).ToString();
                    break;
                case "%":
                    total_text.Text = (total % Double.Parse(total_text.Text)).ToString();
                    break;
                default:
                    break;
            }
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
