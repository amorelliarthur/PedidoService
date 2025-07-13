# Sistema de Emissão e Acompanhamento de Pedidos (SOAP - WCF)

Este projeto foi desenvolvido com o objetivo de implementar um serviço SOAP utilizando WCF em .NET para simular um sistema de emissão e acompanhamento de pedidos.

---

## Tecnologias utilizadas

- .NET Framework (WCF - Windows Communication Foundation)
- Visual Studio
- C#
- SOAP (WSDL + tipos complexos)
- WCF Test Client (para testes)
- Aplicação Console (cliente)

---

## Estrutura da Solução

A solução contém **dois projetos**:

1. `PedidoService` — Projeto WCF que expõe o serviço SOAP.
2. `PedidoClient` — Aplicação console que consome o serviço via referência WSDL.

---

## Modelos usados no serviço

### `Produto`
```csharp
public class Produto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}
```

### `Pedido`
```csharp
public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; }
    public List<Produto> Produtos { get; set; }
    public string Status { get; set; } // Ex: "Recebido", "Processando", "Finalizado"
}
```

---

## Funcionalidades implementadas

- **CriarPedido(Pedido pedido)**  
  Adiciona um novo pedido. O status inicial é `"Recebido"`.

- **ListarPedidos()**  
  Retorna todos os pedidos criados.

- **BuscarPedidoPorId(int id)**  
  Busca um pedido pelo seu ID.

- **AtualizarPedido(Pedido pedido)**  
  Atualiza os dados de um pedido (cliente, produtos, preço, status).

- **AtualizarStatus(int id, string novoStatus)**  
  Atualiza somente o status do pedido.

- **ExcluirPedido(int id)**  
  Remove o pedido pelo ID.

---

## Como executar o projeto

### Pré-requisitos

- Visual Studio 2019 ou superior
- .NET Framework instalado
- WCF Test Client (já incluso no Visual Studio)

### Passo a passo

1. **Clone o repositório**

2. **Abra a solução (`.sln`) no Visual Studio**

3. **Execute o projeto WCF (`PedidoService`)**
   - Defina como projeto de inicialização (`Set as Startup Project`)
   - Pressione `F5` para iniciar
   - O navegador abrirá com a URL do serviço (ex: `http://localhost:xxxx/PedidoService.svc`)

4. **Verifique o WSDL** (se necessário)
   - `http://localhost:xxxx/PedidoService.svc?wsdl`

5. **Abra o projeto `PedidoClient` (console)**

6. **Adicione a referência de serviço:**
   - Clique com o direito no projeto `PedidoClient` > `Add Service Reference...`
   - No campo "Address", informe a URL do WSDL
   - Clique em `Go`, selecione o serviço e finalize

7. **Execute o projeto `PedidoClient`**
   - Defina como projeto de inicialização (`Set as Startup Project`)
   - Execute com `Ctrl + F5`
   - Use o menu interativo no console para testar as funcionalidades (CRUD + Status)

---

## Status disponíveis

- `Recebido`
- `Processando`
- `Saiu para entrega`
- `Entregue`
- `Finalizado`

---

## Organização dos arquivos

### Projeto `PedidoService` (servidor)
- `PedidoService.svc`: entrada principal
- `IPedidoService.cs`: interface com os contratos SOAP
- `PedidoService.svc.cs`: implementação do CRUD
- `Models/Produto.cs`, `Models/Pedido.cs`: modelos de dados

### Projeto `PedidoClient` (console)
- `Program.cs`: menu interativo com todas as chamadas SOAP
- `PedidoServiceReference`: referência ao WSDL do serviço

---

## Observações

- Dados são mantidos em memória (não persiste em banco).
- O serviço e o cliente funcionam localmente via SOAP.
- Tipos complexos como listas e objetos aninhados foram utilizados.
- Todas as funcionalidades foram testadas com sucesso.
