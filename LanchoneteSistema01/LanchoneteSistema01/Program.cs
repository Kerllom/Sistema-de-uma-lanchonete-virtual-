using LanchoneSistema01.Modelos;
using LanchoneteSistema01.Modelos;

namespace LanchoneteSistema
{
    /// <summary>
    /// Classe principal da aplicação. Responsável por orquestrar a
    /// interação com o usuário através de um menu interativo no console.
    /// 
    /// Esta classe NÃO modela uma entidade do mundo real (como Cliente
    /// ou Produto). Seu papel é apenas conectar as classes do sistema
    /// e gerenciar o fluxo de execução.
    /// </summary>
    internal class Program
    {
        // ===== VARIÁVEIS DE ESTADO DA APLICAÇÃO =====
        // São 'static' porque a classe Program é estática (orquestradora).
        // Em um projeto maior, essas variáveis estariam em uma classe específica.

        /// <summary>Cardápio fixo da lanchonete (produtos disponíveis para venda).</summary>
        private static List<Produto> _cardapio = new List<Produto>();

        /// <summary>Pedido atual que está sendo montado pelo cliente.</summary>
        private static Pedido _pedidoAtual = null;

        // ===== MÉTODO MAIN — PONTO DE ENTRADA =====
        /// <summary>
        /// Método principal da aplicação. Inicializa o cardápio,
        /// cadastra o cliente e exibe o menu em loop até o usuário sair.
        /// </summary>
        static void Main(string[] args)
        {
            // Configura o console para exibir caracteres especiais (acentos e emojis)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Mensagem de boas-vindas
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("        🍔 BEM-VINDO À LANCHONETE VIRTUAL 🍔");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine();

            // Inicializa o cardápio com produtos pré-cadastrados
            InicializarCardapio();

            // Solicita os dados do cliente e cria o pedido
            CadastrarCliente();

            // Inicia o loop principal do menu
            bool sair = false;
            while (!sair)
            {
                ExibirMenuPrincipal();

                string opcao = Console.ReadLine()?.Trim();

                // Estrutura switch para tratar a opção escolhida.
                // É mais limpa que vários if/else encadeados.
                switch (opcao)
                {
                    case "1":
                        ExibirCardapio();
                        break;
                    case "2":
                        AdicionarItemAoCarrinho();
                        break;
                    case "3":
                        RemoverItemDoCarrinho();
                        break;
                    case "4":
                        VerCarrinho();
                        break;
                    case "5":
                        sair = FinalizarPedido();
                        break;
                    case "0":
                        Console.WriteLine("\n👋 Obrigado por visitar! Até mais!");
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Opção inválida. Tente novamente.");
                        break;
                }

                // Pausa para o usuário ler a saída antes de mostrar o menu de novo
                if (!sair)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // ===== EXIBIÇÃO DO MENU =====
        /// <summary>
        /// Exibe as opções do menu principal no console.
        /// </summary>
        private static void ExibirMenuPrincipal()
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("                  📋 MENU PRINCIPAL");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("  1️⃣  - Ver Cardápio");
            Console.WriteLine("  2️⃣  - Adicionar Item ao Carrinho");
            Console.WriteLine("  3️⃣  - Remover Item do Carrinho");
            Console.WriteLine("  4️⃣  - Ver Carrinho");
            Console.WriteLine("  5️⃣  - Finalizar Pedido");
            Console.WriteLine("  0️⃣  - Sair");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.Write("👉 Escolha uma opção: ");
        }

        // ===== INICIALIZAÇÃO DO CARDÁPIO =====
        /// <summary>
        /// Cria os produtos pré-cadastrados disponíveis para venda.
        /// Em um sistema real, esses dados viriam de um banco de dados,
        /// mas para este projeto didático eles ficam fixos em memória.
        /// </summary>
        private static void InicializarCardapio()
        {
            // === LANCHES ===
            _cardapio.Add(new Lanche(
                "X-Burguer",
                18.00,
                new List<string> { "Pão", "Carne", "Queijo", "Alface", "Tomate" }
            ));

            _cardapio.Add(new Lanche(
                "X-Bacon",
                22.00,
                new List<string> { "Pão", "Carne", "Queijo", "Bacon", "Alface" }
            ));

            _cardapio.Add(new Lanche(
                "X-Tudo",
                28.00,
                new List<string> { "Pão", "Carne", "Queijo", "Bacon", "Ovo", "Presunto", "Alface", "Tomate" }
            ));

            // === BEBIDAS ===
            _cardapio.Add(new Bebida("Coca-Cola", 8.00, 350));
            _cardapio.Add(new Bebida("Suco de Laranja", 10.00, 500));
            _cardapio.Add(new Bebida("Água Mineral", 5.00, 500));

            // === SOBREMESAS ===
            _cardapio.Add(new Sobremesa("Sorvete de Chocolate", 12.00, "Gelada"));
            _cardapio.Add(new Sobremesa("Brownie com Sorvete", 15.00, "Mista"));

            // === COMBOS ===
            // Combo é montado a partir de outros produtos do cardápio.
            // O preço do combo é fechado (menor que a soma individual).
            var lancheCombo = new Lanche(
                "X-Burguer (Combo)",
                18.00,
                new List<string> { "Pão", "Carne", "Queijo", "Alface", "Tomate" }
            );
            var bebidaCombo = new Bebida("Coca-Cola (Combo)", 8.00, 350);

            _cardapio.Add(new Combo(
                "Combo Clássico",
                22.00,  // preço fechado: 18 + 8 = 26 normalmente, no combo sai por 22
                new List<Produto> { lancheCombo, bebidaCombo }
            ));
        }

        // ===== CADASTRO DO CLIENTE =====
        /// <summary>
        /// Solicita os dados do cliente via console e cria o pedido vinculado a ele.
        /// Realiza validação básica (tenta novamente em caso de erro).
        /// </summary>
        private static void CadastrarCliente()
        {
            Console.WriteLine("📝 Antes de começar, precisamos dos seus dados:\n");

            Cliente cliente = null;

            // Loop até conseguir criar o cliente sem erro de validação
            while (cliente == null)
            {
                try
                {
                    Console.Write("Nome completo: ");
                    string nome = Console.ReadLine();

                    Console.Write("Endereço completo: ");
                    string endereco = Console.ReadLine();

                    Console.Write("Telefone: ");
                    string telefone = Console.ReadLine();

                    // Se algum dado for inválido, a própria classe Cliente lança exceção
                    cliente = new Cliente(nome, endereco, telefone);
                }
                catch (ArgumentException ex)
                {
                    // Captura erros de validação vindos da classe Cliente
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                    Console.WriteLine("Por favor, tente novamente.\n");
                }
            }

            // Cria o pedido vinculado ao cliente recém-cadastrado
            _pedidoAtual = new Pedido(cliente);

            Console.WriteLine($"\n✅ Cliente cadastrado com sucesso, {cliente.Nome}!");
            Console.WriteLine("Pressione qualquer tecla para acessar o menu...");
            Console.ReadKey();
            Console.Clear();
        }

        // ===== EXIBIÇÃO DO CARDÁPIO =====
        /// <summary>
        /// Lista todos os produtos disponíveis no cardápio, numerados.
        /// A numeração serve para o usuário escolher pelo número ao adicionar
        /// um item ao carrinho.
        /// </summary>
        private static void ExibirCardapio()
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("                  📜 CARDÁPIO");
            Console.WriteLine("═══════════════════════════════════════════════════════");

            // Percorre o cardápio mostrando cada produto numerado.
            // Aqui o POLIMORFISMO aparece de novo: ExibirDetalhes() funciona
            // diferente para cada tipo de produto (Lanche, Bebida, Combo...).
            for (int i = 0; i < _cardapio.Count; i++)
            {
                Console.WriteLine($"\n[{i + 1}] {_cardapio[i].ExibirDetalhes()}");
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
        }

        // ===== ADICIONAR ITEM AO CARRINHO =====
        /// <summary>
        /// Solicita ao usuário o número do produto desejado e o adiciona
        /// ao carrinho do pedido atual. Faz validação da entrada.
        /// </summary>
        private static void AdicionarItemAoCarrinho()
        {
            // Primeiro mostra o cardápio para o usuário escolher
            ExibirCardapio();

            Console.Write("\n👉 Digite o número do produto que deseja adicionar (0 para cancelar): ");
            string entrada = Console.ReadLine()?.Trim();

            // Tenta converter a entrada em número inteiro.
            // int.TryParse retorna true se conseguiu converter, false se não.
            // Isso é mais seguro que int.Parse, que lançaria exceção.
            if (!int.TryParse(entrada, out int numero))
            {
                Console.WriteLine("\n❌ Entrada inválida. Digite apenas números.");
                return;
            }

            // Opção 0 = cancelar a operação
            if (numero == 0)
            {
                Console.WriteLine("\n↩️  Operação cancelada.");
                return;
            }

            // Valida se o número está dentro do intervalo do cardápio
            if (numero < 1 || numero > _cardapio.Count)
            {
                Console.WriteLine("\n❌ Número fora do cardápio. Tente novamente.");
                return;
            }

            // Como o usuário digita a partir de 1, mas a lista começa em 0,
            // subtraímos 1 para obter o índice correto.
            Produto produtoEscolhido = _cardapio[numero - 1];

            // Adiciona ao carrinho usando o método da classe Pedido
            _pedidoAtual.AdicionarItem(produtoEscolhido);

            Console.WriteLine($"\n✅ '{produtoEscolhido.Nome}' adicionado ao carrinho!");
        }

        // ===== REMOVER ITEM DO CARRINHO =====
        /// <summary>
        /// Solicita ao usuário o número do item que deseja remover do
        /// carrinho e executa a remoção. Faz validação da entrada.
        /// </summary>
        private static void RemoverItemDoCarrinho()
        {
            // Se o carrinho estiver vazio, não tem nada para remover
            if (_pedidoAtual.CarrinhoVazio())
            {
                Console.WriteLine("\n🛒 Seu carrinho está vazio. Nada para remover.");
                return;
            }

            // Mostra os itens atuais do carrinho com numeração
            VerCarrinho();

            Console.Write("\n👉 Digite o número do item que deseja remover (0 para cancelar): ");
            string entrada = Console.ReadLine()?.Trim();

            if (!int.TryParse(entrada, out int numero))
            {
                Console.WriteLine("\n❌ Entrada inválida. Digite apenas números.");
                return;
            }

            if (numero == 0)
            {
                Console.WriteLine("\n↩️  Operação cancelada.");
                return;
            }

            // Tenta remover o item pelo índice.
            // A própria classe Pedido valida e lança exceção se for inválido.
            try
            {
                // numero - 1 porque o usuário digita a partir de 1
                _pedidoAtual.RemoverItem(numero - 1);
                Console.WriteLine("\n✅ Item removido do carrinho!");
            }
            catch (ArgumentOutOfRangeException)
            {
                // Capturamos a exceção lançada pela classe Pedido
                Console.WriteLine("\n❌ Número inválido. Verifique os itens do carrinho.");
            }
        }

        // ===== VER CARRINHO =====
        /// <summary>
        /// Exibe os itens atualmente no carrinho com numeração,
        /// além de um subtotal parcial. Não mostra desconto nem taxa
        /// (isso aparece só na finalização do pedido).
        /// </summary>
        private static void VerCarrinho()
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("                  🛒 SEU CARRINHO");
            Console.WriteLine("═══════════════════════════════════════════════════════");

            if (_pedidoAtual.CarrinhoVazio())
            {
                Console.WriteLine("\n   (Carrinho vazio. Adicione produtos pelo menu.)");
            }
            else
            {
                // Acessa a propriedade Itens do pedido (que é List<Produto>)
                List<Produto> itens = _pedidoAtual.Itens;

                for (int i = 0; i < itens.Count; i++)
                {
                    Console.WriteLine($"\n[{i + 1}] {itens[i].ExibirDetalhes()}");
                }

                Console.WriteLine($"\n💰 Subtotal parcial: R$ {_pedidoAtual.CalcularSubtotal():F2}");
                Console.WriteLine("   (Desconto e taxa serão calculados na finalização)");
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
        }

        // ===== FINALIZAR PEDIDO =====
        /// <summary>
        /// Exibe o resumo completo do pedido e encerra a aplicação.
        /// Retorna true se o pedido foi finalizado com sucesso (encerra o programa),
        /// ou false se o usuário cancelou (volta ao menu).
        /// </summary>
        /// <returns>True se o pedido foi finalizado, false se cancelado</returns>
        private static bool FinalizarPedido()
        {
            // Não dá pra finalizar um pedido vazio
            if (_pedidoAtual.CarrinhoVazio())
            {
                Console.WriteLine("\n🛒 Seu carrinho está vazio. Adicione produtos antes de finalizar.");
                return false;
            }

            // Confirmação antes de finalizar
            Console.WriteLine("\n⚠️  Deseja realmente finalizar o pedido? (S/N): ");
            string confirmacao = Console.ReadLine()?.Trim().ToUpper();

            if (confirmacao != "S")
            {
                Console.WriteLine("\n↩️  Finalização cancelada. Você voltou ao menu.");
                return false;
            }

            // Gera e exibe o resumo completo do pedido.
            // O método GerarResumo() da classe Pedido faz todo o trabalho:
            // cliente, itens, subtotal, desconto, taxa, total.
            Console.Clear();
            Console.WriteLine(_pedidoAtual.GerarResumo());

            Console.WriteLine("\n🎉 Pedido confirmado! Em breve você receberá seu pedido.");
            Console.WriteLine("👋 Obrigado pela preferência!\n");

            // Retorna true para encerrar o loop principal do Main
            return true;
        }
    }
}