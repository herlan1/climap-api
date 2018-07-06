using System;
using System.Collections.Generic;
using System.Text;

namespace Climap.Dominio.Questionarios
{
    public class Pergunta
    {
        public Pergunta()
        {
            Respostas = new List<Resposta>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Resposta> Respostas { get; set; }
    }
}
