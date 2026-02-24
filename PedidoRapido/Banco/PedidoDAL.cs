using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido.Banco;

public class PedidoDAL
{
    public IEnumerable<Pedido> Listar()

    {
        var lista = new List<Pedido>();//lista vazia
        using var connection = new Connection().ObterConexao();
        connection.Open(); //abre a conexao

        string sql = "SELECT * FROM Pedidos";
        SqlCommand command = new SqlCommand(sql, connection); //comando sql
        using SqlDataReader dataReader = command.ExecuteReader(); //executa o comando

        while (dataReader.Read()) //lendo os dados
        {
            string nomeCliente = Convert.ToString(dataReader["NomeCliente"]);
            string enderecoCliente = Convert.ToString(dataReader["EnderecoCliente"]);
            string telefoneCliente = Convert.ToString(dataReader["TelefoneCliente"]);
            decimal valorPedido = Convert.ToDecimal(dataReader["ValorPedido"]);
            decimal taxaDeEntrega = Convert.ToDecimal(dataReader["ValorTaxaEntrega"]);
            decimal valorTotal = Convert.ToDecimal(dataReader["ValorTotal"]);
            int idPedido = Convert.ToInt32(dataReader["Id"]);
            var cliente = new Cliente
            {
                NomeCliente = nomeCliente,
                EnderecoCliente = enderecoCliente,
                TelefoneCliente = telefoneCliente
            };

            Pedido pedido = new Pedido
            {
                Id = idPedido,
                Cliente = cliente,
                ValorTotal = valorTotal,
                ValorTaxaEntrega = taxaDeEntrega,
                ValorPedido = valorPedido
            };


            lista.Add(pedido);//adiciona o pedido na lista

        }
        return lista;
    }

    public void Adicionar(Pedido pedido)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();
        string sql = "INSERT INTO Pedidos (NomeCliente, EnderecoCliente, TelefoneCliente, ValorPedido, ValorTaxaEntrega, ValorTotal) " +
                     "VALUES (@NomeCliente, @EnderecoCliente, @TelefoneCliente, @ValorPedido, @ValorTaxaEntrega, @ValorTotal)";
        SqlCommand command = new SqlCommand(sql, connection); //comando sql
        command.Parameters.AddWithValue("@NomeCliente", pedido.Cliente.NomeCliente);
        command.Parameters.AddWithValue("@EnderecoCliente", pedido.Cliente.EnderecoCliente);
        command.Parameters.AddWithValue("@TelefoneCliente", pedido.Cliente.TelefoneCliente);
        command.Parameters.AddWithValue("@ValorPedido", pedido.ValorPedido);
        command.Parameters.AddWithValue("@ValorTaxaEntrega", pedido.ValorTaxaEntrega);
        command.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotalPedido());
        
        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"{retorno} linha(s) inserida(s).");
    }

    public void Remover(int id)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();
        string sql = "DELETE FROM Pedidos WHERE Id = @Id";
        SqlCommand command = new SqlCommand(sql, connection); //comando sql
        command.Parameters.AddWithValue("@Id", id);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"{retorno} linha(s) removida(s).");
    }

    public bool Atualizar(Pedido pedido)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = @"UPDATE Pedidos 
                   SET NomeCliente = @NomeCliente,
                       EnderecoCliente = @EnderecoCliente,
                       TelefoneCliente = @TelefoneCliente,
                       ValorPedido = @ValorPedido,
                       ValorTaxaEntrega = @ValorTaxaEntrega,
                       ValorTotal = @ValorTotal
                   WHERE Id = @Id";

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", pedido.Id);
        command.Parameters.AddWithValue("@NomeCliente", pedido.Cliente.NomeCliente);
        command.Parameters.AddWithValue("@EnderecoCliente", pedido.Cliente.EnderecoCliente);
        command.Parameters.AddWithValue("@TelefoneCliente", pedido.Cliente.TelefoneCliente);
        command.Parameters.AddWithValue("@ValorPedido", pedido.ValorPedido);
        command.Parameters.AddWithValue("@ValorTaxaEntrega", pedido.ValorTaxaEntrega);
        command.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotalPedido());

        int linhasAfetadas = command.ExecuteNonQuery();

        return linhasAfetadas > 0;
    }

}
