using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        // _mediator??= -> eğer hali hazırda elimizde bir _mediator bulunuyorsa onu döndür yoksa IoC ortamından IMediator'ın karşılığını getir.
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
