using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using STARListener.Models;
using STARListener.Api.Data;


namespace STARListener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacoesDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PontuacoesDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoData>>> GetPontuacaoData()
        {
            return await _context.PontuacoesData.OrderBy(r => r.DeDiasAtras).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PontuacaoData>> PostPontuacaoData(PontuacaoData regra)
        {
            _context.PontuacoesData.Add(regra);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPontuacaoData), new { id = regra.Id }, regra);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PontuacaoData>> PutPontuacaoData(int id, PontuacaoData pontuacao)
        {
            if (id != pontuacao.Id)
            {
                return BadRequest("Não há um intervalo registrado com esse ID!");
            }

            _context.Entry(pontuacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Pontuação do intervalo atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontuacaoData(int id)
        {
            var pontuacao = await _context.PontuacoesData.FindAsync(id);
            if (pontuacao == null)
            {
                return NotFound("Não há um intervalo registrado com esse ID!");
            }

            _context.PontuacoesData.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return Ok("Intervalo deletado com sucesso!");
        }
    }
}
