namespace LanchoneSistema01.Modelos
{
    /// <summary>
    /// Representa um Cliente da lanchonete que faz pedidos.
    /// Armazena dados básicos para identificação e entrega.
    /// 
    /// Esta classe não herda de nenhuma outra, pois um cliente é uma
    /// entidade independente no sistema (não é um tipo de produto).
    /// </summary>
    public class Cliente
    {
        // ===== ATRIBUTOS PRIVADOS =====
        // Todos privados, seguindo o pilar do ENCAPSULAMENTO.
        // O acesso externo só acontece através das propriedades.
        private string _nome;
        private string _endereco;
        private string _telefone;

        // ===== PROPRIEDADES =====

        /// <summary>
        /// Nomme completo do cliente.
        /// </summary>
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do cliente não pode ser vazio.");
                _nome = value;
            }
        }

        /// <summary>
        /// Endereço completo para entrega (rua, número, bairro).
        /// </summary>
        public string Endereco
        {
            get { return _endereco; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O endereço não pode ser vazio.");
                _endereco = value;
            }
        }

        /// <summary>
        /// Telefone de contato do cliente.
        /// </summary>
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O  telefone não pode ser vazio.");
                _telefone = value;
            }
        }

        // ===== CONSTRUTOR =====
        /// <summary>
        /// Cria um novo Cliente com seus dados básicos.
        /// </summary>
        /// <param name="nome">Nome completo do cliente</param>
        /// <param name="endereco">Endereço para entrega</param>
        /// <param name="telefone">Telefone de contato</param>
        public Cliente(string nome, string endereco, string telefone)
        {
            // Atribuímos via propriedades para que as validações sejam executadas
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }

        // ===== MÉTODO AUXILIAR =====
        /// <summary>
        /// Retorna uma string formatada com os dados do cliente,
        /// útil para exibir no resumo do pedido.
        /// </summary>
        public string ExibirDados()
        {
            // Usa StringBuilder pra formar a string de saída de forma organizada
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"    Cliente: {Nome}");
            sb.AppendLine($"    Endereço: {Endereco}");
            sb.AppendLine($"    Telefone: {Telefone}");
            return sb.ToString().TrimEnd();
        }
    }
}