using Lancamento.WF.Classes;
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
        private double tempo; // Incremento de tempo para a animação
        private double velocidadeProjetil = 300; // Velocidade inicial em m/s
        private double velocidadeAlvo = -50;
        private double anguloLancamento; // Ângulo de lançamento em graus
        private double projetilX, projetilY; // Posição do projétil no gráfico
        private double alvoX, alvoY; // Posição inicial do alvo em X

        public Form1()
        {
            InitializeComponent();
            chart1.Series.Add("Projectile");
            chart1.Series["Projectile"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series.Add("Target");
            chart1.Series["Target"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            // Configuração para exibir apenas uma casa decimal nos eixos X e Y
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0";

            // Remover as linhas de fundo da ChartArea
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            // Configura a ChartArea para ser fixa
            chart1.ChartAreas[0].AxisX.Minimum = 0; // Ajuste conforme necessário
            chart1.ChartAreas[0].AxisX.Maximum = 2000;  // Ajuste conforme necessário
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 4000; // Ajuste conforme necessário
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Limpa as séries do gráfico
            LimparGrafico();

            // Reinicializa variáveis
            tempo = 0;
            projetilX = 0;
            projetilY = 0;
            alvoX = 1000;
            alvoY = 4000;

            // Lê o ângulo de lançamento do TextBox
            if (double.TryParse(Angulo.Text, out anguloLancamento))
            {
                Timer timer = new Timer();
                timer.Interval = 100; // Intervalo em milissegundos
                timer.Tick += new EventHandler(AtualizarPosicao);
                timer.Start();
            }
            else
            {
                MessageBox.Show("Por favor, insira um ângulo válido.");
            }
        }

        private void AtualizarPosicao(object sender, EventArgs e)
        {
            // Limpa os pontos antigos do projétil
            chart1.Series["Projectile"].Points.Clear();

            //velocidadeProjetil = (-(alvoX * velocidadeAlvo) / Math.Cos(radianos) + Math.Sqrt((alvoX * velocidadeAlvo) / Math.Cos(radianos) - 4.0 * (alvoX * Math.Tan(radianos) - alvoY) * (-4.9 * Math.Pow(alvoX, 2)) / Math.Pow(Math.Cos(radianos), 2))) / 2 * (alvoX * Math.Tan(radianos) - alvoY);

            // Cálculo do Seno e Cosseno do ângulo
            double cos = CalculoAvancado.Cos(anguloLancamento);
            double sen = CalculoAvancado.Sen(anguloLancamento);

            // Equações de movimento oblíquo
            double x = Fisica.PosicaoProjetilX(velocidadeProjetil, cos, tempo);
            double y = Fisica.PosicaoProjetilY(velocidadeProjetil, sen, tempo);

            // Atualiza a posição do projétil no gráfico
            chart1.Series["Projectile"].Points.AddXY(x, y);

            // Limpa os pontos antigos do alvo
            chart1.Series["Target"].Points.Clear();

            // Equação da queda do Alvo
            alvoY = Fisica.PosicaoAlvoY(velocidadeAlvo, alvoY);

            // Atualiza a posição do alvo no gráfico
            chart1.Series["Target"].Points.AddXY(alvoX, alvoY);

            tempo += 0.1;

            // Se atingir o solo, pare a animação
            if (y < 0)
            {
                ((Timer)sender).Stop();
            }

            // Verifica se a distância entre o projétil e o alvo é pequena o suficiente para considerar como acerto
            if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y - alvoY, 2)) < 1.0)
            {
                ((Timer)sender).Stop();
                MessageBox.Show("Projétil atingiu o alvo!");
            }
        }

        private void LimparGrafico()
        {
            // Limpa os pontos das séries do gráfico
            chart1.Series["Projectile"].Points.Clear();
            chart1.Series["Target"].Points.Clear();
        }
    }
}
