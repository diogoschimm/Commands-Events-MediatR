using AppWithMediatR.ApplicationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWithMediatR.ApplicationLayer.Events
{
    public class VendaRealizadaComSucessoEvent : Event
    {
        public int IdVenda { get; set; }

        public RegistrarVendaCommand RegistrarVendaCommand { get; set; }
    }
}
