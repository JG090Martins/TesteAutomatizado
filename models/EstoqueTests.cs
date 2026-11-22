using NUnit.Framework;
using ControleEstoque;

namespace ControleEstoque.Tests;

[TestFixture]
public class EstoqueTests
{
    // Teste 1: Comportamento esperado
    [Test]
    public void AdicionarEstoque_QuantidadeValida_DeveSomarAoSaldoAtual()
    {
        var produto = new Produto(1, "Monitor", 5, 900.00m);
        produto.AdicionarEstoque(3);

        Assert.That(produto.Quantidade, Is.EqualTo(8));
    }

    // Teste 2: Tratamento de erro 
    [Test]
    public void RemoverEstoque_QuantidadeMaiorQueSaldo_DeveLancarExcecao()
    {
        var produto = new Produto(1, "Webcam", 2, 300.00m);

        Assert.Throws<InvalidOperationException>(() => produto.RemoverEstoque(5));
    }

    // Teste 3: Retorno de função (cálculo do valor total)
    [Test]
    public void CalcularValorTotalEmEstoque_DeveRetornarMultiplicacaoCorreta()
    {
        var produto = new Produto(1, "Cabo HDMI", 4, 25.50m);
        var valorTotal = produto.CalcularValorTotalEmEstoque();

        Assert.That(valorTotal, Is.EqualTo(102.00m));
    }
}