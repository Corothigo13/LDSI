using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUC.LDSI.Domain.Entities
{
    public class Turma : Entity
    {
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public List<Publicacao> Publicacoes { get; set; }
        public List<Aluno> Alunos { get; set; }
    }
}
