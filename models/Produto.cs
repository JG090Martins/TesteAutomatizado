namespace ControleEstoque;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; set; }

    public Produto(int id, string nome, int quantidadeInicial, decimal precoUnitario)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto não pode ser vazio.");

        if (quantidadeInicial < 0)
            throw new ArgumentException("A quantidade inicial não pode ser negativa.");

        if (precoUnitario <= 0)
            throw new ArgumentException("O preço unitário deve ser maior que zero.");

        Id = id;
        Nome = nome;
        Quantidade = quantidadeInicial;
        PrecoUnitario = precoUnitario;
    }

    public void AdicionarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade a adicionar deve ser maior que zero.");

        Quantidade += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade a remover deve ser maior que zero.");

        if (quantidade > Quantidade)
            throw new InvalidOperationException("Estoque insuficiente para realizar a baixa.");

        Quantidade -= quantidade;
    }

    public decimal CalcularValorTotalEmEstoque()
    {
        return Quantidade * PrecoUnitario;
    }

    public bool EstaAbaixoDoMinimo(int limiteMinimo = 5)
{
    return Quantidade <= limiteMinimo;
}

}