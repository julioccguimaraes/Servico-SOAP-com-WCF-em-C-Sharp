using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EstoqueLibrary
{
    // Data contract describing the details of a product passed to client applications
    [DataContract]
    public class Produto
    {
        [DataMember]
        private string numeroProduto;

        [DataMember]
        private string nomeProduto;

        [DataMember]
        private string descricaoProduto;

        [DataMember]
        private int estoqueProduto;

        public string NumeroProduto { get => numeroProduto; set => numeroProduto = value; }
        public string NomeProduto { get => nomeProduto; set => nomeProduto = value; }
        public string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }
        public int EstoqueProduto { get => estoqueProduto; set => estoqueProduto = value; }
    }

    // Service contract describing the operations provided by the WCF service
    [ServiceContract(Namespace = "http://projetoavaliativo.dm113/01", Name = "IServicoEstoque")]
    public interface IServicoEstoque
    {
        // Get all products
        [OperationContract]
        List<string> ListarProdutos();

        // Add a product
        [OperationContract]
        bool IncluirProduto(Produto produto);

        // Delete a product
        [OperationContract]
        bool RemoverProduto(string NumeroProduto);

        // Get the current stock for a product
        [OperationContract]
        int ConsultarEstoque(string NumeroProduto);

        // Add stock for a product
        [OperationContract]
        bool AdicionarEstoque(string NumeroProduto, int Quantidade);

        // Subtract stock for a product
        [OperationContract]
        bool RemoverEstoque(string NumeroProduto, int Quantidade);

        // Get the details of a single product
        [OperationContract]
        Produto VerProduto(string NumeroProduto);
    }

    [ServiceContract(Namespace = "http://projetoavaliativo.dm113/02", Name = "IServicoEstoqueV2")]
    public interface IServicoEstoqueV2
    {
        // Add stock for a product
        [OperationContract]
        bool AdicionarEstoque(string NumeroProduto, int EstoqueProduto);

        // Subtract stock for a product
        [OperationContract]
        bool RemoverEstoque(string NumeroProduto, int EstoqueProduto);

        // Get the current stock for a product
        [OperationContract]
        int ConsultarEstoque(string NumeroProduto);
    }
}
