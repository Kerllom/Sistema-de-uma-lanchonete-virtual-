using LanchoneteSistema01.Modelos;

namespace LanchoneSistema01.Modelos
{
    /// <summary>
    /// Representa um Combo da lanchonete, que é um conjunto de outros produtos
    /// vendidos juntos por um preço fechado (geralmente promocional).
    /// 
    /// IMPORTANTE: Combo HERDA de Produto, então ele pode ser tratado como 
    /// um produto qualquer. Mas também contém uma lista de produtos dentro de si.
    /// Esse padrão (objeto que é-um e tem-um do mesmo tipo) chama-se COMPOSITE.
    /// </summary>
    public class Combo : Produto
    {
        // ===== ATRIBUTO ESPECÍFICO DO COMBO =====
        // Uma lista de produtos que fazem parte do combo.
        // Repare: como Combo também é um Produto, em teoria poderíamos colocar
        // um combo dentro de outro combo! Não vamos fazer isso aqui, mas é
        // uma demonstração do poder da herança.
        private List<Produto> _itens;

        /// <summary>
        /// Lista dos produtos que compôem o combo.
        /// </summary>
        public List<Produto> Itens
        {
            get { return _itens; }
            set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentException("O combo deve ter pelo menos um item.");
                _itens = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria um novo Combo com nome, preço promocional e lista de itens.
        /// </summary>
        /// <param name="nome">Nome do combo (ex: "Combo Família")</param>
        /// <param name="preco">Preço fechado do combo (já com desconto embutido)</param>
        /// <param name="itens">Lista de produtos que compôem o combo</param>
        public Combo(string nome, double preco, List<Produto> itens)
            : base(nome, preco)
        {
            Itens = itens;
        }

        // ===== MÉTODO AUXILIAR =====
        /// <summary>
        /// Calcula quanto custaria comprar cada item do combo separadamente.
        /// Útil para mostrar ao cliente o quanto ele está economizando.
        /// </summary>
        /// <returns>Soma dos preços individuais dos itens</returns>
        public double CalcularPrecoSeparado()
        {
            double total = 0;
            // Percorre cada produto da lista e soma seu preço.
            // Aqui o POLIMORFISMO age silenciosamente: não importa se o item
            // é um Lanche, Bebida ou Sobremesa, todos têm a propriedade Preco
            // (herdada de Produto).
            foreach (Produto item in _itens)
            {
                total += item.Preco;
            }
            return total;
        }

        /// <summary>
        /// Calcula o desconto (em reais) que o combo oferece em relação
        /// a comprar os intens separadamente.
        /// </summary>
        public double CalcularEconomia()
        {
            return CalcularPrecoSeparado() - Preco;
        }

        // ===== IMPLEMENTAÇÃO DO MÉTODO ABSTRATO =====
        /// <summary>
        /// Exibe os detalhes do combo, listando todos os seus itens
        /// e mostrando a economia em relação à compra separada.
        /// </summary>
        public override string ExibirDetalhes()
        {
            // StringBuilder é mais eficientes que concatenar strings com +
            // quando temos muitas concatenações. É uma boa prática.
            var sb = new System.Text.StringBuilder();

            sb.AppendLine($" Combo: {Nome} | Preço: R$ {Preco:F2}");
            sb.AppendLine($"    Itens inclusos:");

            // Aqui está o POLIMORFISMO em ação NOVAMENTE:
            // Chamamos ExibirDetalhes() em cada item, mas cada um responde
            // de forma diferente (Lanche mostra ingredientes, Bebida mostra ml...)
            foreach (Produto item in _itens)
            {
                sb.AppendLine($"   -> {item.ExibirDetalhes()}");
            }

            double economia = CalcularEconomia();
            if (economia > 0)
            {
                sb.AppendLine($"    Economia em relação à compra separada: R$ {economia:F2}");
            }

            // TrimEnd() remove a última quebra de linha para ficar mais limpo
            return sb.ToString().TrimEnd();
        }
    }
}