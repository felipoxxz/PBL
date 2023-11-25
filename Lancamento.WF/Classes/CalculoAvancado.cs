using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lancamento.WF
{
    public class CalculoAvancado
    {
        // Cálcula o Cosseno do ângulo
        public static double Cos(double angulo, double precisaoDesejada = 1e-20)
        {
            double f = 0;
            int k = 0;
            int cont = 0;
            double Resto;
            angulo = ConverteAngulo(angulo);
            while (true)
            {
                if (k % 2 == 0)
                {
                    f += (Math.Pow(-1, cont) / CalculoAvancado.Fatorial(2 * cont)) * Math.Pow(angulo, 2 * cont); // Valor genérico da função cos(x) em MacLaurin                   
                    cont += 1;
                } // As potências ímpares não aparecem, logo se considera apenas as pares          
                Resto = (1 / CalculoAvancado.Fatorial(k + 1)) * Math.Pow(Math.Abs(angulo), k + 1);
                if (Resto <= precisaoDesejada)
                {
                    break;
                }
                k++;
            }
            return f;
        }

        // Cálcula o Seno do ângulo
        public static double Sen(double angulo, double precisaoDesejada = 1e-20)
        {
            double f = 0;
            int k = 0;
            int cont = 0;
            double Resto;
            angulo = ConverteAngulo(angulo);
            while (true)
            {
                if (k % 2 == 0)
                {
                    f += (Math.Pow(-1, cont) / CalculoAvancado.Fatorial(2 * cont + 1)) * Math.Pow(angulo, 2 * cont + 1); // Valor genérico da função sen(x) em MacLaurin                   
                    cont += 1;
                }
                Resto = (1 / CalculoAvancado.Fatorial(k + 1)) * Math.Pow(Math.Abs(angulo), k + 1);
                if (Resto <= precisaoDesejada)
                {
                    break;
                }
                k++;
            }
            return f;
        }

        // Cálcula a Tangente do ângulo
        public static double Tan(double angulo, double precisaoDesejada = 1e-20)
        {
            return Sen(angulo, precisaoDesejada) / Cos(angulo, precisaoDesejada);
        }

        // Cálcula do Fatorial 
        public static double Fatorial(int x)
        {
            if (x == 0) return 1;
            int fatorial = x;
            for (int i = x; i > 1; i--)
            {
                fatorial *= i - 1;
            }
            return fatorial;
        }

        // Converte o ângulo degraus para radianos
        public static double ConverteAngulo(double AnguloEmGraus) // Função que converte Angulo de Graus para Radianos
        {
            double AnguloEmRad = AnguloEmGraus * Math.PI / 180;
            return AnguloEmRad;
        }
    }
}
