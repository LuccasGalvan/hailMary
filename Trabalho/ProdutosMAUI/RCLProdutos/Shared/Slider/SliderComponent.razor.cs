using Microsoft.AspNetCore.Components;
using RCLAPI.DTO;
using RCLAPI.Services;
using RCLProdutos.Services.Interfaces;
using System.Globalization;

namespace RCLProdutos.Shared.Slider
{
    public partial class SliderComponent
    {

        [Parameter]
        public int? initProd { get; set; }

        [Inject]
        public IApiServices? _apiServices { get; set; }

        [Inject]
        public ISliderUtilsServices sliderUtilsService { get; set; }

        // listas inicializadas, não-null
        private List<ProdutoDTO> produtos { get; set; } = new();
        private List<ProdutoDTO> userFavoritos { get; set; } = new();

        public ProdutoDTO sugestaoProduto = new ProdutoDTO();
        private int witdthPerc { get; set; } = 0;
        private bool IsDisabledNext { get; set; } = false;
        private bool IsDisbledPrevious { get; set; } = false;

        public int? actualProd = 0;
        [Parameter] public int? CategoriaId { get; set; }


        private int? _lastCategoriaId = null;

        protected override async Task OnParametersSetAsync()
        {
            // evita reload infinito se nada mudou
            if (_lastCategoriaId == CategoriaId && produtos.Count > 0)
                return;

            _lastCategoriaId = CategoriaId;

            // RESET do slider (sempre que a lista muda)
            sliderUtilsService.MarginLeftSlide.Clear();
            sliderUtilsService.CountSlide = 0;
            sliderUtilsService.Index = 0;
            IsDisabledNext = false;
            IsDisbledPrevious = false;

            int? categoriasenviadaID;
            string produtosEspecificos;

            // ✅ NOVA REGRA: se CategoriaId vier null -> "todos"
            if (CategoriaId == null || CategoriaId == 0)
            {
                produtosEspecificos = "todos";
                categoriasenviadaID = null;
            }
            else
            {
                produtosEspecificos = "categoria";
                categoriasenviadaID = CategoriaId.Value;
            }

            try
            {
                if (_apiServices == null)
                {
                    Console.WriteLine("IApiServices não foi injetado.");
                    produtos = new List<ProdutoDTO>();
                    return;
                }

                produtos = await _apiServices
                                .GetProdutosEspecificos(produtosEspecificos, categoriasenviadaID)
                           ?? new List<ProdutoDTO>();

                // garante coerência: MarginLeftSlide tem sempre o mesmo tamanho que produtos
                sliderUtilsService.MarginLeftSlide.Clear();
                for (int i = 0; i < produtos.Count; i++)
                    sliderUtilsService.MarginLeftSlide.Add("margin-left:0%");


                if (produtos.Count == 0)
                    return;

                if (await _apiServices.IsUserLoggedIn())
                {
                    userFavoritos = await _apiServices.GetFavoritos() ?? new List<ProdutoDTO>();

                    for (int i = 0; i < userFavoritos.Count; i++)
                        for (int j = 0; j < produtos.Count; j++)
                            if (produtos[j].Id == userFavoritos[i].Id)
                                produtos[j].Favorito = true;
                }

                // sugestão aleatória
                Random random = new Random();
                sugestaoProduto = produtos[random.Next(produtos.Count)];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                produtos = new List<ProdutoDTO>();
                return;
            }

            int qtdProd = produtos.Count;
            witdthPerc = qtdProd * 100;
            sliderUtilsService.WidthSlide2 = 100f / qtdProd;

            // para não duplicar subscriptions
            sliderUtilsService.OnChange -= StateHasChanged;
            sliderUtilsService.OnChange += StateHasChanged;
        }

        void PreviousSlide()
        {
            if (sliderUtilsService.CountSlide != 0)
            {
                sliderUtilsService.MarginLeftSlide[sliderUtilsService.CountSlide - 1] = "margin-left:0%";
                sliderUtilsService.CountSlide--;
                IsDisabledNext = false;
                IsDisbledPrevious = false;
            }
            else
            {
                if (sliderUtilsService.MarginLeftSlide.Count > 0)
                    sliderUtilsService.MarginLeftSlide[0] = "margin-left:0%";

                IsDisbledPrevious = true;
            }
            sliderUtilsService.Index = sliderUtilsService.CountSlide;
        }

        void NextSlide()
        {
            sliderUtilsService.CountSlide++;
            sliderUtilsService.Index = sliderUtilsService.CountSlide;

            if (sliderUtilsService.CountSlide < sliderUtilsService.MarginLeftSlide.Count)
            {
                var w = sliderUtilsService.WidthSlide2.ToString(CultureInfo.InvariantCulture);

                sliderUtilsService.MarginLeftSlide[sliderUtilsService.CountSlide - 1] =
                    $"margin-left:-{w}%";

                IsDisabledNext = false;
                IsDisbledPrevious = false;
            }
            else
            {
                IsDisabledNext = true;
            }
        }

    }
}
