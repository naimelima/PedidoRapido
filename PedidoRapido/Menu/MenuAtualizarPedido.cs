using PedidoRapido.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido.Menu;

internal class MenuAtualizarPedido : Menu
{
    public override void Executar(Pedido pedido)
    {
        base.Executar(pedido);
        ExibirTituloDaOpcao("Atualizar Pedido");

        Console.WriteLine("Digite o ID do pedido:");
        if (!int.TryParse(Console.ReadLine(), out int pedidoId))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        Console.WriteLine("Novo nome do cliente:");
        string nome = Console.ReadLine()!;

        Console.WriteLine("Novo endereço:");
        string endereco = Console.ReadLine()!;

        Console.WriteLine("Novo telefone:");
        string telefone = Console.ReadLine()!;

        Console.WriteLine("Novo valor do pedido:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valorPedido))
        {
            Console.WriteLine("Valor inválido.");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        Console.WriteLine("Nova taxa de entrega:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal taxaEntrega))
        {
            Console.WriteLine("Taxa inválida.");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        var pedidoAtualizado = new Pedido
        {
            Id = pedidoId,
            ValorPedido = valorPedido,
            ValorTaxaEntrega = taxaEntrega,
            Cliente = new Cliente
            {
                NomeCliente = nome,
                EnderecoCliente = endereco,
                TelefoneCliente = telefone
            }
        };

        var pedidoDAL = new PedidoDAL();
        bool atualizado = pedidoDAL.Atualizar(pedidoAtualizado);

        if (atualizado)
            Console.WriteLine("Pedido atualizado com sucesso!");
        else
            Console.WriteLine("Pedido não encontrado.");

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
        Console.Clear();
    }
}
