using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lancamento.Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario = new Usuario();

            //Obter velocidade
            double velocidade = usuario.ObterVelocidadeLancamento();

            //Obter ângulo
            double angulo = usuario.ObterAnguloLancamento();

        }
    }
}
