namespace ControleEstoque;

public class GerenciadorEstoque
{
    private readonly List<Produto> _produtos = new();

    public void CadastrarProduto(Produto produto)
    {
        if (_produtos.Any(p => p.Id == produto.Id))
            throw new InvalidOperationException("Já existe um produto cadastrado com este ID.");

        _produtos.Add(produto);
    }

    public Produto? BuscarPorId(int id)
    {
        return _produtos.FirstOrDefault(p => p.Id == id);
    }

    public bool EstaEmEstoqueBaixo(int id, int limiteMinimo = 5)
    {
        var produto = BuscarPorId(id) ?? throw new KeyNotFoundException("Produto não encontrado.");
        return produto.Quantidade <= limiteMinimo;
    }
}