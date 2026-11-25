github: https://github.com/JG090Martins/TesteAutomatizado

trello: https://trello.com/b/MEvEbTyf/controle-de-estoque

Equipe: João Gabriel, Otávio Nogueira e Pedro Tavares


Sistema de controle de estoque com testes automaticos

Projeto desenvolvido para a disciplina de **Gestão da Qualidade de Software**, com foco na implementação de regras de negócio para controle de estoque em C#, validação por meio de testes unitários automatizados com **NUnit** e controle de versão estruturado via **Gitflow**.


 Visão geral e funções

O sistema modela o gerenciamento de produtos, movimentações de entrada/saída e alertas de nível mínimo de estoque:

--Cadastro e Validação de Produtos ('Produto.cs'):** Validação de nome obrigatório, preço unitário positivo, quantidade inicial válida, entrada e saída de itens com bloqueio de saldo negativo, cálculo de valor total em estoque e verificação de estoque mínimo.
--Gerenciamento de Estoque ('GerenciadorEstoque.cs' e 'Estoque.cs'): Cadastro de produtos sem duplicidade de ID, busca por identificador, controle de movimentações e monitoramento de estoque baixo.
--Registro de Movimentações ('MovimentacaoEstoque.cs' e 'TipoMovimentacao.cs'): Rastreamento de operações de entrada e saída vinculadas aos produtos.

Tecnologias

--Linguagem: C# (.NET 10.0)
--Framework de Testes: NUnit (NUnit3TestExecutor / NUnit Adapter 5.0.0)
--Controle de Versão: Git & GitHub (Fluxo Gitflow)
--Gestão Ágil (Kanban): Trello


Testes Unitários (9 Testes Aprovados)

A suíte automatizada cobre comportamentos esperados, cálculos de retorno e lançamentos de exceções para regras inválidas:

--Operações de Produto ('EstoqueTest.cs'):
   Adição de quantidade válida somando corretamente ao saldo atual.
   Bloqueio (exceção) ao tentar remover quantidade maior que o saldo disponível.
   Cálculo exato do valor total em estoque ('Quantidade' * 'PrecoUnitario').
   Validações de dados inválidos na criação e movimentação de produtos.
--Operações de Gerenciamento e Saída ('GerenciadorEstoqueTests.cs' e testes integrados):**
   Cadastro e busca de produtos por ID.
   Verificação de alerta para produtos abaixo do limite mínimo de estoque.
   Validações de regras de saída e movimentação de itens.

--Condições e Funcionamento para Execução dos Testes

Para que cada teste seja executado e aprovado, o código de domínio precisa ter as seguintes entradas, ações e regras de negócio:

--Preparação de Ambiente Isolado ([SetUp]):** Antes de cada teste em GerenciadorEstoqueTests, uma nova instância limpa de GerenciadorEstoque é criada para evitar que cadastros de um cenário interfiram no outro.
--Soma de Saldo na Entrada (EstoqueTests.cs):** Ao instanciar Produto(1, "Monitor", 5, 900.00m) e executar AdicionarEstoque(3), a propriedade Quantidade deve ser atualizada para 8.
--Bloqueio de Saldo Negativo na Saída (EstoqueTests.cs): Ao instanciar Produto(1, "Webcam", 2, 300.00m) e tentar executar RemoverEstoque(5), o sistema deve bloquear a retirada acima do saldo e lançar InvalidOperationException.
--Cálculo de Valor Total em Estoque (EstoqueTests.cs): Ao instanciar `Produto(1, "Cabo HDMI", 4, 25.50m) e chamar CalcularValorTotalEmEstoque(), o método deve multiplicar quantidade por preço unitário e retornar exatamente 102.00m.
--Bloqueio de ID Duplicado (GerenciadorEstoqueTests.cs): Após cadastrar Produto(1, "Teclado", 10, 150.00m), qualquer tentativa de cadastrar outro produto com o mesmo Id = 1 (Produto(1, "Mouse", 5, 80.00m)) deve lançar InvalidOperationException.
--Consulta de Referência por ID (GerenciadorEstoqueTests.cs): Após cadastrar Produto(2, "Headset", 7, 220.00m), a chamada BuscarPorId(2) deve localizar e retornar exatamente a mesma instância do objeto armazenado (Is.SameAs).
--Verificação de Limite Mínimo (GerenciadorEstoqueTests.cs): Ao cadastrar Produto(3, "Pen Drive", quantidade, 40.00m) e consultar EstaEmEstoqueBaixo(3), o sistema compara o saldo com o limite mínimo padrão de 5 unidades — retornando true para 3 e 5, e false para 6.