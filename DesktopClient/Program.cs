using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using DesktopClient.ServicoEstoque;


namespace DesktopClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER when the service has started");
            Console.ReadLine();

            // Create a proxy object and connect to the service
            ServicoEstoqueClient proxy = new ServicoEstoqueClient("BasicHttpBinding_IServicoEstoque");

            // Add a product
            string NumeroProduto = "11000";
            Console.WriteLine("#Test 1: Add product - code: {0}", NumeroProduto);

            Produto newProduto = new Produto
            {
                numeroProduto = NumeroProduto,
                nomeProduto = "Produto 11",
                descricaoProduto = "Este é o produto 11",
                estoqueProduto = 111
            };

            if(proxy.IncluirProduto(newProduto))
            {
                Console.WriteLine("Product added");
            } 
            else
            {
                Console.WriteLine("Failed to add product");
            }

            Console.WriteLine("-----------------");

            // Delete a product
            NumeroProduto = "10000";

            Console.WriteLine("#Test 2: Delete product - code: {0}", NumeroProduto);
            
            if (proxy.RemoverProduto(NumeroProduto))
            {
                Console.WriteLine("Product deleted");
            }
            else
            {
                Console.WriteLine("Failed to delete product");
            }

            Console.WriteLine("-----------------");

            // Obtain a list of all products
            Console.WriteLine("#Test 3: List all products");

            List<string> products = proxy.ListarProdutos().ToList();
            
            foreach (string productInfo in products)
            {
                Console.WriteLine(productInfo);
            }

            Console.WriteLine("-----------------");

            // Get details of this product
            NumeroProduto = "2000";

            Console.WriteLine("#Test 4: Display product details - code: {0}", NumeroProduto);
            
            Produto produto = proxy.VerProduto(NumeroProduto);

            if (produto != null)
            {
                Console.WriteLine("Número: {0}", produto.numeroProduto);
                Console.WriteLine("Nome: {0}", produto.nomeProduto);
                Console.WriteLine("Descrição: {0}", produto.descricaoProduto);
                Console.WriteLine("Estoque: {0}", produto.estoqueProduto);
            }
            else
            {
                Console.WriteLine("No such product");
            }

            Console.WriteLine("-----------------");

            // Add stock for this product
            Console.WriteLine("#Test 5/6: Add stock - code: {0}", NumeroProduto);
            
            if (proxy.AdicionarEstoque(NumeroProduto, 10))
            {
                Console.WriteLine("Stock changed. Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));
            }
            else
            {
                Console.WriteLine("Stock update failed");
            }
            Console.WriteLine("-----------------");

            // Query the stock of this product
            NumeroProduto = "1000";

            Console.WriteLine("#Test 7: Display stock - code: {0}", NumeroProduto);
            Console.WriteLine("Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));

            Console.WriteLine("-----------------");

            // Subtract stock of this product
            Console.WriteLine("#Test 8/9: Subtract stock - code: {0}", NumeroProduto);

            if (proxy.RemoverEstoque(NumeroProduto, 20))
            {
                Console.WriteLine("Stock changed. Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));
            }
            else
            {
                Console.WriteLine("Stock update failed");
            }

            Console.WriteLine("-----------------");

            // Get details of this product
            Console.WriteLine("#Test 10: Display product details - code: {0}", NumeroProduto);

            produto = proxy.VerProduto(NumeroProduto);

            if (produto != null)
            {
                Console.WriteLine("Número: {0}", produto.numeroProduto);
                Console.WriteLine("Nome: {0}", produto.nomeProduto);
                Console.WriteLine("Descrição: {0}", produto.descricaoProduto);
                Console.WriteLine("Estoque: {0}", produto.estoqueProduto);
            }
            else
            {
                Console.WriteLine("No such product");
            }

            // Disconnect from the service
            proxy.Close();
            Console.WriteLine("Press ENTER to finish");
            Console.ReadLine();
        }
    }
}
