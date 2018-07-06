using System;
using System.Collections.Generic;
using System.Text;

namespace Climap.Dominio.Questionarios
{
    public class Resposta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Peso Peso { get; set; }
    }
}
