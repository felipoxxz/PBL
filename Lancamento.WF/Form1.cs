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
        private double frames; // Incremento de tempo para a animação
        private double anguloLancamento; // Ângulo de lançamento em graus
        private double distancia, altura; // Posição inicial do alvo em X
        public Form1()
        {
            InitializeComponent();
            ConfigurarGrafico();
        }

        // Configuração do gráfico
        private void ConfigurarGrafico()
        {
            // Criação de "Series" que serão meu projétil e alvo
            chart1.Series.Add("Projétil");
            chart1.Series["Projétil"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series.Add("Alvo");
            chart1.Series["Alvo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            // Configuração para não exibir casas decimais nos eixos X e Y
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0";

            // Remover as linhas de fundo da ChartArea
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            // Configura a ChartArea para ser fixa
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 2000;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 8000;

            // Configurar propriedades de estilo do gráfico
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10);
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 10);
            chart1.ChartAreas[0].AxisX.Title = "Distância";
            chart1.ChartAreas[0].AxisY.Title = "Altura";

            chart1.Series["Projétil"].ChartType = SeriesChartType.Line;
            chart1.Series["Projétil"].Color = Color.Blue;
            chart1.Series["Projétil"].BorderWidth = 2;
            chart1.Series["Projétil"].MarkerStyle = MarkerStyle.Circle;

            chart1.Series["Alvo"].ChartType = SeriesChartType.Point;
            chart1.Series["Alvo"].Color = Color.Red;
            chart1.Series["Alvo"].MarkerSize = 10;
            chart1.Series["Alvo"].MarkerStyle = MarkerStyle.Circle;

            // Adicionar grade leve
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Remover a legenda padrão chamada "series"
            chart1.Legends.Add("Projétil");
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Limpa as séries do gráfico para um novo lançamento
            LimparGrafico();

            // Reinicializa variáveis para um novo lançamento
            frames = 0;
            distancia = 1000;
            altura = 4000;

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
            chart1.Series["Projétil"].Points.Clear();

            // Cálculo do Seno e Cosseno do ângulo
            double cos = CalculoAvancado.Cos(anguloLancamento);
            double sen = CalculoAvancado.Sen(anguloLancamento);

            // Velocidade Inicial do lançamento
            double velocidadeProjetil = Fisica.VelocidadeInicial(anguloLancamento);

            // Equações de movimento oblíquo do Projétil
            double x = Fisica.PosicaoProjetilX(velocidadeProjetil, cos, frames);
            double y = Fisica.PosicaoProjetilY(velocidadeProjetil, sen, frames);

            // Atualiza a posição do projétil no gráfico
            chart1.Series["Projétil"].Points.AddXY(x, y);

            // Limpa os pontos antigos do alvo
            chart1.Series["Alvo"].Points.Clear();

            // Equação da queda do Alvo
            altura = Fisica.PosicaoAlvoY(altura, frames);

            // Atualiza a posição do alvo no gráfico
            chart1.Series["Alvo"].Points.AddXY(distancia, altura);

            frames += 0.1;

            // Se atingir o solo, pare a animação
            if (y < 0)
            {
                ((Timer)sender).Stop();
            }

            // Verifica se a distância entre o projétil e o alvo é pequena o suficiente para considerar como acerto
            if (Math.Abs(x - distancia) <= 10 && Math.Abs(y - altura) <= 10)
            {
                // Verifica se o alvo foi atingido na subida ou na descida do lançamento
                if(Fisica.VerificarAcerto(x, velocidadeProjetil, anguloLancamento))
                {
                    ((Timer)sender).Stop();
                    MessageBox.Show($"Projétil acertou o alvo! \nLevou {frames:F1} segundos! \nO alvo foi atingido na subida do lançamento! \nA velocidade do projétil foi de {velocidadeProjetil:F1}m/s");
                }
                else
                {
                    ((Timer)sender).Stop();
                    MessageBox.Show($"Projétil acertou o alvo! \nLevou {frames:F1} segundos! \nO alvo foi atingido na descida do lançamento \nA velocidade do projétil foi de {velocidadeProjetil:F1}m/s");
                }
            }
        }

        // Limpa os pontos das séries do gráfico
        private void LimparGrafico()
        {
            chart1.Series["Projétil"].Points.Clear();
            chart1.Series["Alvo"].Points.Clear();
        }
    }
}
