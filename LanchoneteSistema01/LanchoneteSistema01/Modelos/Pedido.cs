using LanchoneteSistema01.Modelos;

namespace LanchoneSistema01.Modelos
{
    /// <summary>
    /// Classe central do sistema, responsável por agregar um Cliente
    /// e seus produtos selecionados (carrinho), além de calcular valores
    /// e aplicar as regras de negócio (desconto, taxa de entrega).
    /// 
    /// Esta classe é um exemplo de AGREGAÇÃO: ela "tem-um" Cliente e
    /// "tem-vários" Produtos, mas não é um deles.
    /// </summary>
    public class Pedido
    {
        // ===== CONSTANTES (REGRAS DE NEGÓCIO FIXAS) =====
        // Usamos 'const' para valores que nunca mudam durante a execução.
        // É uma boa prática evitar "números mágicos" espalhados no código.

        /// <summary>Valor da taxa de entrega fixa, em reais.</summary>
        public const double TAXA_ENTREGA = 4.00;

        /// <summary>Valor mínimo do carrinho para ativar o desconto.</summary>
        public const double VALOR_MINIMO_DESCONTO = 50.00;

        /// <summary>Percentual de desconto aplicado (10% = 0.10).</summary>
        public const double PERCENTUAL_DESCONTO = 0.10;

        // ===== ATRIBUTOS PRIVADOS =====
        private Cliente _cliente;
        private List<Produto> _itens;
        private DateTime _dataPedido;

        // ===== PROPRIEDADES =====

        /// <summary>
        /// Cliente que está realizando o pedido.
        /// </summary>
        public Cliente Cliente
        {
            get { return _cliente; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("O pedido precisa estar associado a um cliente.");
                _cliente = value;
            }
        }

        /// <summary>
        /// Lista de produtos no carrinho do pedido.
        /// O 'get' é público, mas o 'set' é PRIVATE - isso impede que
        /// alguém de fora substitua a lista inteira. Para modificar o carrinho, 
        /// devem ser usados os métodos AdicionarItem() e RemoverItem().
        /// </summary>
        public List<Produto> Itens
        {
            get { return _itens; }
            private set { _itens = value; }
        }

