using Microsoft.Data.SqlClient;
using PedidoRapido.Banco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido.Menu;

internal class MenuApagarPedido : Menu
{
    public override void Executar(Pedido pedido)
    {
        base.Executar(pedido);
        ExibirTituloDaOpcao("Apagar Pedido");

        Console.WriteLine("Digite o ID do pedido que deseja apagar:");

        if (!int.TryParse(Console.ReadLine(), out int pedidoId))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        var pedidoDAL = new PedidoDAL();
        pedidoDAL.Remover(pedidoId);

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
        Console.Clear();
    }
}