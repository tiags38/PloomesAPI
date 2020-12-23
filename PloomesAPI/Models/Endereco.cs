using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesAPI.Models
{
    public class Endereco
    {
        /*public Endereco(string cep, string cidade, string estado, string pais, string numero, string logradouro, string bairro)
        {
            this.cep = cep;
            this.cidade = cidade;
            this.estado = estado;
            this.pais = pais;
            this.numero = numero;
            this.logradouro = logradouro;
            this.bairro = bairro;
        }*/

        public string cep { get; set; }
		public string cidade { get; set; }
		public string estado { get; set; }
		public string pais { get; set; }
		public string numero { get; set; }
		public string logradouro { get; set; }
		public string bairro { get; set; }
	}
}
