using System.Text.Json.Serialization;

namespace RESTfulAPIPWEB.Entity
{
    public class ModoDisponibilizacao
    {
        public int ModoDisponibilizacaoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool IsForSale { get; set; } = false;
        public bool Ativo { get; set; } = true;

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
