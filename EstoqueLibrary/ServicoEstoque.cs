using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using EstoqueEntityModel;
using System.ServiceModel.Activation;

namespace EstoqueLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    // This implementation performs minimal error checking and exception handling
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServicoEstoque : IServicoEstoque, IServicoEstoqueV2
    {
        public bool AdicionarEstoque(string NumeroProduto, int Quantidade)
        {
            try
            {
                // Connect to the ProvedorEstoque database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (!ProductExists(NumeroProduto, database))
                        return false;
                    else
                    {
                        // Find the product object that matches the parameters passed
                        // in to the operation
                        ProdutoEstoque produto = database.ProdutoEstoque.First(p => string.Compare(p.NumeroProduto, NumeroProduto) == 0);

                        produto.EstoqueProduto += Quantidade;
                        database.ProdutoEstoque.Add(produto);

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;
        }

        public int ConsultarEstoque(string NumeroProduto)
        {
            int quantityTotal = 0;
            try
            {
                // Connect to the ProvedorEstoque database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (ProductExists(NumeroProduto, database))
                    {
                        // Calculate the sum of all quantities for the specified product
                        quantityTotal = (from p in database.ProdutoEstoque 
                                         where string.Compare(p.NumeroProduto, NumeroProduto) == 0
                                         select p.EstoqueProduto).First();
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }
            // Return the stock level
            return quantityTotal;
        }

        public bool IncluirProduto(Produto produto)
        {
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (ProductExists(produto.NumeroProduto, database))
                        return false;
                    else
                    {
                        // Add the product
                        ProdutoEstoque produtoEstoque = new ProdutoEstoque
                        {
                            NumeroProduto = produto.NumeroProduto,
                            NomeProduto = produto.NomeProduto,
                            DescricaoProduto = produto.DescricaoProduto,
                            EstoqueProduto = produto.EstoqueProduto
                        };

                        database.ProdutoEstoque.Add(produtoEstoque);

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;
        }

        public List<string> ListarProdutos()
        {
            // Create a list of products
            List<string> productsList = new List<string>();
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Fetch the products in the database
                    List<ProdutoEstoque> products = (from product in database.ProdutoEstoque
                                              select product).ToList();

                    foreach (ProdutoEstoque product in products)
                    {
                        productsList.Add("Número: " + product.NumeroProduto + ", Nome: " + product.NomeProduto + ", Estoque: " + product.EstoqueProduto);
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }
            // Return the list of products
            return productsList;
        }

        public bool RemoverEstoque(string NumeroProduto, int Quantidade)
        {
            try
            {
                // Connect to the ProvedorEstoque database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (!ProductExists(NumeroProduto, database))
                        return false;
                    else
                    {
                        // Find the product object that matches the parameters passed
                        // in to the operation
                        ProdutoEstoque produto = database.ProdutoEstoque.First(p => string.Compare(p.NumeroProduto, NumeroProduto) == 0);

                        produto.EstoqueProduto -= Quantidade;
                        database.ProdutoEstoque.Add(produto);

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;
        }

        public bool RemoverProduto(string NumeroProduto)
        {
            try
            {
                // Connect to the ProvedorEstoque database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (!ProductExists(NumeroProduto, database))
                        return false;
                    else
                    {
                        // Find the product object that matches the parameters passed
                        // in to the operation
                        ProdutoEstoque produto = database.ProdutoEstoque.First(p => string.Compare(p.NumeroProduto, NumeroProduto) == 0);

                        database.ProdutoEstoque.Remove(produto);

                        // Save the change back to the database
                        database.SaveChanges();
                    }
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;
        }

        public Produto VerProduto(string NumeroProduto)
        {
            Produto produto = null;
            try
            {
                // Connect to the ProvedorEstoque database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    if (ProductExists(NumeroProduto, database))
                    {
                        // Find the product object that matches the parameters passed
                        ProdutoEstoque matchingProduct = database.ProdutoEstoque.First(p =>
                            string.Compare(p.NomeProduto, NumeroProduto) == 0);

                        produto = new Produto()
                        {
                            NumeroProduto = matchingProduct.NumeroProduto,
                            NomeProduto = matchingProduct.NomeProduto,
                            DescricaoProduto = matchingProduct.DescricaoProduto,
                            EstoqueProduto = matchingProduct.EstoqueProduto
                        };
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }
            // Return the product
            return produto;
        }

        public bool ProductExists(string NumeroProduto, ProvedorEstoque database)
        {
            // Check to see whether the specified product exists in the database
            int numProducts = (from p in database.ProdutoEstoque
                               where string.Equals(p.NumeroProduto, NumeroProduto)
                               select p).Count();

            return numProducts > 0;
        }
    }
}
