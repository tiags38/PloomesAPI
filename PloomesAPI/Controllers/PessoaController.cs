using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PloomesAPI.Helper;
using PloomesAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PloomesAPI.Controllers
{
   
    public class PessoaController : ApiController
    {
        private SqlDataReader dados; 

        [Route("api/pessoa/cadastro")]
        [HttpPost]
        public Resposta cadastro([FromBody]Pessoa pessoa)
        {
            Resposta resposta = new Resposta();

            try
            {       
                
                Banco banco = new Banco();
                string sql = $@"SELECT P.* FROM PESSOA P WHERE P.CPF = {pessoa.cpf}";

                dados = banco.Consultar(sql);

                if (!dados.HasRows)
                {
                    dados.Close();

                    sql = $@"
                            INSERT INTO ENDERECO(   bairro,
                                                    cep,
                                                    cidade,
                                                    estado,
                                                    logradouro,
                                                    numero,
                                                    pais) 
                                        VALUES (   '{pessoa.endereco.bairro}',
                                                   '{pessoa.endereco.cep}',
                                                   '{pessoa.endereco.cidade}',
                                                   '{pessoa.endereco.estado}',
                                                   '{pessoa.endereco.logradouro}',
                                                   '{pessoa.endereco.numero}', 
                                                   '{pessoa.endereco.pais}')";

                    banco.executarSQL(sql);

                    sql = $@"
                    SELECT TOP 1 
                        ID_ENDERECO 
                    FROM ENDERECO 
                    WHERE 
                        bairro      = '{pessoa.endereco.bairro}' AND 
                        cep         = '{pessoa.endereco.cep}' AND
                        cidade      = '{pessoa.endereco.cidade}' AND 
                        estado      = '{pessoa.endereco.estado}' AND 
                        logradouro  = '{pessoa.endereco.logradouro}' AND 
                        numero      = '{pessoa.endereco.numero}' AND 
                        pais        = '{pessoa.endereco.pais}'";

                    dados = banco.Consultar(sql);

                    dados.Read();

                    sql = $@"
                    INSERT INTO PESSOA( ativo, 
                                        cpf, 
                                        documentoidentitdade,
                                        nascimento, 
                                        nomecompleto, 
                                        sexo, 
                                        id_endereco)
                                VALUES  (1,
                                        '{pessoa.cpf}',
                                        '{pessoa.documentoidentitdade}',
                                        '{pessoa.nascimento.ToString("dd/MM/yyyy")}',
                                        '{pessoa.nomecompleto}',
                                        '{pessoa.sexo}',
                                        {dados["ID_ENDERECO"]})";

                    dados.Close();

                    banco.executarSQL(sql);

                    resposta.mensagem = "Requisição realizada com sucesso";
                    resposta.status = true;

                }
                else
                {
                    resposta.mensagem = "Requisição não foi realizada com sucesso, devido a CPF ja existente.";
                    resposta.status = false;
                }

                

            }
            catch (Exception e)
            {
                resposta.mensagem = "Não foi possível realizar a requisição, devido a " + e.Message;
                resposta.status = false;
            }

            return resposta;
        }

        [Route("api/pessoa/consulta")]
        [HttpPost]
        public Resposta consulta ([FromBody] Cpf cpf)
        {
            Resposta resposta = new Resposta();

            try
            {
                Banco banco = new Banco();
             
                string sql = $@" select * 
                                from pessoa p
                                inner join endereco e on (p.id_endereco = e.id_endereco)
                                where ativo = 1 ";

                if (cpf != null)
                {
                    sql += " and cpf = '" + cpf.cpf + "'";

                    cpf = null;
                }

                dados = banco.Consultar(sql);

                var listaPessoas = new List<Pessoa>();

                Endereco endereco;
                Pessoa pessoa;

                while (dados.Read())
                {
                    endereco = new Endereco();

                    endereco.cep = dados["cep"].ToString();
                    endereco.cidade = dados["cidade"].ToString();
                    endereco.estado = dados["estado"].ToString();
                    endereco.pais = dados["pais"].ToString();
                    endereco.numero = dados["numero"].ToString();
                    endereco.logradouro = dados["logradouro"].ToString();
                    endereco.bairro = dados["bairro"].ToString();

                    pessoa = new Pessoa();

                    pessoa.nomecompleto = dados["nomecompleto"].ToString();
                    pessoa.documentoidentitdade = dados["documentoidentitdade"].ToString();
                    pessoa.cpf = dados["cpf"].ToString();
                    pessoa.nascimento = Convert.ToDateTime(dados["nascimento"]);
                    pessoa.sexo = dados["sexo"].ToString();
                    pessoa.ativo = dados["ativo"].ToString();
                    pessoa.endereco = endereco;

                    listaPessoas.Add(pessoa);
                }

                resposta.mensagem = "Requisição realizada com sucesso";
                resposta.status = true;
                resposta.dados = listaPessoas;                                
            }
            catch (Exception e )
            {
                resposta.mensagem = "Não foi possível realizar a requisição, devido a " + e.Message;
                resposta.status = false;
            }

            return resposta;
        }
       
    }
}
