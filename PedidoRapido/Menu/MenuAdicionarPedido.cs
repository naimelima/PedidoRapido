using PedidoRapido.Banco;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido.Menu;

internal class MenuAdicionarPedido : Menu
{
    public override void Executar(Pedido pedido)
    {
            base.Executar(pedido);
            ExibirTituloDaOpcao("Registrar um novo pedido");
        bool continuar = true;

        while (continuar)
        {
            var cliente = new Cliente();

            Console.Write("\nNome do Cliente: ");
            cliente.NomeCliente = Console.ReadLine();

            Console.Write("Telefone: ");
            cliente.TelefoneCliente = Console.ReadLine();

            Console.Write("Endereço de entrega: ");
            cliente.EnderecoCliente = Console.ReadLine();

            Console.Write("Forma de pagamento: ");
            cliente.FormaDePagamento = Console.ReadLine();

            // Valor do pedido (com validação)
            decimal valorPedido;
            Console.Write("Valor do pedido (R$): ");
            while (!decimal.TryParse(Console.ReadLine(),
                   NumberStyles.Number,
                   CultureInfo.CurrentCulture,
                   out valorPedido))
            {
                Console.Write("Valor inválido. Digite novamente (ex: 100,50): ");
            }

            //Distância (com validação)
            double distancia;
            Console.Write("Distância até o cliente (km): ");
            while (!double.TryParse(Console.ReadLine(),
                   NumberStyles.Number,
                   CultureInfo.CurrentCulture,
                   out distancia))
            {
                Console.Write("Distância inválida. Digite novamente (ex: 5,2): ");
            }

            var pedidos = new Pedido
            {
                Cliente = cliente,
                ValorPedido = valorPedido,
                DistanciaKm = distancia
            };

            pedidos.ValorTaxaEntrega = (decimal)CalculadoraEntrega.Calcular(distancia);

            //Resumo
            Console.WriteLine("\n--- RESUMO DO PEDIDO ---");
            Console.WriteLine($"Cliente: {cliente.NomeCliente}");
            Console.WriteLine($"Endereço: {cliente.EnderecoCliente}");
            Console.WriteLine($"Telefone: {cliente.TelefoneCliente}");
            Console.WriteLine($"Valor do pedido: R$ {pedidos.ValorPedido:F2}");
            Console.WriteLine($"Taxa de entrega: R$ {pedidos.ValorTaxaEntrega:F2}");
            Console.WriteLine($"TOTAL: R$ {pedidos.ValorTotalPedido():F2}");

            //Confirmação
            Console.Write("\nConfirmar pedido? (S/N): ");
            var confirmacao = Console.ReadLine();

            if (confirmacao?.Trim().ToUpper() == "S")
            {
                var pedidoDAL = new PedidoDAL();
                pedidoDAL.Adicionar(pedidos);

                Console.WriteLine("\n✅ Pedido salvo com sucesso!");
            }
            else
            {
                Console.WriteLine("\n❌ Pedido cancelado.");
            }

            //Pergunta se continua
            Console.Write("\nDeseja adicionar outro pedido? (S/N): ");
            var resposta = Console.ReadLine();

            continuar = resposta?.Trim().ToUpper() == "S";
        }

        Console.WriteLine("\nSistema encerrado. Obrigado por usar o Pedido Rápido!");





    }
}
