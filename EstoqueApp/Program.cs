using System.Globalization;
using ControleEstoque;

var gerenciador = new GerenciadorEstoque();
bool executando = true;

while (executando)
{
    Console.WriteLine("\n==========================================");
    Console.WriteLine("     SISTEMA DE CONTROLE DE ESTOQUE");
    Console.WriteLine("==========================================");
    Console.WriteLine("1 - Cadastrar novo produto no estoque");
    Console.WriteLine("2 - Adicionar quantidade ao estoque");
    Console.WriteLine("3 - Remover quantidade e validar saldo");
    Console.WriteLine("4 - Calcular valor total em estoque");
    Console.WriteLine("5 - Alertar estoque baixo");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                Console.Write("ID do produto (número inteiro): ");
                int id = int.Parse(Console.ReadLine()!);

                Console.Write("Nome do produto: ");
                string nome = Console.ReadLine()!;

                Console.Write("Quantidade inicial: ");
                int qtdInicial = int.Parse(Console.ReadLine()!);

                Console.Write("Preço unitário (ex: 150.00): ");
                decimal preco = decimal.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);

                var novoProduto = new Produto(id, nome, qtdInicial, preco);
                gerenciador.CadastrarProduto(novoProduto);

                Console.WriteLine($"[OK] Produto '{nome}' cadastrado com sucesso!");
                break;

            case "2":
                Console.Write("ID do produto para entrada: ");
                int idEntrada = int.Parse(Console.ReadLine()!);
                var prodEntrada = gerenciador.BuscarPorId(idEntrada)
                    ?? throw new KeyNotFoundException("Produto não encontrado.");

                Console.Write($"Quantidade a adicionar (Saldo atual: {prodEntrada.Quantidade}): ");
                int qtdAdd = int.Parse(Console.ReadLine()!);

                prodEntrada.AdicionarEstoque(qtdAdd);
                Console.WriteLine($"[OK] Novo saldo de '{prodEntrada.Nome}': {prodEntrada.Quantidade} unidades.");
                break;

            case "3":
                Console.Write("ID do produto para saída: ");
                int idSaida = int.Parse(Console.ReadLine()!);
                var prodSaida = gerenciador.BuscarPorId(idSaida)
                    ?? throw new KeyNotFoundException("Produto não encontrado.");

                Console.Write($"Quantidade a remover (Saldo atual: {prodSaida.Quantidade}): ");
                int qtdRem = int.Parse(Console.ReadLine()!);

                prodSaida.RemoverEstoque(qtdRem);
                Console.WriteLine($"[OK] Saída realizada! Saldo restante de '{prodSaida.Nome}': {prodSaida.Quantidade} unidades.");
                break;

            case "4":
                Console.Write("ID do produto para calcular valor total: ");
                int idValor = int.Parse(Console.ReadLine()!);
                var prodValor = gerenciador.BuscarPorId(idValor)
                    ?? throw new KeyNotFoundException("Produto não encontrado.");

                decimal valorTotal = prodValor.CalcularValorTotalEmEstoque();
                Console.WriteLine($"[INFO] '{prodValor.Nome}': {prodValor.Quantidade} un. x R$ {prodValor.PrecoUnitario:F2} = R$ {valorTotal:F2}");
                break;

            case "5":
                Console.Write("ID do produto para verificar nível mínimo (limite = 5): ");
                int idAlerta = int.Parse(Console.ReadLine()!);
                var prodAlerta = gerenciador.BuscarPorId(idAlerta)
                    ?? throw new KeyNotFoundException("Produto não encontrado.");

                bool abaixoDoMinimo = gerenciador.EstaEmEstoqueBaixo(idAlerta);
                if (abaixoDoMinimo)
                    Console.WriteLine($"[ALERTA] Estoque baixo para '{prodAlerta.Nome}'! Saldo atual: {prodAlerta.Quantidade} unidades (mínimo: 5).");
                else
                    Console.WriteLine($"[OK] Estoque normal para '{prodAlerta.Nome}'. Saldo atual: {prodAlerta.Quantidade} unidades.");
                break;

            case "0":
                executando = false;
                Console.WriteLine("Encerrando o sistema...");
                break;

            default:
                Console.WriteLine("[AVISO] Opção inválida. Tente novamente.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERRO BLOQUEADO PELA REGRA DE NEGÓCIO] {ex.Message}");
    }
}