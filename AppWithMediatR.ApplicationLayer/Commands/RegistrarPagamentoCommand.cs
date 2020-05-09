using System;
using System.Collections.Generic;
using System.Text;

namespace AppWithMediatR.ApplicationLayer.Commands
{
    public class RegistrarPagamentoCommand : Command<bool>
    {
        public int IdVenda { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
