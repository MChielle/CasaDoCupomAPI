using CasaDoCupom.Domain.DefaultLenght;
using CasaDoCupom.Domain.Interface.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasaDoCupom.Domain.Models
{
    public class EmpresaModel : IModel<Guid>
    {
        public EmpresaModel()
        {

        }

        public EmpresaModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        public IEnumerable<CupomModel> Cupons { get; set; } = new List<CupomModel>();
    }
}