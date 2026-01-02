using Microsoft.AspNetCore.Components;
using RCLAPI.DTO;
using RCLAPI.Services;
using RCLProdutos.Services.Interfaces;

namespace RCLProdutos.Shared.Cards
{
    public partial class CardsCarousel
    {
        [Parameter]
        public int SelectedId { get; set; }

        [Parameter]
        public int catSel { get; set; }

        [Inject]
        public IApiServices _apiServices { get; set; }

        [Inject]
        public ICardsUtilsServices cardsUtilsServices { get; set; }
        [Parameter] public int? TipoCategoriaId { get; set; }

        private List<Categoria> categorias { get; set; } = new();

        private bool IsDisabledNext { get; set; } = false;
        private bool IsDisbledPrevious { get; set; } = false;

        private int SelectCategoria;
        private int enviaCat;

        private int currentIndex = 0;

        // AJUSTA ESTE VALOR ao “tamanho real” de 1 card (largura + gap).
        // Normalmente 220~280 costuma bater certo.
        private const int StepPx = 240;

        private string TrackStyle => $"transform: translateX(-{currentIndex * StepPx}px); transition: transform 450ms ease;";

        private bool NextDisabled => categorias == null || currentIndex >= Math.Max(0, categorias.Count - 1);

        // para não “ir infinito”. (simples e funciona)
        private bool PrevDisabled => currentIndex <= 0;
        [Parameter] public int SelectedCategoriaId { get; set; }
        [Parameter] public EventCallback<int> SelectedCategoriaIdChanged { get; set; }



        protected override void OnInitialized()
        {
            cardsUtilsServices.OnChange += StateHasChanged;
        }
        private async Task SelecionarCategoria(int id)
        {
            SelectCategoria = id; // mantém highlight/seleção se já tens
            await SelectedCategoriaIdChanged.InvokeAsync(id);
        }


        protected override async Task OnParametersSetAsync()
        {
            // reset estado do slider (muito importante)
            cardsUtilsServices.CountSlide = 0;
            cardsUtilsServices.Index = 0;
            cardsUtilsServices.MarginLeftSlide.Clear();

            IsDisabledNext = false;
            IsDisbledPrevious = false;
            SelectCategoria = 0;

            // carregar categorias filtradas pelo tipo selecionado
            categorias = await _apiServices.GetCategorias(TipoCategoriaId) ?? new List<Categoria>();

            // alinhar margens com a nova lista (à prova de bala)
            cardsUtilsServices.MarginLeftSlide.Clear();
            for (int i = 0; i < categorias.Count; i++)
                cardsUtilsServices.MarginLeftSlide.Add("margin-left:0%");

            // se por alguma razão ainda não bate certo, força
            if (cardsUtilsServices.MarginLeftSlide.Count < categorias.Count)
            {
                while (cardsUtilsServices.MarginLeftSlide.Count < categorias.Count)
                    cardsUtilsServices.MarginLeftSlide.Add("margin-left:0%");
            }

            StateHasChanged();
        }

        void EnsureMargins()
        {
            cardsUtilsServices.MarginLeftSlide.Clear();

            for (int i = 0; i < categorias.Count; i++)
                cardsUtilsServices.MarginLeftSlide.Add("margin-left:0%");
        }


        void PreviousCard()
        {
            if (currentIndex > 0)
                currentIndex--;
        }

        void NextCard()
        {
            if (!IsDisabledNext)
                currentIndex++;
        }
    }
}
