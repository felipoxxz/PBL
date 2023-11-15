using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lancamento.WF
{
    public partial class Form1 : Form
    {
        private const double Gravidade = 9.8; // Aceleração devido à gravidade em m/s^2
        private double tempo = 0.1; // Incremento de tempo para a animação
        private double velocidadeProjetil = 30.0; // Velocidade inicial em m/s
        private double anguloLancamento = 45.0; // Ângulo de lançamento em graus
        private double posX, posY; // Posição do objeto no gráfico
        private double alvoX = 50.0; // Posição inicial do alvo em X
        private double alvoY = 100.0; // Posição inicial do alvo em Y
        private double velocidadeAlvo = -2.0; // Velocidade constante do alvo

        public Form1()
        {
            InitializeComponent();
            chart1.Series.Add("Projectile");
            chart1.Series["Projectile"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series.Add("Target");
            chart1.Series["Target"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 100; // Intervalo em milissegundos
            timer.Tick += new EventHandler(AtualizarPosicao);
            timer.Start();
        }

        private void AtualizarPosicao(object sender, EventArgs e)
        {
            double radianos = anguloLancamento * (Math.PI / 180.0);

            // Equações de movimento oblíquo
            double x = velocidadeProjetil * Math.Cos(radianos) * tempo;
            double y = (velocidadeProjetil * Math.Sin(radianos) * tempo) - (0.5 * Gravidade * Math.Pow(tempo, 2));

            // Atualiza a posição no gráfico
            chart1.Series["Projectile"].Points.AddXY(x, y);

            // Limpa os pontos antigos da série "Target"
            chart1.Series["Target"].Points.Clear();

            // Atualiza a posição do alvo no gráfico
            chart1.Series["Target"].Points.AddXY(alvoX, alvoY);

            tempo += 0.1;

            alvoY += velocidadeAlvo;

            // Se atingir o solo, pare a animação
            if (y < 0)
            {
                ((Timer)sender).Stop();
            }

            if (Math.Abs(x) < 1 && Math.Abs(y - alvoY) < 1)
            {
                ((Timer)sender).Stop();
                MessageBox.Show("Projétil atingiu o alvo!");
            }
        }
    }
}
