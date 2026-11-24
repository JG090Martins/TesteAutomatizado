using NUnit.Framework;
using ControleEstoque;

namespace ControleEstoque.Tests;

[TestFixture]
public class GerenciadorEstoqueTests
{
    private GerenciadorEstoque _gerenciador = null!;

    [SetUp]
    public void Setup()
    {
        _gerenciador = new GerenciadorEstoque();
    }

    [Test]
    public void CadastrarProduto_IdDuplicado_DeveLancarExcecao()
    {
        _gerenciador.CadastrarProduto(new Produto(1, "Teclado", 10, 150.00m));
        var duplicado = new Produto(1, "Mouse", 5, 80.00m);

        Assert.That(() => _gerenciador.CadastrarProduto(duplicado),
            Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void BuscarPorId_ProdutoCadastrado_DeveRetornarOProdutoCorreto()
    {
        var produto = new Produto(2, "Headset", 7, 220.00m);
        _gerenciador.CadastrarProduto(produto);

        var resultado = _gerenciador.BuscarPorId(2);

        Assert.That(resultado, Is.SameAs(produto));
    }

    [TestCase(3, true)]
    [TestCase(5, true)]
    [TestCase(6, false)]
    public void EstaEmEstoqueBaixo_DeveRespeitarLimiteMinimo(int quantidade, bool esperado)
    {
        _gerenciador.CadastrarProduto(new Produto(3, "Pen Drive", quantidade, 40.00m));

        var resultado = _gerenciador.EstaEmEstoqueBaixo(3);

        Assert.That(resultado, Is.EqualTo(esperado));
    }
}
