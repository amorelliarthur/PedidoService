using System.Runtime.Serialization;

namespace PedidoService
{
    [DataContract]
    public class Produto
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public decimal Preco { get; set; }
    }
}
