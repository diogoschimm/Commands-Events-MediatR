using AppWithMediatR.ApplicationLayer.Dtos;
using System;
using System.Collections.Generic;

namespace AppWithMediatR.ApplicationLayer.Commands
{
    public class RegistrarVendaCommand : Command<bool>
    {
        public int IdVendedor { get; set; }

        public Cliente Cliente { get; set; }

        public double ValorTotal { get; set; }

        public IList<ItemVenda> Itens { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
