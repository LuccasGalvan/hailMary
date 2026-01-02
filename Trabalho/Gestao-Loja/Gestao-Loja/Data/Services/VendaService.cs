namespace Gestao_Loja.Data.Services;

using Gestao_Loja.Constants;
using Gestao_Loja.Entities;
using Microsoft.EntityFrameworkCore;

    public class VendaService
    {
        private readonly ApplicationDbContext _db;

        public VendaService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Venda?> ObterVendaAsync(int id)
        {
            return await _db.Vendas
                .Include(v => v.Linhas)
                    .ThenInclude(l => l.Produto)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.VendaId == id);
        }

        public async Task ConfirmarVendaAsync(int vendaId)
        {
            var venda = await ObterVendaAsync(vendaId);
            if (venda is null || venda.Estado != EstadoVenda.Pendente)
                return;

            // Validar stock
            foreach (var linha in venda.Linhas)
            {
                if (linha.Produto.Stock < linha.Quantidade)
                    throw new InvalidOperationException(
                        $"Stock insuficiente para o produto {linha.Produto.Nome}");
            }

            // Deduzir stock
            foreach (var linha in venda.Linhas)
            {
                linha.Produto.Stock -= linha.Quantidade;
            }

            venda.DataConfirmacao = DateTime.Now;
            venda.Estado = EstadoVenda.Confirmada;
            venda.ValorTotal = venda.Linhas.Sum(l => l.TotalLinha);

            await _db.SaveChangesAsync();
        }

        public async Task RejeitarVendaAsync(int vendaId, string motivo)
        {
            var venda = await _db.Vendas.FindAsync(vendaId);
            if (venda is null || venda.Estado != EstadoVenda.Pendente)
                return;

            venda.Estado = EstadoVenda.Rejeitada;
            venda.Observacoes = motivo;

            await _db.SaveChangesAsync();
        }

        public async Task ExpedirVendaAsync(int vendaId)
        {
            var venda = await _db.Vendas.FindAsync(vendaId);
            if (venda is null || venda.Estado != EstadoVenda.Confirmada)
                return;

            venda.Estado = EstadoVenda.Expedida;
            venda.DataExpedicao = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task SimularPagamentoAsync(int vendaId)
        {
            var venda = await _db.Vendas.FindAsync(vendaId);
            if (venda == null) return;

            venda.MetodoPagamento = "Simulado";
            venda.DataPagamento = DateTime.Now;
            venda.PagamentoExecutado = true;

            await _db.SaveChangesAsync();
        }

}

