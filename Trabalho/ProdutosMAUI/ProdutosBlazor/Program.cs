using ProdutosBlazor.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using RCLProdutos.Services.Interfaces;
using RCLProdutos.Services;
using RCLAPI.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Globalization;
using RCLProdutos.Shared.Slider;



var builder = WebApplication.CreateBuilder(args);
// Configurar a cultura global para Português de Portugal (Produtos em Euros) 
var cultureInfo = new CultureInfo("pt-PT");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();


builder.Services.AddScoped<ISliderUtilsServices, SliderUtilsServices>();
builder.Services.AddScoped<ICardsUtilsServices, CardsUtilsServices>();
builder.Services.AddScoped<ICarrinhoServices, CarrinhoServices>();
builder.Services.AddScoped<IApiServices, ApiService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress =
    new Uri("https://localhost:7058")
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
