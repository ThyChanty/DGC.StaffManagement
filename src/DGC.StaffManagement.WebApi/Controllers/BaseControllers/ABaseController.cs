using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DGC.StaffManagement.WebApi.Controllers.BaseControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class AControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
