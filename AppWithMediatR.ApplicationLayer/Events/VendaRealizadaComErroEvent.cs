using System;
using System.Collections.Generic;
using System.Text;

namespace AppWithMediatR.ApplicationLayer.Events
{
    public class VendaRealizadaComErroEvent : Event 
    {
        public string Error { get; set; }
    }
}
