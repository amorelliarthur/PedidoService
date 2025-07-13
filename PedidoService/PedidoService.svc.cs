using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PedidoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PedidoService : IPedidoService
    {
        // Lista estática para simular o banco de dados
        private static List<Pedido> pedidos = new List<Pedido>();
        private static int proximoId = 1;

        public void CriarPedido(Pedido pedido)
        {
            pedido.Id = proximoId++;
            pedido.Status = "Recebido"; // Força o status inicial
            pedidos.Add(pedido);
        }

        public List<Pedido> ListarPedidos()
        {
            return pedidos;
        }

        public Pedido BuscarPedidoPorId(int id)
        {
            return pedidos.FirstOrDefault(p => p.Id == id);
        }

        public void AtualizarPedido(Pedido pedido)
        {
            var existente = pedidos.FirstOrDefault(p => p.Id == pedido.Id);
            if (existente != null)
            {
                existente.Cliente = pedido.Cliente;
                existente.Produtos = pedido.Produtos;
                existente.Status = pedido.Status;
            }
        }

        public void AtualizarStatus(int id, string novoStatus)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
            }
        }

        public void ExcluirPedido(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                pedidos.Remove(pedido);
            }
        }
    }
}
