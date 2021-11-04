using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using VendasClient.ServicoEstoque;

namespace VendasClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER when the service has started");
            Console.ReadLine();

            // Create a proxy object and connect to the service
            ServicoEstoqueV2Client proxy = new ServicoEstoqueV2Client("WS2007HttpBinding_IServicoEstoque");

            // Query the stock of this product
            string NumeroProduto = "1000";

            Console.WriteLine("#Test 1: Product stock - code: {0}", NumeroProduto);
            Console.WriteLine("Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));

            Console.WriteLine("-----------------");

            // Add stock for this product
            Console.WriteLine("#Test 2/3: Add stock - code: {0}", NumeroProduto);

            if (proxy.AdicionarEstoque(NumeroProduto, 20))
            {
                Console.WriteLine("Stock changed. Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));
            }
            else
            {
                Console.WriteLine("Stock update failed");
            }

            Console.WriteLine("-----------------");

            // Query the stock of this product
            NumeroProduto = "5000";

            Console.WriteLine("#Test 4: Display stock - code: {0}", NumeroProduto);
            Console.WriteLine("Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));

            Console.WriteLine("-----------------");

            // Subtract stock of this product
            Console.WriteLine("#Test 5/6: Subtract stock - code: {0}", NumeroProduto);

            if (proxy.RemoverEstoque(NumeroProduto, 10))
            {
                Console.WriteLine("Stock changed. Current stock: {0}", proxy.ConsultarEstoque(NumeroProduto));
            }
            else
            {
                Console.WriteLine("Stock update failed");
            }

            // Disconnect from the service
            proxy.Close();
            Console.WriteLine("Press ENTER to finish");
            Console.ReadLine();
        }
    }
}
