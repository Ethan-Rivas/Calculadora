using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Parcial1
{
    /* TODO:
    * Permitir:
    * Elevar a la N potencia.
    * Raíz N de un número.
    * Extra: Permitir el porcentaje correctamente en sus 3 formas.
    */
    class Operation
    {
        // Suma
        public Double add(Double a, Double b)
        {
            return a + b;
        }
        // Resta
        public Double sub(Double a, Double b)
        {
            return a - b;
        }
        // Multiplicación
        public Double mult(Double a, Double b)
        {
            return a * b;
        }
        // División
        public Double div(Double a, Double b)
        {
            return a / b;
        }
        // Raíz
        public Double squaret(Double a, Double b)
        {
            return Math.Sqrt(a);
        }
        // Potencia
        public Double pot(Double a, Double b)
        {
            return a * a;
        }
        // Inverso
        public Double onex(Double a, Double b)
        {
            return 1 / a;
        }
        // Porcentaje
        public Double porcentage(Double a, Double b)
        {
            return a % a;
        }
    }
}
