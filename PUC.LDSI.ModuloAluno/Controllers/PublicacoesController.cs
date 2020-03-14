using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.DataBase;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.ModuloAluno.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly AppDbContext _context;

        public PublicacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Publicacoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Publicacoes.Include(p => p.Avaliacao).Include(p => p.Turma);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Publicacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes
                .Include(p => p.Avaliacao)
                .Include(p => p.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }

        // GET: Publicacoes/Create
        public IActionResult Create()
        {
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id");
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id");
            return View();
        }

        // POST: Publicacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvaliacaoId,TurmaId,DataPublicacao,DataInicio,DataFim,ValorProva,Id")] Publicacao publicacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", publicacao.AvaliacaoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", publicacao.TurmaId);
            return View(publicacao);
        }

        // GET: Publicacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes.FindAsync(id);
            if (publicacao == null)
            {
                return NotFound();
            }
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", publicacao.AvaliacaoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", publicacao.TurmaId);
            return View(publicacao);
        }

        // POST: Publicacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvaliacaoId,TurmaId,DataPublicacao,DataInicio,DataFim,ValorProva,Id")] Publicacao publicacao)
        {
            if (id != publicacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacaoExists(publicacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", publicacao.AvaliacaoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", publicacao.TurmaId);
            return View(publicacao);
        }

        // GET: Publicacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacoes
                .Include(p => p.Avaliacao)
                .Include(p => p.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }

        // POST: Publicacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicacao = await _context.Publicacoes.FindAsync(id);
            _context.Publicacoes.Remove(publicacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacaoExists(int id)
        {
            return _context.Publicacoes.Any(e => e.Id == id);
        }
    }
}
