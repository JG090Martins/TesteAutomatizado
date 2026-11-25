namespace ControleEstoque.Models;

public class MovimentacaoEstoque
{
    public Produto Produto { get; }
    public TipoMovimentacao Tipo { get; }
    public int Quantidade { get; }
    public DateTime Data { get; }

    public MovimentacaoEstoque(Produto produto, TipoMovimentacao tipo, int quantidade, DateTime? data = null)
    {
        ArgumentNullException.ThrowIfNull(produto);
        if (quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");

        Produto = produto;
        Tipo = tipo;
        Quantidade = quantidade;
        Data = data ?? DateTime.Now;
    }
}
