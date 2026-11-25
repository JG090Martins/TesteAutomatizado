namespace TesteAutomatizado.Models;

/// <summary>
/// Agrega os produtos e registra o histórico de movimentações.
/// </summary>
public class Estoque
{
    private readonly List<Produto> _produtos = new();
    private readonly List<MovimentacaoEstoque> _movimentacoes = new();

    public IReadOnlyList<Produto> Produtos => _produtos;
    public IReadOnlyList<MovimentacaoEstoque> Movimentacoes => _movimentacoes;

    public void CadastrarProduto(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(produto);
        if (_produtos.Any(p => p.Id == produto.Id))
            throw new InvalidOperationException($"Já existe um produto com o Id {produto.Id}.");

        _produtos.Add(produto);
    }

    public MovimentacaoEstoque RegistrarEntrada(Produto produto, int quantidade)
    {
        produto.AdicionarEstoque(quantidade);
        return Registrar(produto, TipoMovimentacao.Entrada, quantidade);
    }

    public MovimentacaoEstoque RegistrarSaida(Produto produto, int quantidade)
    {
        produto.RemoverEstoque(quantidade);
        return Registrar(produto, TipoMovimentacao.Saida, quantidade);
    }

    /// <summary>Produtos que precisam de reposição.</summary>
    public IEnumerable<Produto> ProdutosAbaixoDoMinimo() =>
        _produtos.Where(p => p.EstaAbaixoDoMinimo());

    private MovimentacaoEstoque Registrar(Produto produto, TipoMovimentacao tipo, int quantidade)
    {
        var movimentacao = new MovimentacaoEstoque(produto, tipo, quantidade);
        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}
