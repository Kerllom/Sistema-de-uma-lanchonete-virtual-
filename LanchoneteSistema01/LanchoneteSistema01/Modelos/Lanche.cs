namespace LanchoneteSistema01.Modelos
{
    /// <summary>
    /// Representa um Lanche da lanchonete (ex: X-Burguer, X-Salada).
    /// Herda todos os atributos e comportamentos de Produto e adiciona
    /// uma característica específica: a lista de ingredientes.
    /// </summary>
    public class Lanche : Produto
    {
        // ===== ATRIBUTO ESPECÍFICO DO LANCHE =====
        // Usamos List<string> para armazenar os ingredientes.
        // É privado, seguindo o encapsulamento.
        private List<string> _ingredientes;

        /// <summary>
        /// Lista de ingredientes do lanche (ex: pão, carne, queijo).
        /// </summary>
        public List<string> Ingredientes
        {
            get { return _ingredientes; }
            set
            {
                // Validação: a lista não pode ser nula
                if (value == null)
                    throw new ArgumentException("A lista de ingredientes não pode ser nula.");
                _ingredientes = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria um novo Lanche.
        /// </summary>
        /// <param name="nome">Nome do lanche</param>
        /// <param name="preco">Preço do lanche</param>
        /// <param name="ingredientes">Lista de ingredientes</param>
        public Lanche(string nome, double preco, List<string> ingredientes)
            : base(nome, preco)
        // 'base(nome, preco)' chama o construtor da classe PAI (Produto),
        // passando o nome e o preço para ele cuidar das validações.
        // Isso é HERANÇA na prática: reaproveitamos a lógica do pai.
        {
            Ingredientes = ingredientes;
        }

        // ===== IMPLEMENTAÇÃO DO MÉTODO ABSTRATO =====
        /// <summary>
        /// Sobrescreve (override) o método abstrato de Produto.
        /// Exibe os detalhes do lanche incluindo seus ingredientes.
        /// </summary>
        public override string ExibirDetalhes()
        {
            // string.Join junta todos os itens da lista separados por vírgula
            string ingredientesTexto = string.Join(", ", _ingredientes);
            return $" Lanche: {Nome} | Preço: R$ {Preco:F2} | Ingredientes: {ingredientesTexto}";
        }
    }
}