using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PedidoService
{
    [DataContract]
    public class Pedido
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Cliente { get; set; }

        [DataMember]
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        [DataMember]
        public string Status { get; set; } = "Recebido"; // valor padrão

    }
}
