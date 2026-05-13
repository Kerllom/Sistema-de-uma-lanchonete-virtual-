using LanchoneteSistema01.Modelos;

namespace LanchoneSistema01.Modelos
{
    /// <summary>
    /// Representa uma Bebida da lanchonete (ex: Coca-Cola, Suco).
    /// Herda de Produto e adiciona o atributo de tamanho em mililitros.
    /// </summary>
    public class Bebida : Produto
    {
        // ===== ATRIBUTO ESPECÍFICO DA BEBIDA =====
        private int _tamanhoML;

        /// <summary>
        /// Tamanho da bebida em mililitros (ex: 350, 500, 1000).
        /// Deve ser maior que zero.
        /// </summary>
        public int TamanhoML
        {
            get { return _tamanhoML; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O tamanho da bebida deve ser maior que zero.");
                _tamanhoML = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria uma nova Bebida.
        /// </summary>
        /// <param name="nome">Nome da bebida</param>
        /// <param name="preco">Preço da bebida</param>
        /// <param name="tamanhoML">Tamanho em mililitros</param>
        public Bebida(string nome, double preco, int tamanhoML)
            : base(nome, preco)
        {
            TamanhoML = tamanhoML;
        }

        // ===== IMPLEMENTAÇÃO DO MÉTODO ABSTRATO =====
        /// <summary>
        /// Exibe os detalhes da bebida, mostrando o tamanho em ML.
        /// </summary>
        public override string ExibirDetalhes()
        {
            return $" Bebida: {Nome} | Preço: R$ {Preco:F2} | Tamanho: {TamanhoML}ml";
        }
    }
}