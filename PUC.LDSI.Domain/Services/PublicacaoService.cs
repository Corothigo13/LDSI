using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class PublicacaoService : IPublicacaoService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        public PublicacaoService(IPublicacaoRepository publicacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
        }
        public async Task<int> AdicionarPublicacaoAsync(string descricao)
        {
            var publicacao = new Publicacao(); //{ Nome = descricao };

            _publicacaoRepository.Adicionar(publicacao);

            await _publicacaoRepository.SaveChangesAsync();

            return publicacao.Id;
        }
        public async Task<int> AlterarPublicacaoAsync(int id, string descricao)
        {
            var publicacao = await _publicacaoRepository.ObterAsync(id);
            //publicacao.Nome = descricao;
            _publicacaoRepository.Modificar(publicacao);
            return await _publicacaoRepository.SaveChangesAsync();
        }
        public async Task ExcluirAsync(int id)
        {
            //var publicacao = await _publicacaoRepository.ObterComAvaliacaoAsync(id);
            //if (publicacao.Avaliacao?.Count > 0)
        //throw new DomainException("Não é possível excluir uma publicacao que já possui avaliacao!");
        _publicacaoRepository.Remover(id);
        await _publicacaoRepository.SaveChangesAsync();
        }
    }
}
