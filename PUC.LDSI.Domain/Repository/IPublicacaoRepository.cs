using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Repository
{
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        //Task<Avaliacao> ObterComAvaliacaoAsync(int id);
    }
}
