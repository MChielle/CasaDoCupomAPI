using CasaDoCupom.Domain.Entities.CRUDBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CasaDoCupom.Domain.Entities
{
    public class Empresa : EntityCRUD<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [AllowNull]
        public IEnumerable<Cupom> Cupons { get; set; } = new List<Cupom>();
    }
}