using Microsoft.AspNetCore.Components;

using RCLAPI.DTO;

namespace RCLProdutos.Shared.Cards
{
    public partial class CardComponent
    {
        [Parameter]
        public Categoria? categoria { get; set; }

        [Parameter]
        public int? selectedCatId { get; set; }    

        [Parameter]
        public string? marginLeft { get; set; }

        public int? selectedCategoriaId { get; set; }
        [Parameter] public EventCallback<int> OnSearchClick { get; set; }


        protected override async Task OnInitializedAsync()
        {
            selectedCategoriaId = selectedCatId;
        }
        protected string GetStyle()
        {
            var img = !string.IsNullOrWhiteSpace(categoria?.UrlImagem)
                ? categoria!.UrlImagem
                : "images/category-placeholder.png";

            return $"background-image:url('{img}'); {marginLeft} background-position:center; background-size:cover;";
        }

        private void Navega(Categoria categoria)
        {
            NavigationManager.NavigateTo($"slider?Id={categoria.Id}&nomeCat={categoria.Nome}");
        }
    }
}
