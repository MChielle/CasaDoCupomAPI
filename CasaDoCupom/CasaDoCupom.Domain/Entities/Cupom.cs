using CasaDoCupom.Domain.DefaultLenght;
using CasaDoCupom.Domain.Entities.CRUDBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaDoCupom.Domain.Entities
{
    public class Cupom : EntityCRUD<Guid>
    {

        [Required]
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        [Required]
        [Range(0,99.99), Column(TypeName = "decimal(4,2)")]
        public decimal Desconto { get; set; }

        [Required]
        public bool Validado { get; set; }

        [Required]
        [MaxLength(LenghtTypes.CODIGO_CUPOM)]
        public string Codigo { get; set; }

        [Required]
        public bool Reservado { get; set; } = false;

    }
}