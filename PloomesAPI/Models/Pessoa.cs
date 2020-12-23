using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesAPI.Models
{
    public class Pessoa
    {
        public string nomecompleto { get; set; }
		public string documentoidentitdade { get; set; }
		public string cpf { get; set; }
		public DateTime nascimento { get; set; }
		public string sexo { get; set; }
		public string ativo { get; set; }
		public Endereco endereco { get; set; }
    }
}
