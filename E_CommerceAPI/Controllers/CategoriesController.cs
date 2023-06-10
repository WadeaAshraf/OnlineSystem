using E_CommerceApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
       
        private IMediator mediator;
        public CategoriesController(IMediator _mediator) {
            mediator = _mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoriesQuery(CancellationToken cancellationToken)
        {
            var result =await mediator.Send(new GetCategoriesQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
