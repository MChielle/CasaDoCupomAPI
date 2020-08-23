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

        public EmpresaModel(Guid id, string nome, string cNPJ)
        {
            Id = id;
            Nome = nome;
            CNPJ = cNPJ;
        }

        [Required(ErrorMessage = "Id é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string CNPJ { get; set; }

        public DateTime DataCriacao { get; set; }

        public IEnumerable<CupomModel> Cupons { get; set; } = new List<CupomModel>();
    }
}