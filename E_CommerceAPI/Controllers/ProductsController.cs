using E_CommerceApplication.Commands;
using E_CommerceApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading;

namespace E_CommerceAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IMediator mediator;
        public ProductsController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        // [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> AddNewProduct([FromBody]AddProductCommand addProductCommand,CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(addProductCommand, cancellationToken);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken,int PageIndex, int PageSize = 5)
        {
            try
            {
                var result = await mediator.Send(new GetProductsQuery { PageIndex=PageIndex,PageSize=PageSize}, cancellationToken);
                return Ok(result);
               // return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
