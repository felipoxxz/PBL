using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lancamento.WF.Classes
{
    public class Dados
    {
        //Atributos para buscar variaveis
        public string Angulo { get; set; }
        public double VelocidadeIncial { get; set; }

        public int CodColisao { get; set; }

        //Método pra chamar Stored Procedure de Consulta
        public DataTable Buscar(string angulo)
        {
            var dt = new DataTable();
            //TRY para caso dê erro
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.strConn))
                {
                    //Abre a Connection com o SQL Server
                    cn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(null, cn))
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //Nome da Stored Procedure
                        da.SelectCommand.CommandText = "ConsultarLancamento";
                        da.SelectCommand.Parameters.AddWithValue("@anguloTiro", CodColisao);
                        //Insere valor na variavel dt
                        da.Fill(dt);
                    }
                }
            }
            //Mensagem do CATCH para caso acontença erro
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir dados!" + ex.Message);
            }

            return dt;
        }
        //Método pra chamar Stored Procedure de Inserção
        private int Inserir(Dados dado)
        {
            int id = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.strConn))
                {
                    //Abre a Connection com o SQL Server
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(null, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        /q
                        cmd.CommandText = "CriarLancamento";

                        cmd.Parameters.AddWithValue("@VelocidadeMeteoro", 50);
                        cmd.Parameters.AddWithValue("@VelocidadeTiro", VelocidadeIncial);
                        cmd.Parameters.AddWithValue("@distanciaColisao", 1000);
                        cmd.Parameters.AddWithValue("@alturaColisao", 10);
                        cmd.Parameters.AddWithValue("@anguloTiro", Angulo);
                        cmd.Parameters.AddWithValue("@alturaMeteoro", 4000);

                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Falha ao inserir dados!\n\n" + ex.Message);
                return 0;
            }
            return id;
        }

        public DataTable Deletar(int CodColisao)
        {
            var dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.strConn))
                {
                    cn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(null, cn))
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.CommandText = "RemoverLancamento";
                        da.SelectCommand.Parameters.AddWithValue("@anguloTiro", CodColisao);

                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir dados!" + ex.Message);
            }

            return dt;
        }


    }
}
