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
    }
}
