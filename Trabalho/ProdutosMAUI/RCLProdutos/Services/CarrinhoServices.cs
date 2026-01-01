using RCLAPI.DTO;
using RCLProdutos.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLProdutos.Services;

public class CarrinhoServices : ICarrinhoServices
{
    static List<ItemCarrinhoCompra> carrinhoCompras = new List<ItemCarrinhoCompra>();

    public void AdicionarItem(ItemCarrinhoCompra item)
    {
        Console.WriteLine("Item Adicionado Com Sucesso");
        carrinhoCompras.Add(item);

        Console.WriteLine("Carrinho de Compras");
        foreach (var i in carrinhoCompras)
        {
            Console.WriteLine($"Produto: {i.ProdutoId} - Quantidade: {i.Quantidade}");
        }
    }

    public List<ItemCarrinhoCompra> getItems()
    {
        return carrinhoCompras;
    }

    public void RemoverItem(int id)
    {
        carrinhoCompras.RemoveAll(x => x.ProdutoId == id);
    }

    public void LimparCarrinho()
    {
        carrinhoCompras.Clear();
    }
}
