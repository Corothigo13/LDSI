using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IPublicacaoService
    {
        Task<int> AdicionarPublicacaoAsync(string descricao);
        Task<int> AlterarPublicacaoAsync(int id, string descricao);
        Task ExcluirAsync(int id);
    }
}