        /// <summary>
        /// Data e hora em que o pedido foi criado.
        /// Apenas leitura externa (o 'set' é privado).
        /// </summary>
        public DateTime DataPedido
        {
            get { return _dataPedido; }
            private set { _dataPedido = value; }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria um novo pedido vinculado a um cliente.
        /// O carrinho começa vazo e a data é definida automaticamente.
        /// </summary>
        /// <param name="cliente">Cliente que está realizando o pedido</param>
        public Pedido(Cliente cliente)
        {
            Cliente = cliente;
            Itens = new List<Produto>();    // Carrinho começa vazio
            DataPedido = DateTime.Now;      // Marca o momento da criação
        }

        // ===== MÉTODOS DE GERENCIAMENTO DO CARRINHO =====

        /// <summary>
        /// Adiciona um produto ao carrinho do pedido.
        /// Aceita qualquer tipo de Produto (Lanche, Bebida, Sobremesa, Combo)
        /// graças ao POLIMORFISMO.
        /// </summary>
        /// <param name="produto">Produto a ser adicionado</param>
        public void AdicionarItem(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException("Não é possível adicionar um produto nulo ao carrinho.");

            _itens.Add(produto);
        }

        /// <summary>
        /// Remove um produto do carrinho pelo índice (posição na lista).
        /// </summary>
        /// <param name="indice">Posição do item na lista (0 = primeiro)</param>
        public void RemoverItem(int indice)
        {
            // Validação: o índice precisa estar dentro dos limites da lista
            if (indice < 0 || indice >= _itens.Count)
                throw new ArgumentOutOfRangeException(nameof(indice),
                    "Índice inválido. Verifique os itens do carrinho.");

            _itens.RemoveAt(indice);
        }

        /// <summary>
        /// Verifica se o carrinho está vazio.
        /// </summary>
        public bool CarrinhoVazio()
        {
            return _itens.Count == 0;
        }

        // ===== MÉTODOS DE CÁLCULO (REGRAS DE NEGÓCIO) =====

        /// <summary>
        /// Calcula o subtotal do pedido somando o preço de todos os itens
        /// do carrinho, antes de qualquer desconto ou taxa.
        /// </summary>
        /// <returns>Soma dos preços de todos os itens</returns>
        public double CalcularSubtotal()
        {
            double subtotal = 0;

            // Percorre a lista de produtos somando os preços.
            // Aqui o POLIMORFISMO é silencioso: o foreach trata cada item
            // como Produto, e a propriedade Preco existe em todos eles
            // (foi herdada da classe Produto).
            foreach (Produto item in _itens)
            {
                subtotal += item.Preco;
            }

            return subtotal;
        }

        /// <summary>
        /// Calcula o desconto aplicado ao pedido.
        /// REGRA DE NEGÓCIO: se o subtotal for maior que R$ 50,
        /// aplica-se 10% de desconto. Caso contrário, o desconto é zero.
        /// </summary>
        /// <returns>Valor do desconto em reais (0 se não atingiu o mínimo)</returns>
        public double CalcularDesconto()
        {
            double subtotal = CalcularSubtotal();

            // Aplica o desconto apenas se ultrapassar o valor mínimo
            if (subtotal > VALOR_MINIMO_DESCONTO)
            {
                return subtotal * PERCENTUAL_DESCONTO;
            }

            return 0;
        }

        /// <summary>
        /// Calcula o valor total final do pedido:
        /// subtotal - desconto + taxa de entrega.
        /// </summary>
        /// <returns>Valor final que o cliente deve pagar</returns>
        public double CalcularTotal()
        {
            double subtotal = CalcularSubtotal();
            double desconto = CalcularDesconto();

            // Fórmula final: (itens) - (desconto) + (taxa fixa)
            return subtotal - desconto + TAXA_ENTREGA;
        }

        // ===== MÉTODO DE EXIBIÇÃO =====

        /// <summary>
        /// Gera o resumo completo do pedido em formato de texto,
        /// pronto para ser exibido no console. Inclui dados do cliente,
        /// lista de itens, cálculos e total final.
        /// </summary>
        /// <return>String formada com o resumo do pedido</return>
        public string GerarResumo()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("═══════════════════════════════════════════════════════");
            sb.AppendLine("           🧾 RESUMO DO PEDIDO");
            sb.AppendLine("═══════════════════════════════════════════════════════");
            sb.AppendLine($"📅 Data: {DataPedido:dd/MM/yyyy HH:mm}");
            sb.AppendLine();

            // Exibe os dados do cliente (usando o método da classe Cliente)
            sb.AppendLine(Cliente.ExibirDados());
            sb.AppendLine();

            sb.AppendLine("───────────────────────────────────────────────────────");
            sb.AppendLine("🛒 ITENS DO PEDIDO:");
            sb.AppendLine("───────────────────────────────────────────────────────");

            if (CarrinhoVazio())
            {
                sb.AppendLine("     (Carrinho vazio)");
            }
            else
            {
                // Aqui está o POLIMORFISMO no seu auge:
                // chamamos ExibirDetalhes() em cada item, mas cada tipo
                // (Lanche, Bebida, Sobremesa, Combo) responde do seu jeito.
                int numero = 1;
                foreach (Produto item in _itens)
                {
                    sb.AppendLine($"{numero}. {item.ExibirDetalhes()}");
                    numero++;
                }
            }

            sb.AppendLine();
            sb.AppendLine("───────────────────────────────────────────────────────");
            sb.AppendLine("💵 VALORES:");
            sb.AppendLine("───────────────────────────────────────────────────────");

            double subtotal = CalcularSubtotal();
            double desconto = CalcularDesconto();
            double total = CalcularTotal();

            sb.AppendLine($"   Subtotal:        R$ {subtotal,8:F2}");

            // Só mostra a linha de desconto se houver desconto aplicado
            if (desconto > 0)
            {
                sb.AppendLine($"    Desconto (10%): -R$ {desconto,8:F2}");
            }

            sb.AppendLine($"   Taxa de entrega:+R$ {TAXA_ENTREGA,8:F2}");
            sb.AppendLine("───────────────────────────────────────────────────────");
            sb.AppendLine($"   TOTAL:           R$ {total,8:F2}");
            sb.AppendLine("═══════════════════════════════════════════════════════");

            return sb.ToString();
        }
    }
}