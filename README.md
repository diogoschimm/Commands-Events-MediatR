# Commands-Events-MediatR
Comandos e Eventos com MediatR

```
Install-Package MediatR
Install-Package MediatR.Extensions.Microsoft.DependencyInjection
```
## Injeção de Dependências

Método ConfigureServices do Arquivo Startup

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    
    // MediatR
    services.AddMediatR(typeof(Startup));

    // Command Handlers
    services.AddTransient<IRequestHandler<RegistrarVendaCommand, bool>, RegistrarVendaCommandHandler>();
    services.AddTransient<IRequestHandler<RegistrarPagamentoCommand, bool>, RegistrarPagamentoCommandHandler>();

    // Event Handlers
    services.AddTransient<INotificationHandler<VendaRealizadaComSucessoEvent>, RegistrarVendaEventHandler>();
    services.AddTransient<INotificationHandler<VendaRealizadaComErroEvent>, RegistrarVendaEventHandler>();
}
```

## Controller

Utilizamos ao IMediator injetado via construtor para enviar o comando.

```cs
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
```

## Tratando o Command

Através da implementação da interface IRequestHandler podemos tratar o método Handle

```cs
public class RegistrarVendaCommandHandler : IRequestHandler<RegistrarVendaCommand, bool>
{
    private readonly IMediator _mediator;

    public RegistrarVendaCommandHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public Task<bool> Handle(RegistrarVendaCommand request, CancellationToken cancellationToken)
    {
        if (VendaSalva())
        {
            var idVenda = new Random().Next(1000000);
            PublicarEventoSucesso(idVenda, request);
        }
        else
        {
            PublicarEventoErro("Banco de dados indisponível");
        }

        return Task.FromResult(true);
    }
    
    private void PublicarEventoSucesso(int idVenda, RegistrarVendaCommand command)
    {
        var vendaComSucesso = new VendaRealizadaComSucessoEvent
        {
            IdVenda = idVenda,
            RegistrarVendaCommand = command 
        };
        _mediator.Publish(vendaComSucesso);
    }
    ....
```

## Tratando os Events

Utilizamos a implementação da interface INotificationHandler para tratar os eventos 

```cs
public class RegistrarVendaEventHandler :
    INotificationHandler<VendaRealizadaComSucessoEvent>,
    INotificationHandler<VendaRealizadaComErroEvent>
{

    private readonly IMediator _mediator;

    public RegistrarVendaEventHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public Task Handle(VendaRealizadaComSucessoEvent notification, CancellationToken cancellationToken)
    {
        return Task.Run(() => {

            var registrarPagamento = new RegistrarPagamentoCommand
            {
                IdVenda = notification.IdVenda,
                DataPagamento = notification.RegistrarVendaCommand.DataPagamento,
                ValorPago = notification.RegistrarVendaCommand.ValorPago
            };

            _mediator.Send(registrarPagamento);
        });
    }
    ...
```
