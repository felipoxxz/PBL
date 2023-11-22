using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
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
        public static double PosicaoProjetilX(double velocidadeProjetil, double cosseno, double tempo )
        {
            double x = (velocidadeProjetil / 10) * cosseno * tempo;
            return x;
        }

        public static double PosicaoProjetilY(double velocidadeProjetil, double seno, double tempo)
        {
            double y = ((velocidadeProjetil / 10) * seno * tempo) - (0.5 * (gravidade / 10) * Math.Pow(tempo, 2));
            return y;
        }

        public static double PosicaoAlvoY(double velocidadeAlvo, double alvoY)
        {
            alvoY += (velocidadeAlvo / 10);
            return alvoY;
        }
        public static double VelocidadeInicial(double angulo)
        {
            double a, b, c, delta, velocidadeInicialDeltaNegativo, velocidadeInicialDeltaPositivo;
            double tempo_delta_n, tempo_delta_p, altura_impacto_delta_n, altura_impacto_delta_p;
            double distancia = 20;
            double altura = 60;
            double velocidadeAlvo = 10;

            // Cálculo da velocidade
            a = distancia * (CalculoAvancado.Sen(angulo) / CalculoAvancado.Cos(angulo)) - altura;
            b = (distancia * velocidadeAlvo) / CalculoAvancado.Cos(angulo);
            c = (-4.9 * Math.Pow(distancia, 2)) / Math.Pow(CalculoAvancado.Cos(angulo),2);

            delta = Math.Pow(b, 2) - (4 * a * c);

            velocidadeInicialDeltaPositivo = -1.0 * b + Math.Sqrt(delta) / (2.0 * a);
            velocidadeInicialDeltaNegativo = (-1.0 * b - Math.Sqrt(delta)) / (2.0 * a);

            // TEMPO
            tempo_delta_n = distancia / (velocidadeInicialDeltaNegativo * CalculoAvancado.Cos(angulo));
            tempo_delta_p = distancia / (velocidadeInicialDeltaPositivo * CalculoAvancado.Cos(angulo));

            // ALTURA
            altura_impacto_delta_n = altura + velocidadeAlvo * tempo_delta_n;
            altura_impacto_delta_p = altura + velocidadeAlvo * tempo_delta_p;

            // FILTROS
            if (tempo_delta_p > 0 && altura_impacto_delta_p > 0)
            {
                return velocidadeInicialDeltaPositivo;
            }
            else if (tempo_delta_n > 0 && altura_impacto_delta_n > 0)
            {
                return velocidadeInicialDeltaNegativo;
            }
            else
            {
                return 0;
            }
        }
    }
}
