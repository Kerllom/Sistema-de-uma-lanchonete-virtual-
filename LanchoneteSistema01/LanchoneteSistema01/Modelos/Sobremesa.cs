using LanchoneteSistema01.Modelos;

namespace LanchoneSistema01.Modelos
{
    /// <summary>
    /// Representa uma Sobremesa da lanchonete (ex: Sorvete, Brownie).
    /// Herda de Produto e adiciona o atributo "tipo" (gelada, quente, etc.).
    /// </summary>
    public class Sobremesa : Produto
    {
        // ===== ATRIBUTO ESPECÍFICO DA SOBREMESA =====
        private string _tipo;

        /// <summary>
        /// Tipo da sobremesa (ex: "Gelada", "Quente", "Cremosa").
        /// </summary>
        public string Tipo
        {
            get { return _tipo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O tipo da sobremesa não pode ser vazio.");
                _tipo = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria uma nova Sobremesa.
        /// </summary>
        /// <param name="nome">Nome da sobremesa</param>
        /// <param name="preco">Preço da sobremesa</param>
        /// <param name="tipo">Tipo (Gelada, Quente, etc.)</param>
        public Sobremesa(string nome, double preco, string tipo) 
            : base(nome, preco)
        {
            Tipo = tipo;
        }

        // ===== IMPLEMENTAÇÃO DO MÉTODO ABSTRATO =====
        /// <summary>
        /// Exibe os detalhes da sobremesa, incluindo seu tipo.
        /// </summary>
        public override string ExibirDetalhes()
        {
            return $" Sobremesa: {Nome} | Preço: R$ {Preco:F2} | Tipo: {Tipo}";
        }
    }
}