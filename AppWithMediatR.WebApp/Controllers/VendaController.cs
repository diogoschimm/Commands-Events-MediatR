using AppWithMediatR.ApplicationLayer.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppWithMediatR.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendaController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public IActionResult RegistrarVenda([FromBody]RegistrarVendaCommand command)
        {
            _mediator.Send(command);

            return Ok();
        }
    }
}