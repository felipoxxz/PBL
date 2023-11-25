using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lancamento.WF.Classes
{
    public class Fisica
    {
        private const double gravidade = 9.8; // Aceleração devido à gravidade em m/s^2
        private const double velocidadeAlvo = 50; // Velocidade do Meteoro

        // Cálcula a Posição do Projétil no eixo X
        public static double PosicaoProjetilX(double velocidadeProjetil, double cosseno, double tempo )
        {
            double x = velocidadeProjetil  * cosseno * tempo;
            return x;
        }

        // Cálcula a Posição do Projétil no eixo Y
        public static double PosicaoProjetilY(double velocidadeProjetil, double seno, double tempo)
        {
            double y = (velocidadeProjetil * seno * tempo) - (0.5 * gravidade * tempo * tempo);
            return y;
        }

        // Cálcula a Posição do Alvo no eixo Y
        public static double PosicaoAlvoY(double alvoY, double tempo)
        {
            alvoY = 4000;
            double altura = alvoY - (velocidadeAlvo * tempo);
            return altura; 
        }
        public static double VelocidadeInicial(double angulo)
        {
            double a, b, c, delta, velocidadeInicialDeltaNegativo, velocidadeInicialDeltaPositivo;
            double tempoDeltaN, tempoDeltaP, alturaImpactoDeltaN, alturaImpactoDeltaP;
            double distancia = 1000;
            double altura = 4000;

            // Cálculo da Velocidade
            a = distancia * CalculoAvancado.Tan(angulo) - altura;
            b = (distancia * velocidadeAlvo) / CalculoAvancado.Cos(angulo);
            c = (-4.9 * distancia * distancia) / (CalculoAvancado.Cos(angulo) * CalculoAvancado.Cos(angulo));
            
            delta = Math.Pow(b, 2) - (4 * a * c);

            velocidadeInicialDeltaPositivo = (-1.0 * b + Math.Sqrt(delta)) / (2.0 * a);
            velocidadeInicialDeltaNegativo = (-1.0 * b - Math.Sqrt(delta)) / (2.0 * a);

            // Cálculo do Tempo
            tempoDeltaN = distancia / (velocidadeInicialDeltaNegativo * CalculoAvancado.Cos(angulo));
            tempoDeltaP = distancia / (velocidadeInicialDeltaPositivo * CalculoAvancado.Cos(angulo));

            // Cálculo da Altura
            alturaImpactoDeltaN = altura + velocidadeAlvo * tempoDeltaN;
            alturaImpactoDeltaP = altura + velocidadeAlvo * tempoDeltaP;

            // Filtros
            if (tempoDeltaP > 0 && alturaImpactoDeltaP > 0 && velocidadeInicialDeltaPositivo > 0) 
            {
                return velocidadeInicialDeltaPositivo;
            }
            else if (tempoDeltaN > 0 && alturaImpactoDeltaN > 0 && velocidadeInicialDeltaNegativo > 0)
            {
                return velocidadeInicialDeltaNegativo;
            }
            else
            {
                return 0;
            }
        }
        
        // Verifica se o alvo foi atingido na subida ou na descida do lançamento
        public static Boolean VerificarAcerto(double x, double velocidadeInicial, double angulo)
        {
            double alcance;
            alcance = (Math.Pow(velocidadeInicial, 2) / gravidade) * CalculoAvancado.Sen(2 * angulo);

            if(x < alcance/2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
