using Microsoft.Extensions.Logging;

using RCLProdutos.Services.Interfaces;
using RCLProdutos.Services;
using RCLAPI.Services;

namespace ProdutosMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddScoped<ISliderUtilsServices, SliderUtilsServices>();
            builder.Services.AddScoped<ICardsUtilsServices, CardsUtilsServices>();
            builder.Services.AddScoped<ICarrinhoServices, CarrinhoServices>();
            builder.Services.AddScoped<IApiServices, ApiService>();

            return builder.Build();
        }
    }
}