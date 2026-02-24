using PedidoRapido;
using PedidoRapido.Banco;
using PedidoRapido.Menu;
using System;
using System.Globalization;

var connectionString = "Server=localhost;Database=PedidoRapido;Trusted_Connection=True;";
var pedidoDAL = new PedidoDAL();

Console.WriteLine(" === PEDIDO RAPIDO ===");

Dictionary<int ,Menu> opcoes = new();
opcoes.Add(1, new MenuAdicionarPedido());
opcoes.Add(2, new MenuAtualizarPedido());
opcoes.Add(3, new MenuExibirPedido());
opcoes.Add(4, new MenuApagarPedido());

void ExibirOpcoesMenu()
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("Digite 1 para Registrar um novo pedido");
    Console.WriteLine("Digite 2 para Atualizar um pedido existente");
    Console.WriteLine("Digite 3 para Exibir pedidos registrados");
    Console.WriteLine("Digite 4 para Apagar um pedido");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine() ?? string.Empty;

    if (!int.TryParse(opcaoEscolhida, out int opcaoEscolhidanumerica))
    {
        Console.WriteLine("Opção inválida.");
        ExibirOpcoesMenu();
        return;
    }

    if (opcoes.ContainsKey(opcaoEscolhidanumerica))
    {
        Menu menuAserEscolhido = opcoes[opcaoEscolhidanumerica];
        menuAserEscolhido.Executar(new Pedido{Cliente = new Cliente()}); // passa objeto válido

        ExibirOpcoesMenu();
    }
    else
    {
        Console.WriteLine("Opção Inválida");
        ExibirOpcoesMenu();
    }
}

ExibirOpcoesMenu();

       
    


