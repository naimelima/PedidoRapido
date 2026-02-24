using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedidoRapido.Banco;

namespace PedidoRapido.Menu;

internal class MenuExibirPedido : Menu
{
    public override void Executar(Pedido pedido)
    {
        base.Executar(pedido);
        ExibirTituloDaOpcao("Lista de Pedidos");

        var pedidoDAL = new PedidoDAL();
        var pedidos = pedidoDAL.Listar();

        if (!pedidos.Any())
        {
            Console.WriteLine("Nenhum pedido encontrado.");
        }
        else
        {
            foreach (var item in pedidos)
            {
                Console.WriteLine($"Pedido ID: {item.Id}");
                Console.WriteLine($"Cliente: {item.Cliente.NomeCliente}");
                Console.WriteLine($"Endereço: {item.Cliente.EnderecoCliente}");
                Console.WriteLine($"Telefone: {item.Cliente.TelefoneCliente}");
                Console.WriteLine($"Valor do Pedido: {item.ValorPedido:C}");
                Console.WriteLine($"Taxa de Entrega: {item.ValorTaxaEntrega:C}");
                Console.WriteLine($"Valor Total: {item.ValorTotal:C}");
                Console.WriteLine(new string('-', 30));
            }
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
        Console.Clear();
    }
}
