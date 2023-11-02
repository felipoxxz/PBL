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
        public Form1()
        {
            InitializeComponent();

            // Cria um novo gráfico
            Chart chart = new Chart();
            chart.Size = new System.Drawing.Size(1920, 1080);

            // Cria uma nova área de gráfico
            ChartArea chartArea1 = new ChartArea();
            chartArea1.AxisX.Minimum = -2; // Define o valor mínimo do eixo X
            chartArea1.AxisX.Maximum = 2;  // Define o valor máximo do eixo X
            chartArea1.AxisY.Minimum = -2;  // Define o valor mínimo do eixo Y
            chartArea1.AxisY.Maximum = 2;   // Define o valor máximo do eixo Y

            // Adiciona a área de gráfico ao gráfico
            chart.ChartAreas.Add(chartArea1);

            // Adiciona o gráfico ao controle de formulário
            this.Controls.Add(chart);

            // Adiciona uma série ao gráfico
            Series series = new Series();
            chart.Series.Add(series);

            // Adiciona alguns pontos ao gráfico (por exemplo, uma função seno)
            for (double x = -2; x <= 2; x += 0.1)
            {
                double y = Math.Sin(x); // Calcula o valor y usando a função seno
                series.Points.AddXY(x, y); // Adiciona o ponto ao gráfico
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // Obtenha a velocidade e o ângulo do usuário dos TextBox
            if (double.TryParse(txtVelocidade.Text, out double velocidade) &&
                double.TryParse(txtAngulo.Text, out double angulo))
            {
                MessageBox.Show(txtVelocidade.Text);
            }
            else
            {
                MessageBox.Show("Por favor, insira valores válidos para a velocidade e o ângulo.");
            }
        }
    }
}
