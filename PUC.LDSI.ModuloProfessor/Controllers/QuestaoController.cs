using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using PUC.LDSI.Domain.QueryResult;

namespace PUC.LDSI.ModuloProfessor.Controllers
{
    public class QuestaoController : BaseController
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public QuestaoController(UserManager<Usuario> user,
                                 IAvaliacaoService avaliacaoService) : base(user)
        {
            _avaliacaoService = avaliacaoService;
        }

        public async Task<IActionResult> Index(int? avaliacaoId)
        {
            if (avaliacaoId == null)
            {
                return NotFound();
            }

            var avaliacao = await _avaliacaoService.ObterAvaliacaoQueryResultAsync(avaliacaoId.Value);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        public IActionResult Create(int? avaliacaoId)
        {
            var avaliacao = _avaliacaoService.ObterAsync(avaliacaoId).Result;

            if (avaliacao == null)
            {
                return NotFound();
            }

            ViewData["Avaliacao"] = avaliacao;

            ViewData["OpcoesTipo"] = ObterOpcoesTipo();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvaliacaoId,Tipo,Enunciado")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                await _avaliacaoService.AdicionarQuestaoAvaliacaoAsync(questao.AvaliacaoId, questao.Tipo, questao.Enunciado);

                return RedirectToAction(nameof(Index), new { avaliacaoId = questao.AvaliacaoId });
            }

            return View(questao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _avaliacaoService.ObterQuestaoQueryResultAsync(id.Value);

            if (questao == null)
            {
                return NotFound();
            }

            ViewData["OpcoesTipo"] = ObterOpcoesTipo(questao.TipoId);

            return View(questao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestaoId,TipoId,Enunciado")] QuestaoQueryResult questao)
        {
            if (id != questao.QuestaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var parentId = await _avaliacaoService.AlterarQuestaoAvaliacaoAsync(questao.QuestaoId, questao.TipoId, questao.Enunciado);

                return RedirectToAction(nameof(Index), new { avaliacaoId = parentId });
            }

            return View(questao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _avaliacaoService.ObterQuestaoAsync(id.Value);
            
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parentId = await _avaliacaoService.ExcluirQuestaoAvaliacaoAsync(id);

            return RedirectToAction(nameof(Index), new { avaliacaoId = parentId });
        }

        private List<SelectListItem> ObterOpcoesTipo(int tipoId = 0) {
            return new List<SelectListItem>() {
                new SelectListItem{ Text="Múltipla Escolha", Value = "1", Selected = tipoId == 1 },
                new SelectListItem{ Text="Verdadeiro ou Falso", Value = "2", Selected = tipoId == 2 }
            };
        }
    }
}
