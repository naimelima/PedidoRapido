using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido;

public class Pedido
{
   

    public required Cliente Cliente { get; set; }
    public decimal ValorPedido { get; set; }
    public double DistanciaKm { get; set; }
    public decimal ValorTaxaEntrega { get; set; }
    public string? NomeCliente { get; }
    public string? EnderecoCliente { get; }
    public string? TelefoneCliente { get; }
    public int Id { get; set; }
    public decimal ValorTotal { get; set; }

    public decimal ValorTotalPedido()
    {
        return ValorPedido + ValorTaxaEntrega;
    }
}
