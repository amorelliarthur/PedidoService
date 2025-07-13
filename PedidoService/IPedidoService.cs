using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PedidoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPedidoService
    {
        // TODO: Add your service operations here
        [OperationContract]
        void CriarPedido(Pedido pedido);

        [OperationContract]
        List<Pedido> ListarPedidos();

        [OperationContract]
        Pedido BuscarPedidoPorId(int id);

        [OperationContract]
        void AtualizarPedido(Pedido pedido);

        [OperationContract]
        void AtualizarStatus(int id, string novoStatus);

        [OperationContract]
        void ExcluirPedido(int id);
    }    
}
