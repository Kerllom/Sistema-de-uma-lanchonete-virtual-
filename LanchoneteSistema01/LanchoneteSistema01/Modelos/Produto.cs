namespace LanchoneteSistema01.Modelos
{
    /// <summary>
    /// Classe abstrata que representa um produto genérico da lanchonete.
    /// Serve como BASE (molde) para todos os tipos específicos de produto: 
    /// Lanche, Bebida, Sobremesa e Combo. 

    /// Por ser ABSTRATA, esta classe NÃO pode ser instanciada diretamente.
    /// Ou seja, não existe "um produto genérico" no mundo real - sempre
    /// será um lanche, uma bebida, etc. Isso é o pilar da ABSTRAÇÃO.
    /// </summary>
    public abstract class Produto
    {
        // ===== ATRIBUTOS PRIVADOS =====
        // Os atributos são marcados como 'private' para que NINGUÉM de fora
        // da classe consiga acessá-los diretamente. Isso é ENCAPSULAMENTO:
        // protegemos os dados internos e só permitimos acesso através de
        // propriedades (get/set), que podem ter validações.
        
        private string _nome;
        private double _preco;

        // ===== PROPRIEDADES (GETTERS E SETTERS) =====
        // Em C#, usamos PROPRIEDADES no lugar dos métodos getNome()/setNome()
        // do Java. Elas funcionam como um "intermediário" entre o mundo
        // externo e o atributo privado, permitindo validar os dados.
        
        /// <summary>
        /// Nome do produto (ex: "X-Burguer", "Coca-Cola").
        /// Não pode ser nulo nem vazio.
        /// </summary> 
        public string Nome
        {
            get { return _nome; }
            set
            {
                // Validação: não permite nome vazio ou nulo
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do produto não pode ser vazio.");
                _nome = value;
            }
        }

        /// <summary>
        /// Preço do produto em reais. Deve ser maior que zero.
        /// </summary>
        public double Preco
        {
            get { return _preco; }
            set
            {
                // Validação: preço não pode ser negativo ou zero
                if (value <= 0)
                    throw new ArgumentException("O preço deve ser maior que zero.");
                _preco = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Construtor da classe Produto. Como é abstrata, este construtor
        /// será chamado pelas classes filhas através da palavra 'base'.
        /// </summary>
        /// <param name="nome">Nome do produto</param>
        /// <param name="preco">Preço do produto (deve ser > 0)</param>
        public Produto(string nome, double preco)
        {
            // Atribuímos via PROPRIEDADE (e não direto do atributo)
            // Para que as validações dos setters sejam executadas.
            Nome = nome;
            Preco = preco;
        }

        // ===== MÉTODO ABSTRATO =====
        /// <summary>
        /// Método abstrato que exibe os detalhes do produto.
        /// Cada classe filha (Lanche, Bebida, etc.) deve implementar
        /// este método Pa sua maneira. Isso é o pilar do POLIMORFISMO:
        /// o mesmo método se comporta diferente dependendo do tipo real 
        /// do objeto.
        /// </summary>
        public abstract string ExibirDetalhes();
    }
}