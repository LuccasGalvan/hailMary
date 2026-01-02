using RCLAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLProdutos.Services.Interfaces
{
    public interface ICarrinhoServices
    {
        public void AdicionarItem(ItemCarrinhoCompra item);

        public List<ItemCarrinhoCompra> getItems();
        public void RemoverItem(int id);

        public void LimparCarrinho();
    }
}
