using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesAPI.Helper
{
    public class Banco
    {
        private SqlConnection conexao;

        public SqlDataReader Consultar(string sqlConsulta)
        {

            SqlCommand comando = new SqlCommand(sqlConsulta, conexao);

            return comando.ExecuteReader();
        }

        public void executarSQL(string sqlExecutar)
        {
            SqlCommand comando = new SqlCommand(sqlExecutar, conexao);

            comando.ExecuteNonQuery();
        }

        public Banco()
        {
            SqlConnectionStringBuilder stringConection = new SqlConnectionStringBuilder();

            stringConection.DataSource = "servertesteploomes.database.windows.net";
            stringConection.UserID = "adm";
            stringConection.Password = "tiago@1234";
            stringConection.InitialCatalog = "Ploomes";

            conexao = new SqlConnection(stringConection.ConnectionString);

            conexao.Open();
        }
    }
}
