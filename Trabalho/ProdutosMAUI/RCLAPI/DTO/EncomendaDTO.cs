using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLAPI.DTO
{
    public class EncomendaDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProdutoId { get; set; }
        

        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Enviada { get; set; }
        public string Estado { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFinalizacao { get; set; }

        public bool EmStock { get; set; }
        public bool Paga { get; set; }

    }
}
