using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using STARListener.Models;
using STARListener.Api.Data;


namespace STARListener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacoesCriticidadeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PontuacoesCriticidadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoCriticidade>>> GetPontuacoes()
        {
            return await _context.PontuacoesCriticidade.OrderBy(p => p.Nivel).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PontuacaoCriticidade>> PostPontuacao(PontuacaoCriticidade pontuacao)
        {
            if (_context.PontuacoesCriticidade.Any(p => p.Nivel == pontuacao.Nivel))
            {
                return Conflict($"Já existe uma configuração para o Nível {pontuacao.Nivel}.");
            }

            _context.PontuacoesCriticidade.Add(pontuacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPontuacoes), new { id = pontuacao.Nivel }, pontuacao);
        }

        [HttpPut("{nivel}")]
        public async Task<IActionResult> PutPontuacao(int nivel, PontuacaoCriticidade pontuacao)
        {
            if (nivel != pontuacao.Nivel)
            {
                return BadRequest();
            }

            _context.Entry(pontuacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{nivel}")]
        public async Task<IActionResult> DeletePontuacao(int nivel)
        {
            var pontuacao = await _context.PontuacoesCriticidade.FindAsync(nivel);
            if (pontuacao == null)
            {
                return NotFound();
            }

            _context.PontuacoesCriticidade.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
