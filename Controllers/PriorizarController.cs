using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STARListener.Services;

namespace STARListener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorizarController : ControllerBase
    {
        private readonly PriorizacaoService _priorizacaoService;

        public PriorizarController(PriorizacaoService priorizacaoService)
        {
            _priorizacaoService = priorizacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListaPriorizada([FromQuery] string responsavel = null)
        {
            var listaFinal = await _priorizacaoService.CalcularPrioridadesAsync(responsavel);
            return Ok(listaFinal);
        }

    }
}
