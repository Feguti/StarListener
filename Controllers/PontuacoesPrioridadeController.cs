using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using STARListener.Models;
using STARListener.Api.Data;


namespace STARListener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacoesPrioridadeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PontuacoesPrioridadeController(ApplicationDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoPrioridade>>> GetPontuacoes()
        {
            return await _context.PontuacoesPrioridade.OrderBy(p => p.Nivel).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PontuacaoPrioridade>> PostPontuacao(PontuacaoPrioridade pontuacao)
        {
            // Verifica se já existe uma pontuação para este nível
            if (_context.PontuacoesPrioridade.Any(p => p.Nivel == pontuacao.Nivel))
            {
                return Conflict($"Já existe uma configuração para o Nível {pontuacao.Nivel}.");
            }

            _context.PontuacoesPrioridade.Add(pontuacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPontuacoes), new { id = pontuacao.Nivel }, pontuacao);
        }

        [HttpPut("{nivel}")]
        public async Task<IActionResult> PutPontuacao(int nivel, PontuacaoPrioridade pontuacao)
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
            var pontuacao = await _context.PontuacoesPrioridade.FindAsync(nivel);
            if (pontuacao == null)
            {
                return NotFound();
            }

            _context.PontuacoesPrioridade.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
