using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lancamento.Class
{
    public class Usuario
    {
        public double ObterVelocidadeLancamento()
        {
            Console.Write("Digite a velocidade do lançamento (em m/s): ");
            double velocidade;
            while (!Double.TryParse(Console.ReadLine(), out velocidade) || velocidade <= 0)
            {
                Console.WriteLine("Por favor, digite uma velocidade válida maior que zero.");
                Console.Write("Digite a velocidade do lançamento (em m/s): ");
            }
            return velocidade;
        }

        public double ObterAnguloLancamento()
        {
            Console.Write("Digite o ângulo de lançamento (em graus): ");
            double angulo;
            while (!Double.TryParse(Console.ReadLine(), out angulo) || angulo <= 0 || angulo >= 90)
            {
                Console.WriteLine("Por favor, digite um ângulo válido entre 0 e 90 graus.");
                Console.Write("Digite o ângulo de lançamento (em graus): ");
            }
            return angulo;
        }
    }
}
