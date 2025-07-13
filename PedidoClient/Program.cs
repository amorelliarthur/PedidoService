using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoClient.PedidoServiceReference;

namespace PedidoClient
{
    class Program
    {
        static PedidoServiceClient client = new PedidoServiceClient();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Sistema de Pedidos =====");
                Console.WriteLine("1. Criar Pedido");
                Console.WriteLine("2. Listar Pedidos");
                Console.WriteLine("3. Buscar Pedido por ID");
                Console.WriteLine("4. Atualizar Pedido");
                Console.WriteLine("5. Atualizar Status do Pedido");
                Console.WriteLine("6. Excluir Pedido");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) continue;

                Console.Clear();
                switch (opcao)
                {
                    case 1: CriarPedido(); break;
                    case 2: ListarPedidos(); break;
                    case 3: BuscarPorId(); break;
                    case 4: AtualizarPedido(); break;
                    case 5: AtualizarStatus(); break;
                    case 6: ExcluirPedido(); break;
                    case 0: client.Close(); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        static void CriarPedido()
        {

            string cliente;
            while (true)
            {
                Console.Write("Cliente: ");
                cliente = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(cliente))
                    break;

                Console.WriteLine("Nome do cliente não pode estar vazio.");
            }

            var produtos = new List<Produto>();
            
            while (true)
            {
                Console.Write("Produto (deixe vazio para terminar): ");
                string nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) break;

                // Valida nome
                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("Nome do produto não pode estar vazio.");
                    continue;
                }

                // Valida preço
                decimal preco;
                while (true)
                {
                    Console.Write("Preço: ");
                    var input = Console.ReadLine();
                    if (decimal.TryParse(input, out preco))
                        break;
                    Console.WriteLine("Preço inválido. Digite um valor numérico válido.");
                }

                produtos.Add(new Produto { Nome = nome, Preco = preco });
            }

            var pedido = new Pedido
            {
                Cliente = cliente,
                Produtos = produtos.ToArray(),
                Status = "Recebido"
            };

            client.CriarPedido(pedido);
            Console.WriteLine("Pedido criado com sucesso.");
        }

        static void ListarPedidos()
        {
            var pedidos = client.ListarPedidos();
            
            foreach (var pedido in pedidos)
            {
                decimal total = pedido.Produtos.Sum(p => p.Preco);

                Console.WriteLine($"\nID: {pedido.Id} | Cliente: {pedido.Cliente}  | Status: {pedido.Status} | Valor total: R$ {total}");

                foreach (var produto in pedido.Produtos)
                {
                    Console.WriteLine($" - Produto: {produto.Nome} | Preço: R$ {produto.Preco}");
                }
            }
        }

        static void BuscarPorId()
        {
            Console.Write("Digite o ID: ");
            int id = int.Parse(Console.ReadLine());

            var pedido = client.BuscarPedidoPorId(id);
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            decimal total = pedido.Produtos.Sum(p => p.Preco);
            Console.WriteLine($"\nID: {pedido.Id} | Cliente: {pedido.Cliente}  | Status: {pedido.Status} | Valor total: R$ {total}");

            foreach (var produto in pedido.Produtos)
            {
                Console.WriteLine($" - Produto: {produto.Nome} | Preço: R$ {produto.Preco}");
            }


        }

        static void AtualizarPedido()
        {
            Console.Write("ID do pedido a atualizar: ");
            int id = int.Parse(Console.ReadLine());

            var pedido = client.BuscarPedidoPorId(id);
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            decimal total = pedido.Produtos.Sum(p => p.Preco);
            Console.WriteLine($"\nPedido encontrado:");
            Console.WriteLine($"ID: {pedido.Id} | Cliente: {pedido.Cliente} | Status: {pedido.Status} | Valor total: R$ {total}");

            foreach (var produto in pedido.Produtos)
            {
                Console.WriteLine($" - Produto: {produto.Nome} | Preço: R$ {produto.Preco}");
            }

            string novoCliente;
            while (true)
            {
                Console.Write("\nCliente: ");
                novoCliente = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novoCliente))
                    break;

                Console.WriteLine("Nome do cliente não pode estar vazio.");
            }
            pedido.Cliente = novoCliente;

            var novosProdutos = new List<Produto>();
            while (true)
            {
                Console.Write("Produto (deixe vazio para terminar): ");
                string nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) break;

                decimal preco;
                while (true)
                {
                    Console.Write("Preço: ");
                    var input = Console.ReadLine();
                    if (decimal.TryParse(input, out preco))
                        break;
                    Console.WriteLine("Preço inválido. Digite um valor numérico válido.");
                }

                novosProdutos.Add(new Produto { Nome = nome, Preco = preco });
            }

            pedido.Produtos = novosProdutos.ToArray();
            pedido.Status = SelecionarStatus();

            client.AtualizarPedido(pedido);
            Console.WriteLine("Pedido atualizado.");
        }

        static void AtualizarStatus()
        {
            Console.Write("ID do pedido: ");
            int id = int.Parse(Console.ReadLine());

            var pedido = client.BuscarPedidoPorId(id);
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            decimal total = pedido.Produtos.Sum(p => p.Preco);
            Console.WriteLine($"\nPedido encontrado:");
            Console.WriteLine($"ID: {pedido.Id} | Cliente: {pedido.Cliente} | Status atual: {pedido.Status} | Valor total: R$ {total}");

            foreach (var produto in pedido.Produtos)
            {
                Console.WriteLine($" - Produto: {produto.Nome} | Preço: R$ {produto.Preco}");
            }

            string novoStatus = SelecionarStatus();
            client.AtualizarStatus(id, novoStatus);
            Console.WriteLine("Status atualizado.");

        }

        static void ExcluirPedido()
        {
            Console.Write("ID do pedido a excluir: ");
            int id = int.Parse(Console.ReadLine());

            client.ExcluirPedido(id);
            Console.WriteLine("Pedido excluído.");
        }

        static string SelecionarStatus()
        {
            Console.WriteLine("\nSelecione o novo status:");
            Console.WriteLine("1 - Recebido");
            Console.WriteLine("2 - Processando");
            Console.WriteLine("3 - Saiu para entrega");
            Console.WriteLine("4 - Entregue");
            Console.WriteLine("5 - Finalizado");
            Console.Write("\nEscolha: ");
            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1: return "Recebido";
                case 2: return "Processando";
                case 3: return "Saiu para entrega";
                case 4: return "Entregue";
                case 5: return "Finalizado";
                default: return "Recebido";
            }
        }
    }
}
