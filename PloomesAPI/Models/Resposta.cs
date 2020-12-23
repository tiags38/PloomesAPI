using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesAPI.Models
{
    public class Resposta
    {
        public string mensagem { get; set; }
        public bool status { get; set; }
        public List<Pessoa> dados { get; set; }
    }
}
