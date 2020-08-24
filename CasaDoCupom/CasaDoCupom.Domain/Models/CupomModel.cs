using CasaDoCupom.Domain.DefaultLenght;
using CasaDoCupom.Domain.Interface.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CasaDoCupom.Domain.Models
{
    public class CupomModel : IModel<Guid>
    {
        public CupomModel()
        {

        }

        public CupomModel(Guid id, Guid empresaId, decimal desconto, bool validado, string codigo, bool disponivel)
        {
            Id = id;
            EmpresaId = empresaId;
            Desconto = desconto;
            Validado = validado;
            Codigo = codigo;
            Reservado = disponivel;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "EmpresaId é obrigatório")]
        public Guid EmpresaId { get; set; }

        [Required(ErrorMessage = "Valor do desconto promocional é obrigatório")]
        public decimal Desconto { get; set; }

        public bool Validado { get; set; } = false;

        [Required(ErrorMessage = "Código é obrigatório")]
        [MaxLength(LenghtTypes.CODIGO_CUPOM, ErrorMessage = "Tamanho do código inválido.")]
        public string Codigo { get; set; }

        public bool Reservado { get; set; } = false;

        public DateTime DataCriacao { get; set; }
    }
}