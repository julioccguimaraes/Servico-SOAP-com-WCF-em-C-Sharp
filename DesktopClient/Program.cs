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
            // Create a proxy object and connect to the service
            ServicoEstoqueClient proxy = new ServicoEstoqueClient("BasicHttpBinding_IProductsService");

        }
    }
}
