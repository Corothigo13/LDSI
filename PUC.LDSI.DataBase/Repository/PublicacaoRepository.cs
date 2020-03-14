using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        private readonly AppDbContext _context;

        public PublicacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<Publicacao> ObterComAvaliacaoAsync(int id)
        //{
        //    var publicacao = await _context.Publicacoes.Include(x => x.Avaliacao).Where(x => x.Id == id).FirstOrDefaultAsync();
        //    //int publicacao = await _context.Publicacoes.Include(x => x.Avaliacao).Where(x => x.Id == id).FirstOrDefaultAsync();
        //    return publicacao;
        //}
    }
}
