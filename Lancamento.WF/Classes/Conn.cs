using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lancamento.WF.Classes
{
    //Classe para alterar o Banco de Dados Utilizado
    //Alterar de máquina para máquina
    public static class Conn
    {
        //Configuração utilizada para Autenticação com o Windows

        public static readonly string strConn = @"data source=DESKTOP-P66K1K8\SQLEXPRESS;initial catalog=LancamentoBalistico;integrated security=SSPI";
        //Para configuração nas máquinas da Faculdade Engenheiro Salvador Arena:
        //@"data source = NomeServidor\SQLExpress;Integrated Security=False;Intial Catalog=LancamentoBalistico;user ID=sa; Password=123456"
        //Substitua o data source pelo nome do servidor da maquina correspondente
    }
}
