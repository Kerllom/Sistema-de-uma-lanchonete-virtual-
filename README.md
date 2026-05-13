# 🍔 Sistema de Lanchonete Virtual

[![C#](https://img.shields.io/badge/Linguagem-C%23-512BD4?style=flat-square&logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Visual Studio](https://img.shields.io/badge/IDE-Visual%20Studio%202022-5C2D91?style=flat-square&logo=visualstudio)](https://visualstudio.microsoft.com/)
[![Status](https://img.shields.io/badge/Status-Concluído-success?style=flat-square)]()

Sistema back-end de uma lanchonete virtual desenvolvido em **C#**, totalmente baseado nos pilares da **Programação Orientada a Objetos**. Simula a operação de um delivery (estilo iFood), permitindo cadastro de cliente, navegação por cardápio, montagem de carrinho de compras, aplicação de regras de negócio (desconto e taxa de entrega) e finalização de pedido com resumo detalhado.

---

## 📋 Sobre o Projeto

Aplicação **Console** que simula o fluxo completo de um pedido em uma lanchonete:

- O cliente se cadastra ao iniciar o sistema.
- Visualiza um cardápio com lanches, bebidas, sobremesas e combos.
- Monta seu carrinho de compras adicionando ou removendo itens.
- Recebe automaticamente um **desconto de 10%** caso o subtotal ultrapasse **R$ 50,00**.
- Tem uma **taxa de entrega fixa de R$ 10,00** somada ao total.
- Finaliza o pedido recebendo um **resumo detalhado** com todos os valores.

> 📚 *Projeto desenvolvido como exercício prático de Programação Orientada a Objetos.*

---

## ✨ Funcionalidades

- ✅ Cadastro de cliente com validação de dados.
- ✅ Cardápio pré-cadastrado com **lanches, bebidas, sobremesas e combos**.
- ✅ Adicionar e remover itens do carrinho.
- ✅ Visualização do carrinho com subtotal parcial.
- ✅ Cálculo automático de desconto (10% acima de R$ 50,00).
- ✅ Taxa de entrega fixa de R$ 10,00.
- ✅ Resumo completo do pedido com data, cliente, itens e valores.
- ✅ Validação de entradas do usuário (tratamento de erros).
- ✅ Interface interativa via terminal com menu numerado.

---

## 🧱 Pilares de POO Aplicados

Cada pilar da POO está aplicado de forma intencional no projeto:

### 🔹 Abstração
A classe `Produto` é declarada como **abstrata**, representando o conceito genérico de um item da lanchonete. Não pode ser instanciada diretamente — só por suas filhas concretas.

### 🔹 Encapsulamento
Todos os atributos das classes são `private` e o acesso externo é feito **exclusivamente através de propriedades (get/set)** com validações. Exemplo: o preço de um produto não pode ser negativo, o nome do cliente não pode ser vazio, etc.

### 🔹 Herança
As classes `Lanche`, `Bebida`, `Sobremesa` e `Combo` **herdam** de `Produto`, reaproveitando seus atributos e comportamentos básicos.

### 🔹 Polimorfismo
O método `ExibirDetalhes()` é declarado como **abstrato** em `Produto` e implementado de forma específica em cada classe filha. Isso permite que listas de `Produto` contenham qualquer tipo de item e que cada um se comporte do seu jeito ao ser exibido.

---

## 🎯 Princípios de Design Aplicados

Além dos pilares de POO, o projeto aplica:

- **Princípio Aberto/Fechado (SOLID)** — a estrutura aceita novos tipos de produto (ex: Açaí, Pizza) sem necessidade de alterar código existente.
- **Padrão Composite** — a classe `Combo` é um Produto que contém outros Produtos, formando uma estrutura hierárquica.
- **Responsabilidade Única** — cada classe tem um propósito claro e bem definido.
- **Agregação** — `Pedido` agrega `Cliente` e uma lista de `Produto`, mas cada um tem vida independente.

---

## 📂 Estrutura do Projeto

```
Sistema-de-uma-lanchonete-virtual-/
│
├── LanchoneteSistema/
│   ├── Modelos/
│   │   ├── Produto.cs        # Classe abstrata base
│   │   ├── Lanche.cs         # Herda de Produto
│   │   ├── Bebida.cs         # Herda de Produto
│   │   ├── Sobremesa.cs      # Herda de Produto
│   │   ├── Combo.cs          # Herda de Produto + composição
│   │   ├── Cliente.cs        # Entidade independente
│   │   └── Pedido.cs         # Agrega Cliente + Produtos
│   │
│   ├── Program.cs            # Menu interativo (orquestração)
│   └── LanchoneteSistema.csproj
│
└── README.md
```

---

## 🗺️ Diagrama de Classes (Visão Geral)

```
                 ┌──────────────────────┐
                 │   Produto (abstract) │
                 │----------------------│
                 │ - nome: string       │
                 │ - preco: double      │
                 │----------------------│
                 │ + ExibirDetalhes()   │ ← método abstrato
                 └──────────▲───────────┘
                            │ herança
        ┌───────────┬───────┴────────┬────────────┐
        │           │                │            │
   ┌────┴────┐ ┌────┴────┐    ┌──────┴────┐ ┌─────┴─────┐
   │ Lanche  │ │ Bebida  │    │ Sobremesa │ │   Combo   │
   │---------│ │---------│    │-----------│ │-----------│
   │ingred.  │ │tamanhoML│    │   tipo    │ │  itens[]  │
   └─────────┘ └─────────┘    └───────────┘ └─────┬─────┘
                                                  │ composição
                                                  ▼
                                          ┌──────────────┐
                                          │   Produto    │ (lista)
                                          └──────────────┘

   ┌──────────────────┐               ┌──────────────────┐
   │     Cliente      │◄──────tem-um──│     Pedido       │
   │------------------│               │------------------│
   │ nome             │               │ cliente          │
   │ endereco         │               │ itens: List<P.>  │──tem-vários──► Produto
   │ telefone         │               │ dataPedido       │
   └──────────────────┘               │------------------│
                                      │ AdicionarItem()  │
                                      │ RemoverItem()    │
                                      │ CalcularTotal()  │
                                      │ GerarResumo()    │
                                      └──────────────────┘
```

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# (.NET 8.0)
- **IDE:** Visual Studio 2022
- **Tipo de Aplicação:** Console
- **Paradigma:** Programação Orientada a Objetos

---

## 🚀 Como Executar

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) **OU** [VS Code](https://code.visualstudio.com/) com extensão C#

### Passo a passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/KerllomLuis/Sistema-de-uma-lanchonete-virtual-.git
   ```

2. **Acesse a pasta do projeto:**
   ```bash
   cd Sistema-de-uma-lanchonete-virtual-
   ```

3. **Execute a aplicação:**

   **Pelo Visual Studio 2022:**
   - Abra o arquivo `.sln` no Visual Studio.
   - Pressione `F5` ou clique no botão **▶ Iniciar**.

   **Pelo terminal (.NET CLI):**
   ```bash
   dotnet run
   ```

---

## 💻 Exemplo de Uso

Ao iniciar, o sistema solicita o cadastro do cliente e em seguida apresenta o menu principal:

```
═══════════════════════════════════════════════════════
        🍔 BEM-VINDO À LANCHONETE DO CLAUDE 🍔
═══════════════════════════════════════════════════════

📝 Antes de começar, precisamos dos seus dados:

Nome completo: Kerllom Luis
Endereço completo: Rua das Flores, 100
Telefone: (27) 99999-8888

✅ Cliente cadastrado com sucesso, Kerllom Luis!
```

Depois de montar o carrinho e finalizar, o resumo é exibido:

```
═══════════════════════════════════════════════════════
           🧾 RESUMO DO PEDIDO
═══════════════════════════════════════════════════════
📅 Data: 13/05/2026 14:30

👤 Cliente: Kerllom Luis
📍 Endereço: Rua das Flores, 100
📞 Telefone: (27) 99999-8888

───────────────────────────────────────────────────────
🛒 ITENS DO PEDIDO:
───────────────────────────────────────────────────────
1. 🍔 Lanche: X-Bacon | Preço: R$ 22,00 | Ingredientes: ...
2. 🍔 Lanche: X-Tudo | Preço: R$ 28,00 | Ingredientes: ...
3. 🥤 Bebida: Coca-Cola | Preço: R$ 8,00 | Tamanho: 350ml

───────────────────────────────────────────────────────
💵 VALORES:
───────────────────────────────────────────────────────
   Subtotal:        R$    58,00
   Desconto (10%): -R$     5,80
   Taxa de entrega:+R$    10,00
───────────────────────────────────────────────────────
   TOTAL:           R$    62,20
═══════════════════════════════════════════════════════
```

---

## 📐 Regras de Negócio

| Regra | Valor |
|---|---|
| Taxa de entrega (fixa) | **R$ 10,00** |
| Valor mínimo para desconto | **Acima de R$ 50,00** |
| Percentual de desconto | **10% sobre o subtotal** |
| Ordem do cálculo | Subtotal → Desconto → + Taxa |

---

## 👨‍💻 Autor

**Kerllom Luis**

[![GitHub](https://img.shields.io/badge/GitHub-KerllomLuis-181717?style=flat-square&logo=github)](https://github.com/KerllomLuis)

---

## 📝 Licença

Este projeto está sob a licença MIT. Sinta-se livre para estudar, modificar e usar como referência.

---

⭐ *Se este projeto te ajudou de alguma forma, considere deixar uma estrela no repositório!*
