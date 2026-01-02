using System.Text.Json.Serialization;

namespace GestaoLoja.Entity
{
    public class ModoDisponibilizacao
    {
        public int Id { get; set; }

        public bool IsForSale { get; set; }

        public bool Ativo { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
