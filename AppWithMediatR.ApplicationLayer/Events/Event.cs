using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWithMediatR.ApplicationLayer.Events
{
    public abstract class Event : INotification
    {
    }
}
