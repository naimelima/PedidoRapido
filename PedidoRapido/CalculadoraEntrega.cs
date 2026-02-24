using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoRapido
{
    public static class CalculadoraEntrega
    {
        public static decimal Calcular(double distanciaKm)
        {
            if (distanciaKm <= 3)
                return 6.00m;

            if (distanciaKm <= 6)
                return 8.00m;

            return 12.00m;
        }
    }
}
